using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.Hex).NotEmpty().NotNull();
            RuleFor(c => c.Name).NotEmpty().NotNull();
        }
    }
}
