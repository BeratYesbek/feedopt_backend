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
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Business.Concretes
{
    public class AdoptionNoticeImageManager : IAdoptionNoticeImageService
    {
        private readonly EfAdoptionNoticeImageDal adoptionNoticeImageDal = new EfAdoptionNoticeImageDal();

        public IResult Add(AdoptionNoticeImage adoptionNoticeImage, IFormFile[] formFiles)
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
                adoptionNoticeImage.ImagePath = result.Message;
                adoptionNoticeImageDal.Add(adoptionNoticeImage);
            }

            return new SuccessResult();
        }

        public IResult Delete(AdoptionNoticeImage[] adoptionNoticeImages)
        {
            foreach (var image in adoptionNoticeImages)
            {
                var result = FileHelper.Delete(image.ImagePath);
                adoptionNoticeImageDal.Delete(image);
            }

            return new SuccessResult();
        }

        public IDataResult<AdoptionNoticeImage> Get(int id)
        {
            var data = adoptionNoticeImageDal.Get(a => a.AdoptionNoticeImageId == id);

            if (data != null)
            {
                return new SuccessDataResult<AdoptionNoticeImage>(data);
            }

            return new ErrorDataResult<AdoptionNoticeImage>(null);
        }

        public IDataResult<List<AdoptionNoticeImage>> GetAll()
        {
            var data = adoptionNoticeImageDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNoticeImage>>(data);
            }

            return new ErrorDataResult<List<AdoptionNoticeImage>>(null);
        }

        public IDataResult<List<AdoptionNoticeImage>> GetByAdoptionNoticeId(int id)
        {
            var data = adoptionNoticeImageDal.GetAll(a => a.AdoptionNoticeId == id);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdoptionNoticeImage>>(data);
            }

            return new ErrorDataResult<List<AdoptionNoticeImage>>(null);
        }

        public IResult Update(AdoptionNoticeImage[] adoptionNoticeImage, IFormFile[] formFiles)
        {
            FileHelper.SetFileExtension("images", FileExtensions.ImageExtensions);
            for (int i = 0; i < formFiles.Length; i++)
            {
                var result = FileHelper.Update(formFiles[i], adoptionNoticeImage[i].ImagePath);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }

                //result.message contains image path
                adoptionNoticeImage[i].ImagePath = result.Message;
                adoptionNoticeImageDal.Update(adoptionNoticeImage[i]);
            }

            return new SuccessResult();
        }
    }
}