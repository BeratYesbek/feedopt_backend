using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using System.Linq;

namespace DataAccess.Concretes
{
    public class EfOptionDal : IOptionDal
    {
        public OptionDto GetOptions()
        {

            using (var context = new AppDbContext())
            {
                object[] genders = new object[]
                {
                    new {Gender=Gender.Male,Name="Erkek",Id=Gender.Male},
                    new {Gender = Gender.Female,Name="Kadın",Id=Gender.Female},
                };
                var result = new OptionDto
                {
                    Color = (from color in context.Colors select color).ToList(),
                    AdvertCategory = (from advertCategory in context.AdvertCategories select advertCategory).ToList(),
                    AnimalCategory = (from animalCategory in context.AnimalCategories select new AnimalCategoryReadDto { Id = animalCategory.Id, Name = animalCategory.AnimalCategoryName }).ToList(),
                    Age = (from age in context.Ages select new AgeRangesReadDto { Id = age.Id, Name = age.AgeRange }).ToList(),
                    Gender = genders
                };
                return result;
            }

        }
    }
}
