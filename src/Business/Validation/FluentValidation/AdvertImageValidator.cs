using System.ComponentModel.DataAnnotations;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class AdvertImageValidator : AbstractValidator<AdvertImage>
    {
        public AdvertImageValidator()
        {
            RuleFor(a => a.ImagePath).NotEmpty().NotNull();
            RuleFor(a => a.AdvertId).NotEmpty().NotNull();
        }
    }
}