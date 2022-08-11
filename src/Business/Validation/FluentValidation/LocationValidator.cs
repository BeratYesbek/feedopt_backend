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
            RuleFor(l => l.City).NotEmpty().MinimumLength(2);
            RuleFor(l => l.Country).NotEmpty().MinimumLength(2);
            RuleFor(l => l.Longitude).NotEmpty().NotNull();
            RuleFor(l => l.Latitude).NotEmpty().NotNull();
        }
    }
}