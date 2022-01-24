using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class SupportValidator : AbstractValidator<Support>
    {
        public SupportValidator()
        {
            RuleFor(t => t.Description).NotEmpty()
                .WithMessage(TicketValidationMessages.TicketEmptyDescriptionMessage);

            RuleFor(t => t.Description).MinimumLength(150).MaximumLength(600)
                .WithMessage(TicketValidationMessages.TicketDescriptionLengthMessage);

            RuleFor(t => t.Title).NotEmpty()
                .WithMessage(TicketValidationMessages.TicketEmptyTitleMessage);

            RuleFor(t => t.Title).MinimumLength(10).MaximumLength(35)
                .WithMessage(TicketValidationMessages.TicketTitleLengthMessage);

            RuleFor(t => t.UserId).NotEmpty()
                .WithMessage(TicketValidationMessages.TicketUserIdEmptyMessage);
        }
    }
}
