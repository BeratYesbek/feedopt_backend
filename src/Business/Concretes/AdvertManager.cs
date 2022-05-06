using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.BusinessRules;
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
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;
using Entity.Dtos.Filter;
using System.Linq.Expressions;
using Business.BackgroundJob.Hangfire;
using Business.Filters;
using Business.Messages.MethodMessages;
using Business.Services.Abstracts;

namespace Business.Concretes
{
    public class AdvertManager : AdvertFilter, IAdvertService
    {
        private readonly IAdvertDal _advertDal;

        private readonly IAdvertImageService _imageService;

        private readonly ILocationService _locationService;

        private readonly ITelegramService _telegramService;

        private readonly IUserService _userService;

        public AdvertManager(IAdvertDal advertDal, IAdvertImageService imageService, ILocationService locationService,
            ITelegramService telegramService, IUserService userService)
        {
            _imageService = imageService;
            _advertDal = advertDal;
            _locationService = locationService;
            _telegramService = telegramService;
            _userService = userService;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        [ValidationAspect(typeof(AdvertValidator))]
        public async Task<IDataResult<Advert>> Add(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location)
        {
            var ruleResult = Core.Utilities.Business.BusinessRules.Run(
                AdvertBusinessRules.CheckFilesSize(files),
                AdvertBusinessRules.CheckDescriptionIllegalKeyword(advert.Description));

            if (!ruleResult.Success)
                return new ErrorDataResult<Advert>(null, ruleResult.Message);

            var locationResult = _locationService.Add(location);

            if (locationResult is null)
                return new SuccessDataResult<Advert>(null, AdvertMessages.AdvertAdd);


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
                            return new ErrorDataResult<Advert>(null,AdvertMessages.AdvertAdvertFailed);
                        }
                    }
                }

                //Job.Create<AdvertJob>().UpdateAdvertStatusJob(this, result);
                var user = _userService.Get(advert.Id);
                // telegram service is going to send a message our telegram channel
                await _telegramService.SendNewPostAsync(advert, user.Data);

                return new SuccessDataResult<Advert>(result,AdvertMessages.AdvertAdd);
            }

            return new ErrorDataResult<Advert>(null,AdvertMessages.AdvertAdvertFailed);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IResult Delete(Advert advert)
        {
            advert.IsDeleted = true;
            _advertDal.Update(advert);
            return new SuccessResult(AdvertMessages.AdvertDelete);
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
        public IDataResult<List<AdvertReadDto>> GetAllAdvertDetail(int pageNumber, double latitude, double longitude)
        {
            var data = _advertDal.GetAllAdvertDetail(pageNumber, latitude, longitude);
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
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertCategoryGetAll},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        public IDataResult<AdvertReadDto> GetAdvertDetailById(int id)
        {
            var data = _advertDal.GetAdvertDetailById(id);
            if (data is not null)
            {
                return new SuccessDataResult<AdvertReadDto>(data,AdvertMessages.AdvertGet);
            }

            return new ErrorDataResult<AdvertReadDto>(null,AdvertMessages.AdvertNotFound);
        }

        public IDataResult<List<AdvertReadDto>> GetAllAdvertByDistance(double latitude, double longitude,
            int pageNumber)
        {
            var data = _advertDal.GetAllAdvertByDistance(latitude, longitude, pageNumber);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertReadDto>>(data,AdvertMessages.AdvertGetAll);
            }

            return new ErrorDataResult<List<AdvertReadDto>>(null,AdvertMessages.AdvertsNotFound);
        }

        public IDataResult<List<AdvertReadDto>> GetAdvertDetailByUserId(int userId, int pageNumber)
        {
            var data = _advertDal.GetAllAdvertDetailsByFilter(a => a.UserId == userId, pageNumber);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertReadDto>>(data,AdvertMessages.AdvertGetAll);
            }

            return new ErrorDataResult<List<AdvertReadDto>>(null,AdvertMessages.AdvertsNotFound);
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
                return new SuccessDataResult<List<Advert>>(data,AdvertMessages.AdvertGetAll);
            }

            return new ErrorDataResult<List<Advert>>(null,AdvertMessages.AdvertsNotFound);
        }

        //[SecuredOperation($"{Role.SuperAdmin},{Role.Admin}")]
        public IResult UpdateAdvertCase(Advert advert)
        {
            _advertDal.Update(advert);
            return new SuccessResult(AdvertMessages.AdvertStatus);
        }

        //[SecuredOperation($"{Role.SuperAdmin},{Role.Admin}")]
        public IResult UpdateStatus(Advert advert)
        {
            _advertDal.Update(advert);
            return new SuccessResult(AdvertMessages.AdvertStatus);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [SecuredOperation($"{Role.AdvertImageAdd},{Role.User},{Role.SuperAdmin},{Role.Admin}")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetail")]
        [CacheRemoveAspect("IAdvertService.GetAdvertDetailById")]
        [CacheRemoveAspect("IAdvertService.GetAllAdvertDetailsByFilter")]
        [CacheRemoveAspect("IAdvertService.GetAll")]
        //  [ValidationAspect(typeof(AdvertValidator))]
        public async Task<IResult> Update(Advert advert, AdvertImage advertImage, IFormFile[] files, Location location)
        {
            var image = _imageService.GetByAdvertId(advert.Id);

            if (files is not null)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);

                for (int i = 0; i < files.Length; i++)
                {
                    var fileResult =
                        await fileHelper.UpdateAsync(files[i], image.Data[i].ImagePath, image.Data[i].PublicId);
                    var result = _imageService.Update(new AdvertImage
                    {
                        ImagePath = fileResult.Message.Split("&&")[0],
                        PublicId = fileResult.Message.Split("&&")[1],
                        AdvertId = advert.Id,
                        Id = image.Data[i].Id
                    });
                    if (!result.Success)
                    {
                        return new ErrorResult(AdvertMessages.AdvertUpdateFailed);
                    }
                }
            }

            _locationService.Update(location);

            _advertDal.Update(advert);

            return new SuccessResult(AdvertMessages.AdvertUpdate);
        }

        public IDataResult<List<AdvertReadDto>> GetAllAdvertDetailsByFilter(AdvertFilterDto filter, int pageNumber)
        {
            Expression<Func<Advert, bool>> filters = c => true;
            var properties = filter.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(filter, null);
                if (value is not null)
                {
                    if (value is int and not 0)
                    {
                        object[] methodParams = {filters, value};
                        filters = (Expression<Func<Advert, bool>>) GetInvokeMethod($"{property.Name}Condition",
                            methodParams);
                    }
                }
            }

            var data = _advertDal.GetAllAdvertDetailsByFilter(filters, pageNumber);
            if (data is not null)
            {
                return new SuccessDataResult<List<AdvertReadDto>>(data,AdvertMessages.AdvertGetAll);
            }

            return new ErrorDataResult<List<AdvertReadDto>>(null,AdvertMessages.AdvertsNotFound);
        }
    }
}