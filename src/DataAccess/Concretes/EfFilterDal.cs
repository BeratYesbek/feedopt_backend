using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace DataAccess.Concretes
{
    public class EfFilterDal : EfEntityRepositoryBase<Filter, AppDbContext>, IFilterDal
    {
        public List<FilterDto> GetByFilterType(Expression<Func<Filter, bool>> filter)
        {

            using (var context = new AppDbContext())
            {
                object genders = new object[]
                {
                    new {Gender=Gender.Male,Name="Erkek",Id=Gender.Male},
                    new {Gender = Gender.Female,Name="Kadın",Id=Gender.Female},
                };
                var result = from filterData in context.Filters.Where(filter)
                             select new FilterDto
                             {
                                 Id = filterData.Id,
                                 FilterType = filterData.FilterType,
                                 InputType = filterData.InputType,
                                 Label = filterData.Label,
                                 Type = filterData.Type,
                                 Data = (object)filterData.DataType == "Color"
                                 ? context.Colors.ToList() : filterData.DataType == "Age"
                                 ? (from age in context.Ages select new AgeRangesReadDto { Id = age.Id, Name = age.AgeRange }).ToList() : filterData.DataType == "AnimalCategory"
                                 ? (from animalCategory in context.AnimalCategories select new AnimalCategoryReadDto { Id = animalCategory.Id, Name = animalCategory.AnimalCategoryName }).ToList() : filterData.DataType == "AnimalSpecies"
                                 ? (from animalSpecies in context.AnimalSpecies select new AnimalSpeciesReadDto { Id = animalSpecies.Id, AnimalCategoryId = animalSpecies.AnimalCategoryId, Name = animalSpecies.Kind }) : filterData.DataType == "AdvertCategory"
                                 ? context.AdvertCategories.ToList() : genders
                             };
                return result.ToList();
            }
        }


    }

}
