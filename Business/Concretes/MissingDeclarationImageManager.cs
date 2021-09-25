using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Business.Concretes
{
    public class MissingDeclarationImageManager : IMissingDeclarationImageService
    {
        private readonly EfMissingDeclarationImageDal missingDeclarationImageDal = new EfMissingDeclarationImageDal();

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

                //result.message contains image path
                missingDeclarationImage.ImagePath = result.Message;
                missingDeclarationImageDal.Add(missingDeclarationImage);
            }

            return new SuccessResult();
        }

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
                missingDeclarationImageDal.Update(missingDeclarationImage[i]);
            }

            return new SuccessResult();
        }

        public IResult Delete(MissingDeclarationImage[] missingDeclarationImage)
        {
            foreach (var image in missingDeclarationImage)
            {
                var result = FileHelper.Delete(image.ImagePath);
                missingDeclarationImageDal.Delete(image);
            }

            return new SuccessResult();
        }

        public IDataResult<List<MissingDeclarationImage>> GetByMissingDeclarationId(int id)
        {
            var data = missingDeclarationImageDal.GetAll(m => m.MissingDeclarationId == id);
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationImage>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationImage>>(null);
        }

        public IDataResult<MissingDeclarationImage> Get(int id)
        {
            var data = missingDeclarationImageDal.Get(m => m.AnimalImageId == id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclarationImage>(data);
            }

            return new ErrorDataResult<MissingDeclarationImage>(null);
        }

        public IDataResult<List<MissingDeclarationImage>> GetAll()
        {
            var data = missingDeclarationImageDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclarationImage>>(data);
            }

            return new ErrorDataResult<List<MissingDeclarationImage>>(null);
        }
    }
}