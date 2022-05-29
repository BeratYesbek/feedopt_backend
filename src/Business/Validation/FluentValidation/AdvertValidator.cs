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
            RuleFor(a => a.AgeId).NotEmpty().NotNull();
            RuleFor(a => a.AdvertCategoryId).NotEmpty().NotNull();
            RuleFor(a => a.AnimalCategoryId).NotEmpty().NotNull();
            RuleFor(a => a.AnimalSpeciesId).NotEmpty().NotNull();
            RuleFor(a => a.AnimalName).NotEmpty().NotNull();
            RuleFor(a => a.Gender).NotNull();
            RuleFor(a => a.UserId).NotEmpty().NotNull();
            RuleFor(a => a.Description).MinimumLength(20);
            RuleFor(a => a.Description).MaximumLength(4000);
            RuleFor(a => a.AnimalName).MinimumLength(2);
        }
    }
}