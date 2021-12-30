﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;


namespace Business.Concretes
{
    public class AdoptionNoticeImageManager : IAdoptionNoticeImageService
    {
        private readonly IAdoptionNoticeImageDal _adoptionNoticeImageDal;
        private readonly ICloudinaryService _cloudinaryService;

        public AdoptionNoticeImageManager(IAdoptionNoticeImageDal adoptionNoticeImageDal,
            ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
            _adoptionNoticeImageDal = adoptionNoticeImageDal;
        }

        // dfds

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeImageService.GetByAdoptionNoticeId")]
       // [SecuredOperation("AdoptionNotice.Add,User")]
        public IResult Add(AdoptionNoticeImage adoptionNoticeImage, IFormFile[] formFiles)
        {
            // you can set your want to give extension 
            FileHelper.SetFileExtension("images", FileExtensions.ImageExtensions);
            // inside of foreach is scaling and streaming our file in wwwroot

            foreach (var file in formFiles)
            {
                Image image = ImageScaling.ResizeImage(Image.FromStream(file.OpenReadStream(), true, true),
                    ImageScaling.ImageWidth, ImageScaling.ImageHeight);
                var result = _cloudinaryService.Upload(file, image);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }

                //result.message contains image path
                adoptionNoticeImage.Id = 0;
                adoptionNoticeImage.ImagePath = result.Message;
                _adoptionNoticeImageDal.Add(adoptionNoticeImage);
            }

            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeImageService.GetByAdoptionNoticeId")]
        [SecuredOperation("AdoptionNotice.Delete,User")]
        public IResult Delete(AdoptionNoticeImage[] adoptionNoticeImages)
        {
            // delete file in wwwroot and adoptionNoticeImages table
            foreach (var image in adoptionNoticeImages)
            {
                var result = _cloudinaryService.Delete(image.PublicId);
                _adoptionNoticeImageDal.Delete(image);
            }

            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AdoptionNotice.Get,User")]
        public IDataResult<AdoptionNoticeImage> Get(int id)
        {
            var data = _adoptionNoticeImageDal.Get(a => a.Id == id);

            if (data != null)
            {
                return new SuccessDataResult<AdoptionNoticeImage>(data);
            }

            return new ErrorDataResult<AdoptionNoticeImage>(null);
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("AdoptionNotice.GetAll,User")]
        public IDataResult<List<AdoptionNoticeImage>> GetAll()
        {
            var data = _adoptionNoticeImageDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNoticeImage>>(data);
            }

            return new ErrorDataResult<List<AdoptionNoticeImage>>(null);
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AdoptionNotice.Get,User")]
        public IDataResult<List<AdoptionNoticeImage>> GetByAdoptionNoticeId(int id)
        {
            var data = _adoptionNoticeImageDal.GetAll(a => a.AdoptionNoticeId == id);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNoticeImage>>(data);
            }

            return new ErrorDataResult<List<AdoptionNoticeImage>>(null);
        }

        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IAdoptionNoticeImageService.GetByAdoptionNoticeId")]
        [SecuredOperation("AdoptionNotice.Update,User")]
        public IResult Update(AdoptionNoticeImage[] adoptionNoticeImage, IFormFile[] formFiles)
        {
            // you can set file extension what you give
            FileHelper.SetFileExtension("images", FileExtensions.ImageExtensions);
            for (int i = 0; i < formFiles.Length; i++)
            {
                //after this method exchange our files with old files before updating, update table 
                Image image = ImageScaling.ResizeImage(Image.FromStream(formFiles[i].OpenReadStream(), true, true),
                    ImageScaling.ImageWidth, ImageScaling.ImageHeight);
                var result = _cloudinaryService.Update(formFiles[i], adoptionNoticeImage[i].PublicId, image);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }

                //result.message contains image path
                adoptionNoticeImage[i].ImagePath = result.Message;
                _adoptionNoticeImageDal.Update(adoptionNoticeImage[i]);
            }

            return new SuccessResult();
        }
    }
}