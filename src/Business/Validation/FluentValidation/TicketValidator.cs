using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(t => t.Description).NotEmpty().WithMessage("");
            RuleFor(t => t.Description).MinimumLength(150).MaximumLength(600).WithMessage("");
            RuleFor(t => t.Title).NotEmpty().WithMessage("");
            RuleFor(t => t.Title).MinimumLength(10).MaximumLength(35).WithMessage("");
            RuleFor(t => t.UserId).NotEmpty().WithMessage("");
        }
    }
}
