using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Business.Concretes
{
    public class MissingDeclarationImageManager : IMissingDeclarationImageService
    {
        private readonly IMissingDeclarationImageDal _missingDeclarationImageDal;

        public MissingDeclarationImageManager(IMissingDeclarationImageDal missingDeclarationImageDal)
        {
            _missingDeclarationImageDal = missingDeclarationImageDal;
        }

        [CacheRemoveAspect("IMissingDeclarationImageService.GetAll")]
        [SecuredOperation("MissingDeclarationImage.Add,User")]
        [PerformanceAspect(5)]
        public IResult Add(MissingDeclarationImage missingDeclarationImage, IFormFile[] formFiles)
        {
            FileHelper.SetFileExtension("images", FileExtensions.ImageExtensions);

            foreach (var file in formFiles)
            {
                var result = FileHelper.Upload(file);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }

                // result.message contains image path
                missingDeclarationImage.ImagePath = result.Message;
                _missingDeclarationImageDal.Add(missingDeclarationImage);
                // always must assigned zero to missingDeclarationImageId after saved
                missingDeclarationImage.Id = 0;
            }

            return new SuccessResult();
        }

        [CacheRemoveAspect("IMissingDeclarationImageService.GetAll")]
        [SecuredOperation("MissingDeclarationImage.Update,User")]
        [PerformanceAspect(5)]
        public IResult Update(MissingDeclarationImage[] missingDeclarationImage, IFormFile[] formFiles)
        {
            FileHelper.SetFileExtension("images", FileExtensions.ImageExtensions);
            for (int i = 0; i < formFiles.Length; i++)
            {
                var result = FileHelper.Update(formFiles[i], missingDeclarationImage[i].ImagePath);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }

                //result.message contains image path
                missingDeclarationImage[i].ImagePath = result.Message;
                _missingDeclarationImageDal.Update(missingDeclarationImage[i]);
            }

            return new SuccessResult();
        }

        [CacheRemoveAspect("IMissingDeclarationImageService.GetAll")]
        [SecuredOperation("MissingDeclarationImage.Delete,User")]
        [PerformanceAspect(5)]
        public IResult Delete(MissingDeclarationImage[] missingDeclarationImage)
        {
            foreach (var image in missingDeclarationImage)
            {
                var result = FileHelper.Delete(image.ImagePath);
                _missingDeclarationImageDal.Delete(image);
            }

            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclarationImage.Get,User")]
        public IDataResult<List<MissingDeclarationImage>> GetByMissingDeclarationId(int id)
        {
            var data = _missingDeclarationImageDal.GetAll(m => m.MissingDeclarationId == id);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationImage>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationImage>>(null);
        }

        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclarationImage.Get,User")]
        public IDataResult<MissingDeclarationImage> Get(int id)
        {
            var data = _missingDeclarationImageDal.Get(m => m.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclarationImage>(data);
            }

            return new ErrorDataResult<MissingDeclarationImage>(null);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("MissingDeclarationImage.GetAll,User")]
        public IDataResult<List<MissingDeclarationImage>> GetAll()
        {
            var data = _missingDeclarationImageDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationImage>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationImage>>(null);
        }
    }
}