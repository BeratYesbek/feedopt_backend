using System;
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
    public class AdvertImageManager : IAdvertImageService
    {
        private readonly IAdvertImageDal _advertImageDal;

        public AdvertImageManager(IAdvertImageDal advertImageDal)
        {
            _advertImageDal = advertImageDal;
        }


        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeImageService.GetByAdoptionNoticeId")]
        [SecuredOperation("AdoptionNotice.Add,User")]
        public IResult Add(AdvertImage image)
        {
            _advertImageDal.Add(image);

            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheRemoveAspect("IAdoptionNoticeImageService.GetByAdoptionNoticeId")]
        [SecuredOperation("AdoptionNotice.Delete,User")]
        public IResult Delete(AdvertImage image)
        {
            _advertImageDal.Delete(image);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AdoptionNotice.Get,User")]
        public IDataResult<AdvertImage> Get(int id)
        {
            var data = _advertImageDal.Get(a => a.Id == id);

            if (data is not null)
            {
                return new SuccessDataResult<AdvertImage>(data);
            }

            return new ErrorDataResult<AdvertImage>(null);
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("AdoptionNotice.GetAll,User")]
        public IDataResult<List<AdvertImage>> GetAll()
        {
            var data = _advertImageDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertImage>>(data);
            }

            return new ErrorDataResult<List<AdvertImage>>(null);
        }

        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        [CacheAspect]
        [SecuredOperation("AdoptionNotice.Get,User")]
        public IDataResult<List<AdvertImage>> GetByAdvertId(int id)
        {
            var data = _advertImageDal.GetAll(a => a.AdvertId == id);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AdvertImage>>(data);
            }

            return new ErrorDataResult<List<AdvertImage>>(null);
        }

        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IAdoptionNoticeImageService.GetByAdoptionNoticeId")]
        [SecuredOperation("AdoptionNotice.Update,User")]
        public IResult Update(AdvertImage image)
        {
            _advertImageDal.Update(image);
            return new SuccessResult();
        }
    }
}

/*Image image = ImageScaling.ResizeImage(Image.FromStream(file.OpenReadStream(), true, true),
      ImageScaling.ImageWidth, ImageScaling.ImageHeight);*/







/* Image image = ImageScaling.ResizeImage(Image.FromStream(formFiles[i].OpenReadStream(), true, true),
          ImageScaling.ImageWidth, ImageScaling.ImageHeight);*/
//  var result = _cloudinaryService.Update(formFiles[i], adoptionNoticeImage[i].PublicId, image);