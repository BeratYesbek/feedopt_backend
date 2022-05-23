using Core.DataAccess;
using Core.Entity.Abstracts;
using DataAccess.Abstracts;
using DataAccess.Extensions;
using Entity;
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataAccess.Concretes
{
    public class EfFilterDal : EfEntityRepositoryBase<Filter, AppDbContext>, IFilterDal
    {
        public List<FilterDto> GetByFilterType(Expression<Func<Filter, bool>> filter)
        {
            using (var context = new AppDbContext())
            {
                var result = from filterData in context.Filters.Where(filter)

                             select new FilterDto
                             {
                                 Id = filterData.Id,
                                 FilterType = filterData.FilterType,
                                 InputType = filterData.InputType,
                                 Label = filterData.Label,
                                 Type = filterData.Type,
                                 Data = (object) filterData.DataType == "Color"
                                 ? context.Colors.ToList() : filterData.DataType == "Age" 
                                 ? context.Ages.ToList() : filterData.DataType == "AnimalCategory"
                                 ? context.AnimalCategories.ToList() : filterData.DataType == "AnimalSpecies"
                                 ? context.AnimalSpecies.ToList()  : filterData.DataType == "AdvertCategory"
                                 ? context.AdvertCategories.ToList() : Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList()
                             };
                return result.ToList();
            }
        }

  
    }

}
