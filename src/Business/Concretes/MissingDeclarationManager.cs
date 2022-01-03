using System;
using System.Collections.Generic;
using System.Linq;
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
using Entity;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Concretes
{
    public class MissingDeclarationManager : IMissingDeclarationService
    {
        private readonly IMissingDeclarationDal _missingDeclarationDal;
        private readonly IMissingDeclarationImageService _missingDeclarationImageService;

        public MissingDeclarationManager(IMissingDeclarationDal missingDeclarationDal, IMissingDeclarationImageService missingDeclarationImageService)
        {
            _missingDeclarationDal = missingDeclarationDal;
            _missingDeclarationImageService = missingDeclarationImageService;
        }

        [ValidationAspect(typeof(MissingDeclarationValidator))]
        [CacheRemoveAspect("IMissingDeclarationService.GetAll")]
        [CacheRemoveAspect("IMissingDeclarationService.GetAllMissingDeclarationDetail")]
        [CacheRemoveAspect("IMissingDeclarationService.GetMissingDeclarationDetailById")]
        //[SecuredOperation("MissingDeclaration.Add,User")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<MissingDeclaration> Add(MissingDeclaration missingDeclaration)
        {
            var data = _missingDeclarationDal.Add(missingDeclaration);
            if (data == null)
            {
                return new ErrorDataResult<MissingDeclaration>(null);
            }
            foreach (var file in missingDeclaration.FormFiles)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                var fileResult = fileHelper.Upload(file);
                if (fileResult.Success)
                {
                    var missingDeclarationImage = new MissingDeclarationImage();
                    missingDeclaration.Id = 0;
                    missingDeclarationImage.ImagePath = fileResult.Message.Split("&&")[0];
                    missingDeclarationImage.PublicId = fileResult.Message.Split("&&")[1];
                    missingDeclarationImage.MissingDeclarationId = data.Id;
                    var result = _missingDeclarationImageService.Add(missingDeclarationImage);
                    if (!result.Success)
                    {
                        return new ErrorDataResult<MissingDeclaration>(null);
                    }
                }
            }
            return new SuccessDataResult<MissingDeclaration>(data);
        }

        [ValidationAspect(typeof(MissingDeclarationValidator))]
        [CacheRemoveAspect("IMissingDeclarationService.GetAll")]
        [CacheRemoveAspect("IMissingDeclarationService.GetAllMissingDeclarationDetail")]
        [CacheRemoveAspect("IMissingDeclarationService.GetMissingDeclarationDetailById")]
        [SecuredOperation("MissingDeclaration.Update,User")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IResult Update(MissingDeclaration missingDeclaration)
        {
            var missingDeclarationList = _missingDeclarationImageService.GetByMissingDeclarationId(missingDeclaration.Id);
            if (missingDeclaration.FormFiles != null)
            {
                for (int i = 0; i < missingDeclaration.FormFiles.Count(); i++)
                {
                    var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                    var fileResult = fileHelper.Update(missingDeclaration.FormFiles[i], missingDeclarationList.Data[i].ImagePath,
                        missingDeclarationList.Data[i].PublicId);
                    var missingDeclarationImage = new MissingDeclarationImage();
                    missingDeclarationImage.ImagePath = fileResult.Message.Split("&&")[0];
                    missingDeclarationImage.PublicId = fileResult.Message.Split("&&")[1];
                    missingDeclarationImage.Id = missingDeclarationList.Data[0].Id;
                    missingDeclarationImage.MissingDeclarationId = missingDeclaration.Id;
                    var result = _missingDeclarationImageService.Update(missingDeclarationImage);
                    if (!result.Success)
                    {
                        return new ErrorResult();
                    }
                }
            }
            _missingDeclarationDal.Update(missingDeclaration);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheRemoveAspect("IMissingDeclarationService.GetAll")]
        [CacheRemoveAspect("IMissingDeclarationService.GetAllMissingDeclarationDetail")]
        [CacheRemoveAspect("IMissingDeclarationService.GetMissingDeclarationDetailById")]
        [SecuredOperation("MissingDeclaration.Delete,User")]
        [LogAspect(typeof(FileLogger))]
        public IResult Delete(MissingDeclaration missingDeclaration)
        {
            var imageList = _missingDeclarationImageService.GetByMissingDeclarationId(missingDeclaration.Id);
            for (int i = 0; i < imageList.Data.Count; i++)
            {
                var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                fileHelper.Delete(imageList.Data[i].ImagePath, imageList.Data[i].PublicId);
            }

            _missingDeclarationDal.Delete(missingDeclaration);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclaration.Get,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<MissingDeclaration> Get(int id)
        {
            var data = _missingDeclarationDal.Get(m => m.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclaration>(data);
            }

            return new ErrorDataResult<MissingDeclaration>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclaration.GetAll,User")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<MissingDeclaration>> GetAll()
        {
            var data = _missingDeclarationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclaration>>(data);
            }

            return new ErrorDataResult<List<MissingDeclaration>>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclaration.GetAllAdoptionNoticeDetail,User")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<MissingDeclarationDto>> GetAllMissingDeclarationDetail()
        {
            var data = _missingDeclarationDal.GetAllMissingDeclarationsDetail();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationDto>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationDto>>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclaration.GetAdoptionNoticeDetailById,User")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<MissingDeclarationDto> GetMissingDeclarationDetailById(int id)
        {
            var data = _missingDeclarationDal.GetMissingDeclarationDetailById(id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclarationDto>(data);
            }

            return new ErrorDataResult<MissingDeclarationDto>(null);
        }
    }
}