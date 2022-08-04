using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess;
using Core.Utilities.Calculator;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfFavoriteAdvertDal : EfEntityRepositoryBase<FavoriteAdvert, AppDbContext>, IFavoriteAdvertDal
    {
        public List<FavoriteAdvertReadDto> GetAllDetailByFilter(Expression<Func<FavoriteAdvert, bool>> filter,int userId, double latitude, double longitude)
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
                             join color in context.Colors on advert.ColorId equals color.Id
                             select new FavoriteAdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AgeId =advert.AgeId,
                                 AnimalCategory = animalCategory,
                                 AdvertImages = (from image in context.AdvertImages where advert.Id == image.AdvertId select image).ToArray(),
                                 Id = favorite.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 Age = age,
                                 AdvertId = advert.Id,
                                 LocationId = advert.LocationId,
                                 ColorId = advert.ColorId,
                                 Color = color,
                                 Distance = (int) Calculator.CalculateDistance(latitude, longitude, location.Latitude,location.Longitude),
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 AnimalCategoryId = advert.AnimalCategoryId,
                                 FavoriteStatus = (from favorite in context.FavoriteAdverts where advert.Id == favorite.AdvertId && userId == favorite.UserId select favorite).Any(),
                                 Gender = advert.Gender,
                                 AnimalName = advert.AnimalName,
                                 Images = (from image in context.AdvertImages where advert.Id == image.AdvertId select image.ImagePath).ToArray(),
                                 CreatedAt = advert.CreatedAt,
                                 UpdatedAt = advert.UpdatedAt,
                             };
                return result.OrderByDescending(t => t.Id).ToList();
            }
        }
    }
}