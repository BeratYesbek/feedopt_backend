﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
        public List<AdvertReadDto> GetAllAdvertDetail(int pageNumber, double latitude, double longitude, double diameter, int pageSize = 20)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join age in context.Ages on advert.AgeId equals age.Id
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
                                 Distance = (int)Calculator.CalculateDistance(latitude, longitude, Decimal.ToDouble(location.Latitude), Decimal.ToDouble(location.Longitude)),
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

                return result.OrderByDescending(t => t.Id).Where(t => t.Distance <= diameter).Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
        }




        public List<AdvertReadDto> GetAllAdvertDetailsByFilter(Expression<Func<Advert, bool>> filter, int pageNumber, int pageSize = 10)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts.Where(filter)
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory
                                 .Id
                             join user in context.Users on advert.UserId equals user.Id
                             join age in context.Ages on advert.AgeId equals age.Id
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


                return result.OrderByDescending(a => a.Id).Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
        }

        public List<AdvertReadDto> GetAllAdvertByDistance(double latitude, double longitude, int pageNumber, double diameter = 30, int pageSize = 10)
        {
            using (var context = new AppDbContext())
            {
                var result = from advert in context.Adverts
                             join location in context.Locations on advert.LocationId equals location.Id
                             join animalSpecies in context.AnimalSpecies on advert.AnimalSpeciesId equals animalSpecies.Id
                             join advertCategory in context.AdvertCategories on advert.AdvertCategoryId equals advertCategory.Id
                             join user in context.Users on advert.UserId equals user.Id
                             join age in context.Ages on advert.AgeId equals age.Id
                             join animalCategory in context.AnimalCategories on animalSpecies.AnimalCategoryId equals
                                 animalCategory.Id
                           //  where Calculator.CalculateDistance(latitude, longitude, Decimal.ToDouble(location.Latitude),
                              //   Decimal.ToDouble(location.Longitude)) <= 30
                             
                             select new AdvertReadDto
                             {
                                 Location = location,
                                 AnimalSpecies = animalSpecies,
                                 AdvertCategory = advertCategory,
                                 User = user,
                                 AdvertImages =
                            (from image in context.AdvertImages where advert.Id == image.AdvertId select image)
                            .ToArray(),
                                 Distance = (int)Calculator.CalculateDistance(latitude, longitude,
                            Decimal.ToDouble(location.Latitude), Decimal.ToDouble(location.Longitude)),
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

                return result.OrderByDescending(t => t.Id)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize).ToList();
            }
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
                             join age in context.Ages on advert.AgeId equals age.Id
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
                                 // Distance = (int)Calculator.CalculateDistance(latitude, longitude, Decimal.ToDouble(location.Latitude), Decimal.ToDouble(location.Longitude)),
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
                return result.First();
            }
        }
    }
}
