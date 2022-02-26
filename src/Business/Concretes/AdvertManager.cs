﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concretes
{
    public class AdvertManager : IAdvertService
    {
        private readonly IAdvertDal _advertDal;

        private readonly IAdvertImageService _imageService;

        private readonly ILocationService _locationService;

        public AdvertManager(IAdvertDal advertDal, IAdvertImageService imageService, ILocationService locationService)
        {
            _imageService = imageService;
            _advertDal = advertDal;
            _locationService = locationService;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        [ValidationAspect(typeof(AdvertImageValidator))]
        public async Task<IDataResult<Advert>> Add(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location)
        {
            var locationResult = _locationService.Add(location);
            if (locationResult is null)
            {
                return new ErrorDataResult<Advert>(null);
            }
            advert.LocationId = locationResult.Data.Id;
            var result = _advertDal.Add(advert);
            if (result is not null)
            {

                foreach (var file in files)
                {
                    var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                    var fileResult = await fileHelper.UploadAsync(file);
                    if (fileResult.Success)
                    {
                        var resultImage = _imageService.Add(new AdvertImage
                        {
                            ImagePath = fileResult.Message.Split("&&")[0],
                            PublicId = fileResult.Message.Split("&&")[1],
                            AdvertId = result.Id
                        });
                        if (!resultImage.Success)
                        {
                            return new ErrorDataResult<Advert>(null);
                        }
                    }
                }

                return new SuccessDataResult<Advert>(result);


            }

            return new ErrorDataResult<Advert>(null);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public async Task<IResult> Delete(Advert advert)
        {
            var imageList = _imageService.GetByAdvertId(advert.Id);
            for (int i = 0; i < imageList.Data.Count; i++)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                await fileHelper.DeleteAsync(imageList.Data[i].ImagePath, imageList.Data[i].PublicId);
                _imageService.Delete(imageList.Data[i]);
            }

            _advertDal.Delete(advert);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<Advert> Get(int id)
        {
            var data = _advertDal.Get(a => a.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<Advert>(data);
            }

            return new ErrorDataResult<Advert>(null);
        }
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<List<AdvertReadDto>> GetAllAdvertDetail(int pageNumber)
        {
            var data = _advertDal.GetAllAdvertDetail(pageNumber);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertReadDto>>(data);
            }

            return new ErrorDataResult<List<AdvertReadDto>>(null);
        }
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<List<AdvertReadDto>> GetAllAdvertDetailsByFilter(int pageNumber)
        {
            var data = _advertDal.GetAllAdvertDetailsByFilter(null, pageNumber);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertReadDto>>(data);
            }

            return new ErrorDataResult<List<AdvertReadDto>>(null);
        }
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<AdvertReadDto> GetAdvertDetailById(int id)
        {
            var data = _advertDal.GetAdvertDetailById(id);
            if (data is not null)
            {
                return new SuccessDataResult<AdvertReadDto>(data);
            }

            return new ErrorDataResult<AdvertReadDto>(null);
        }
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<List<Advert>> GetAll()
        {
            var data = _advertDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Advert>>(data);
            }

            return new ErrorDataResult<List<Advert>>(null);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        [ValidationAspect(typeof(AdvertImageValidator))]
        public async Task<IResult> Update(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location)
        {
            var image = _imageService.GetByAdvertId(advert.Id);

            if (files is not null)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);

                for (int i = 0; i < files.Count(); i++)
                {
                    var fileResult = await fileHelper.UpdateAsync(files[i], image.Data[i].ImagePath, image.Data[i].PublicId);
                    var result = _imageService.Update(new AdvertImage
                    {
                        ImagePath = fileResult.Message.Split("&&")[0],
                        PublicId = fileResult.Message.Split("&&")[1],
                        AdvertId = advert.Id,
                        Id = image.Data[i].Id
                    });
                    if (!result.Success)
                    {
                        return new ErrorResult();
                    }
                }
            }

            _locationService.Update(location);

            _advertDal.Update(advert);

            return new SuccessResult();
        }
    }
}
