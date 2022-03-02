using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entity;
using Core.Utilities.Calculator;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfAdvertDal : EfEntityRepositoryBase<Advert, AppDbContext>, IAdvertDal
    {
        public List<AdvertReadDto> GetAllAdvertDetail(int pageNumber, double latitude, double longitude, int pageSize = 20)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals
                                 animalCategory.Id
                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages =
                                     (from image in context.AdvertImages where advert.Id == image.AdvertId select image)
                                     .ToArray(),
                                 Distance = (int) Calculator.CalculateDistance(latitude, longitude, Decimal.ToDouble(location.Latitude), Decimal.ToDouble(location.Longitude)),
                                 Id = advert.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 Age = advert.Age,
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 Gender = advert.Gender,
                                 AnimalName = advert.AnimalName,
                                 AdvertCategoryName = advertCategory.Name,
                                 AnimalCategoryName = animalCategory.AnimalCategoryName,
                                 Kind = animalSpecies.Kind,
                                 Latitude = location.Latitude,
                                 Longitude = location.Longitude,
                                 City = location.City,
                                 Country = location.Country,
                                 County = location.County,
                                 Images = (from image in context.AdvertImages
                                           where advert.Id == image.AdvertId
                                           select image.ImagePath).ToArray()

                             };

                return result.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
        }

        public List<AdvertReadDto> GetAllAdvertDetailsByFilter(Expression<Func<Advert, bool>> filter, int pageNumber, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public AdvertReadDto GetAdvertDetailById(int id)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts.Where(advert => advert.Id == id)
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals
                                 animalCategory.Id
                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages =
                                     (from image in context.AdvertImages where advert.Id == image.AdvertId select image)
                                     .ToArray(),

                                 Id = advert.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 Age = advert.Age,
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 Gender = advert.Gender,
                                 AnimalName = advert.AnimalName,
                                 AdvertCategoryName = advertCategory.Name,
                                 AnimalCategoryName = animalCategory.AnimalCategoryName,
                                 Kind = animalSpecies.Kind,
                                 Latitude = location.Latitude,
                                 Longitude = location.Longitude,
                                 City = location.City,
                                 Country = location.Country,
                                 County = location.County,
                                 Images = (from image in context.AdvertImages
                                           where advert.Id == image.AdvertId
                                           select image.ImagePath).ToArray()

                             };
                return result.First();
            }
        }
    }
}
