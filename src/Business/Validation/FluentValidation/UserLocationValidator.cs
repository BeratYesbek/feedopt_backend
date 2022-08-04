using System.Data;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation;

public class UserLocationValidator : AbstractValidator<UserLocation>
{
    public UserLocationValidator()
    {
        RuleFor(t => t.UserId).NotEmpty().NotNull();
        RuleFor(t => t.Latitude).NotEmpty().NotNull();
        RuleFor(t => t.Longitude).NotEmpty().NotNull();
    }
}