using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class AdvertCategoryValidator : AbstractValidator<AdvertCategory>
    {
        public AdvertCategoryValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.Name).MinimumLength(2);
        }
    }
}