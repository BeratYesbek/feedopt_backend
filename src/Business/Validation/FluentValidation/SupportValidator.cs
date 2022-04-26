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
    public class SupportValidator : AbstractValidator<Ticket>
    {
        public SupportValidator()
        {
            RuleFor(t => t.Description).NotEmpty();

            RuleFor(t => t.Description).MinimumLength(150).MaximumLength(600);

            RuleFor(t => t.Title).NotEmpty();

            RuleFor(t => t.Title).MinimumLength(10).MaximumLength(35);

            RuleFor(t => t.UserId).NotEmpty();
        }
    }
}
