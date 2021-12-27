using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(l => l.Address).NotEmpty().NotNull();
            RuleFor(l => l.Address).MinimumLength(10);
            RuleFor(l => l.City).NotEmpty().MinimumLength(2);
            RuleFor(l => l.Country).NotEmpty().MinimumLength(2);
            RuleFor(l => l.PlaceId).NotNull().NotEmpty();
            RuleFor(l => l.Longitude).NotEmpty().NotNull();
        }
    }
}