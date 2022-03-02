using System.Data;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class AdvertValidator : AbstractValidator<Advert>
    {
        public AdvertValidator()
        {
            RuleFor(a => a.Description).NotEmpty().NotNull();
            RuleFor(a => a.Age).NotEmpty().NotNull();
            RuleFor(a => a.AdvertCategoryId).NotEmpty().NotNull();
            RuleFor(a => a.AnimalSpeciesId).NotEmpty().NotNull();
            RuleFor(a => a.AnimalName).NotEmpty().NotNull();
            RuleFor(a => a.Gender).NotEmpty().NotNull();
            RuleFor(a => a.UserId).NotEmpty().NotNull();

            RuleFor(a => a.Description).MinimumLength(150);
            RuleFor(a => a.Description).MaximumLength(2000);
            RuleFor(a => a.AnimalName).MinimumLength(2);
            RuleFor(a => a.Age).LessThan(25);
        }
    }
}