using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Entity.concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class AdoptionNoticeValidator : AbstractValidator<AdoptionNotice>
    {
        public AdoptionNoticeValidator()
        {
            RuleFor(a => a.LocationId).NotEmpty().NotNull()
                .WithMessage(AdoptionNoticeValidationMessages.AdoptionNoticeEmptyLocationIdMessage);

            RuleFor(a => a.UserId).NotEmpty().NotNull()
                .WithMessage(AdoptionNoticeValidationMessages.AdoptionNoticeUserIdEmptyMessage);

            RuleFor(a => a.Description).NotEmpty()
                .WithMessage(AdoptionNoticeValidationMessages.AdoptionNoticeEmptyDescriptionMessage);

            RuleFor(a => a.Description).MinimumLength(100)
                .WithMessage(AdoptionNoticeValidationMessages.AdoptionNoticeDescriptionLengthMessage);

            RuleFor(a => a.Description).MaximumLength(500)
                .WithMessage(AdoptionNoticeValidationMessages.AdoptionNoticeDescriptionLengthMessage);
        }
    }
}