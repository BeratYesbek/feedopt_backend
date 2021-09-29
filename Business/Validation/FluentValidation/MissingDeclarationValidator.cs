using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class MissingDeclarationValidator : AbstractValidator<MissingDeclaration>
    {
        public MissingDeclarationValidator()
        {
            RuleFor(m => m.Description).NotEmpty().NotNull();
            RuleFor(m => m.Description).MinimumLength(100);
            RuleFor(m => m.Description).MaximumLength(500);
            RuleFor(m => m.LocationId).NotEmpty().NotNull();
            RuleFor(m => m.AnimalSpeciesId).NotEmpty().NotNull();
            RuleFor(m => m.UserId).NotEmpty().NotNull();
        }
    }
}