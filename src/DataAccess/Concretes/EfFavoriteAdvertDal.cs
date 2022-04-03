using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfFavoriteAdvertDal : EfEntityRepositoryBase<FavoriteAdvert, AppDbContext>, IFavoriteAdvertDal
    {
        public List<FavoriteAdvertReadDto> GetAllDetailByFilter(Expression<Func<FavoriteAdvert, bool>> filter)
        {
            using (var context = new AppDbContext())
            {
                var result = from favorite in context.FavoriteAdverts.Where(filter)
                             join advert in context.Adverts on favorite.AdvertId equals advert.Id
                             join user in context.Users on favorite.UserId equals user.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals animalCategory.Id
                             join location in context.Locations on advert.LocationId equals location.Id
                             join age in context.Ages on advert.AgeId equals age.Id
                             select new FavoriteAdvertReadDto
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
                                 Age = age,
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
                                           select image.ImagePath).ToArray(),
                                 CreatedAt = advert.CreatedAt,
                                 UpdatedAt = advert.UpdatedAt,
                             };
                return result.ToList();
            }
        }
    }
}