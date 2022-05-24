using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfOptionDal : IOptionDal
    {
        public OptionDto GetOptions()
        {
            using (var context = new AppDbContext())
            {
                var result = new OptionDto
                {
                    Color = (from color in context.Colors select color).ToList(),
                    AdvertCategory = (from advertCategory in context.AdvertCategories select advertCategory).ToList(),
                    AnimalCategory = (from animalCategory in context.AnimalCategories select animalCategory).ToList(),
                    Age = (from age in context.Ages select age).ToList(),
                    Gender = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList()
                };
                return result;
            }

        }
    }
}
