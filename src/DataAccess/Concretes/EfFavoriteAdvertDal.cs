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
                    join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals
                        animalCategory.Id
                    select new FavoriteAdvertReadDto
                    {
                        Id = favorite.Id,
                        AdvertId = favorite.AdvertId,
                        UserId = favorite.UserId,
                        Advert = advert,
                        User = user,
                        AdvertCategory = advertCategory,
                        AnimalSpecies = animalSpecies,
                        AnimalCategory = animalCategory,
                        Images = (from images in context.AdvertImages
                            where images.AdvertId == advert.Id
                            select images.ImagePath).ToArray()
                    };
                return result.ToList();
            }
        }
    }
}