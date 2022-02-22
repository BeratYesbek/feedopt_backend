using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.concretes;
using Entity.Concretes;

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

        public async Task<IDataResult<Advert>> Add(Advert advert, AdvertImage advertImage, Location location)
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

                foreach (var file in advertImage.Files)
                {
                    var fileHelper = new FileHelper(RecordType.Cloud, FileExtension.ImageExtension);
                    var fileResult = fileHelper.Upload(file);
                    if (fileResult.Success)
                    {
                        var adoptionNoticeImage = new AdoptionNoticeImage();
                        adoptionNoticeImage.Id = 0;
                        adoptionNoticeImage.ImagePath = fileResult.Message.Split("&&")[0];
                        adoptionNoticeImage.PublicId = fileResult.Message.Split("&&")[1];
                        adoptionNoticeImage.AdoptionNoticeId = data.Id;
                        var result = _adoptionNoticeImageService.Add(adoptionNoticeImage);
                        if (!result.Success)
                        {
                            return new ErrorDataResult<AdoptionNotice>(null);
                        }
                    }
                }

            }
        }

        public Task<IResult> Delete(Advert advert)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Advert> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Advert>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(Advert advert, AdvertImage advertImage, Location location)
        {
            throw new NotImplementedException();
        }
    }
}
