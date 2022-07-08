using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using Core.DataAccess;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.Calculator;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfAdvertDal : EfEntityRepositoryBase<Advert, AppDbContext>, IAdvertDal
    {
        public List<AdvertReadDto> GetAllAdvertDetail(int pageNumber, double latitude, double longitude, int userId, double diameter, int pageSize = 20)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts.Where(x => x.IsDeleted != true)
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join color in context.Colors on advert.ColorId equals color.Id
                             join age in context.Ages on advert.AgeId equals age.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals animalCategory.Id

                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image).ToArray(),
                                 Distance = (int)Calculator.CalculateDistance(latitude, longitude, location.Latitude, location.Longitude),
                                 Id = advert.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 AnimalCategoryId = advert.AnimalCategoryId,
                                 LocationId = advert.LocationId,
                                 ColorId = advert.ColorId,
                                 AnimalName = advert.AnimalName,
                                 AgeId = advert.AgeId,
                                 Age = age,
                                 Color = color,
                                 FavoriteAdvert = (from favorite in context.FavoriteAdverts where advert.Id == favorite.AdvertId && userId == favorite.UserId select favorite).FirstOrDefault(),
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 Gender = advert.Gender,
                                 AnimalCategory = animalCategory,
                                 Images = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image.ImagePath).ToArray(),
                                 CreatedAt = advert.CreatedAt,
                                 UpdatedAt = advert.UpdatedAt,

                             };

                //return result.OrderByDescending(t => t.Id).Where(t => t.Distance <= diameter).Skip(pageNumber * pageSize).Take(pageSize).ToList();
                return result.OrderByDescending(t => t.Id).Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
        }

        public List<AdvertReadDto> GetAllAdvertDetailsByFilter(Expression<Func<Advert, bool>> filter, int userId, double latitude, double longitude, int pageNumber, int distance = 100000, int pageSize = 20)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts.OrderByDescending(x => x.Id).Where(filter).Where(x => x.IsDeleted != true)
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join age in context.Ages on advert.AgeId equals age.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals animalCategory.Id
                             join color in context.Colors on advert.ColorId equals color.Id
                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image).ToArray(),
                                 Distance = (int)Calculator.CalculateDistance(latitude, longitude, location.Latitude, location.Longitude),
                                 Id = advert.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 AnimalCategoryId = advert.AnimalCategoryId,
                                 LocationId = advert.LocationId,
                                 ColorId = advert.ColorId,
                                 AnimalName = advert.AnimalName,
                                 AgeId = advert.AgeId,
                                 Age = age,
                                 Color = color,
                                 FavoriteAdvert = (from favorite in context.FavoriteAdverts where advert.Id == favorite.AdvertId && userId == favorite.UserId select favorite).FirstOrDefault(),
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 Gender = advert.Gender,
                                 AnimalCategory = animalCategory,
                                 Images = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image.ImagePath).ToArray(),
                                 CreatedAt = advert.CreatedAt,
                                 UpdatedAt = advert.UpdatedAt,

                             };

                var list = result.ToList().Where(t => t.Distance < distance);
                return list.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
        }

        public List<AdvertReadDto> GetAllAdvertByDistance(double latitude, double longitude, int userId, int pageNumber, double diameter = 30, int pageSize = 10)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join age in context.Ages on advert.AgeId equals age.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals animalCategory.Id
                             join color in context.Colors on advert.ColorId equals color.Id

                             //  where Calculator.CalculateDistance(latitude, longitude, Decimal.ToDouble(location.Latitude),
                             //   Decimal.ToDouble(location.Longitude)) <= 30

                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image).ToArray(),
                                 Distance = (int)Calculator.CalculateDistance(latitude, longitude, location.Latitude, location.Longitude),
                                 Id = advert.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 AnimalCategoryId = advert.AnimalCategoryId,
                                 LocationId = advert.LocationId,
                                 ColorId = advert.ColorId,
                                 AnimalName = advert.AnimalName,
                                 AgeId = advert.AgeId,
                                 Age = age,
                                 Color = color,
                                 FavoriteAdvert = (from favorite in context.FavoriteAdverts where advert.Id == favorite.AdvertId && userId == favorite.UserId select favorite).FirstOrDefault(),
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 Gender = advert.Gender,
                                 AnimalCategory = animalCategory,
                                 Images = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image.ImagePath).ToArray(),
                                 CreatedAt = advert.CreatedAt,
                                 UpdatedAt = advert.UpdatedAt,

                             };

                return result.OrderByDescending(t => t.Id).Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
        }

        public AdvertReadDto GetAdvertDetailById(int id, int userId, double latitude, double longitude)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts.Where(advert => advert.Id == id && advert.IsDeleted != true)
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join age in context.Ages on advert.AgeId equals age.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals animalCategory.Id
                             join color in context.Colors on advert.ColorId equals color.Id
                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image).ToArray(),
                                 Distance = (int)Calculator.CalculateDistance(latitude, longitude, location.Latitude, location.Longitude),
                                 Id = advert.Id,
                                 AdvertCategoryId = advert.AdvertCategoryId,
                                 AnimalSpeciesId = advert.AnimalSpeciesId,
                                 AnimalCategoryId = advert.AnimalCategoryId,
                                 LocationId = advert.LocationId,
                                 ColorId = advert.ColorId,
                                 AnimalName = advert.AnimalName,
                                 AgeId = advert.AgeId,
                                 Age = age,
                                 Color = color,
                                 FavoriteAdvert = (from favorite in context.FavoriteAdverts where advert.Id == favorite.AdvertId && userId == favorite.UserId select favorite).FirstOrDefault(),
                                 Description = advert.Description,
                                 UserId = advert.UserId,
                                 Gender = advert.Gender,
                                 AnimalCategory = animalCategory,
                                 Images = (from image in context.AdvertImages where advert.Id == image.AdvertId && !advert.IsDeleted select image.ImagePath).ToArray(),
                                 CreatedAt = advert.CreatedAt,
                                 UpdatedAt = advert.UpdatedAt,

                             };
                return result.FirstOrDefault();
            }
        }

        public AdvertEditDto Edit(int id)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts.Where(t => t.Id == id)
                    join location in context.Locations on advert.LocationId equals location.Id
                    join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                    join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                    join age in context.Ages on advert.AgeId equals age.Id
                    join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals animalCategory.Id
                    join color in context.Colors on advert.ColorId equals color.Id
                             select new AdvertEditDto
                    {
                        Advert = advert,
                        Color = color,
                        Age = age,
                        AnimalCategory = animalCategory,
                        AdvertCategory = advertCategory,
                        Location = location,
                        AnimalSpecies = animalSpecies,
                        AdvertImage = (from image in context.AdvertImages where image.AdvertId == advert.Id && !image.IsDeleted select image).ToArray()
                    };
                return result.FirstOrDefault();
            }
        }
    }
}
