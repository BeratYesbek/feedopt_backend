using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class AdoptionNoticeValidator : AbstractValidator<AdoptionNotice>
    {
        public AdoptionNoticeValidator()
        {
            RuleFor(a => a.LocationId).NotEmpty().NotNull();
            RuleFor(a => a.AnimalId).NotEmpty().NotNull();
            RuleFor(a => a.UserId).NotEmpty().NotNull();
            RuleFor(a => a.Description).NotEmpty();
            RuleFor(a => a.Description).MinimumLength(100);
            RuleFor(a => a.Description).MaximumLength(500);
        }
    }
}