﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
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
using DataAccess.Concretes;
using Entity.concretes;
using Entity.Concretes;

namespace Business.Concretes
{
    public class AdoptionNoticeManager : IAdoptionNoticeService
    {
        private readonly IAdoptionNoticeDal _adoptionNoticeDal;
        private readonly IAdoptionNoticeImageService _adoptionNoticeImageService;
        public AdoptionNoticeManager(IAdoptionNoticeDal adoptionNoticeDal, IAdoptionNoticeImageService adoptionNoticeImageService)
        {
            _adoptionNoticeDal = adoptionNoticeDal;
            _adoptionNoticeImageService = adoptionNoticeImageService;
        }

        [LogAspect(typeof(FileLogger))]
        //[ValidationAspect(typeof(AdoptionNoticeValidator))]
        //[SecuredOperation("AdoptionNotice.Add,User")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        public IDataResult<AdoptionNotice> Add(AdoptionNotice adoptionNotice)
        {
            foreach (var file in adoptionNotice.FormFiles)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                var fileResult = fileHelper.Upload(file);
                if (fileResult.Success)
                {
                    var adoptionNoticeImage = new AdoptionNoticeImage();
                    adoptionNoticeImage.AdoptionNoticeId = 0;
                    adoptionNoticeImage.ImagePath = fileResult.Message.Split("&&")[0];
                    adoptionNoticeImage.PublicId = fileResult.Message.Split("&&")[1];
                    var result = _adoptionNoticeImageService.Add(adoptionNoticeImage);
                    if (!result.Success)
                    {
                        return new ErrorDataResult<AdoptionNotice>(null);
                    }
                }
            }
            return new SuccessDataResult<AdoptionNotice>(_adoptionNoticeDal.Add(adoptionNotice));
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [SecuredOperation("AdoptionNotice.Update,User")]
        [ValidationAspect(typeof(AdoptionNoticeValidator))]
        public IResult Update(AdoptionNotice adoptionNotice)
        {
            var adoptionNoticeImageList = _adoptionNoticeImageService.GetByAdoptionNoticeId(adoptionNotice.Id);
            if (adoptionNotice.FormFiles != null)
            {
                for (int i = 0; i < adoptionNotice.FormFiles.Count(); i++)
                {
                    var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                    var fileResult = fileHelper.Update(adoptionNotice.FormFiles[i], adoptionNoticeImageList.Data[i].ImagePath,
                        adoptionNoticeImageList.Data[i].PublicId);
                    var adoptionNoticeImage = new AdoptionNoticeImage();
                    adoptionNoticeImage.ImagePath = fileResult.Message.Split("&&")[0];
                    adoptionNoticeImage.PublicId = fileResult.Message.Split("&&")[1];
                    adoptionNoticeImage.Id = adoptionNoticeImageList.Data[0].Id;
                    adoptionNoticeImage.AdoptionNoticeId = adoptionNotice.Id;
                    var result = _adoptionNoticeImageService.Update(adoptionNoticeImage);
                    if (!result.Success)
                    {
                        return new ErrorResult();
                    }
                }
            }

            _adoptionNoticeDal.Update(adoptionNotice);

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IAdoptionNoticeService.GetAll")]
        [SecuredOperation("AdoptionNotice.Delete,User")]
        [PerformanceAspect(5)]
        public IResult Delete(AdoptionNotice adoptionNotice)
        {
            var imageList = _adoptionNoticeImageService.GetByAdoptionNoticeId(adoptionNotice.Id);
            for (int i = 0; i < imageList.Data.Count; i++)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                fileHelper.Delete(imageList.Data[i].ImagePath, imageList.Data[i].PublicId);
            }

            _adoptionNoticeDal.Delete(adoptionNotice);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AdoptionNotice.Get,User")]
        public IDataResult<AdoptionNotice> Get(int id)
        {
            var data = _adoptionNoticeDal.Get(a => a.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<AdoptionNotice>(data);
            }

            return new ErrorDataResult<AdoptionNotice>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("AdoptionNotice.GetAll,User")]
        [CacheAspect]
        public IDataResult<List<AdoptionNotice>> GetAll()
        {
            var data = _adoptionNoticeDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNotice>>(data);
            }

            return new ErrorDataResult<List<AdoptionNotice>>(null);
        }
    }
}