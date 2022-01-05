using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Entity;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class MissingDeclarationValidator : AbstractValidator<MissingDeclaration>
    {
        public MissingDeclarationValidator()
        {
            RuleFor(m => m.Description).NotEmpty().NotNull()
                .WithMessage(MissingDeclarationValidationMessages.MissingDeclarationEmptyDescriptionMessage);
            RuleFor(m => m.Description).MinimumLength(100)
                .WithMessage(MissingDeclarationValidationMessages.MissingDeclarationDescriptionLengthMessage);
            RuleFor(m => m.Description).MaximumLength(500)
                .WithMessage(MissingDeclarationValidationMessages.MissingDeclarationDescriptionLengthMessage);
            RuleFor(m => m.LocationId).NotEmpty().NotNull()
                .WithMessage(MissingDeclarationValidationMessages.MissingDeclarationEmptyLocationIdMessage);
            RuleFor(m => m.UserId).NotEmpty().NotNull()
                .WithMessage(MissingDeclarationValidationMessages.MissingDeclarationUserIdEmptyMessage);
        }
    }
}