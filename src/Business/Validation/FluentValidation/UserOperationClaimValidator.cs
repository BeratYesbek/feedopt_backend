using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Core.Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(u => u.OperationClaimId).NotEqual(0)
                .WithMessage(UserOperationClaimValidationMessages.UserOperationClaimsOperationIdZeroMessage);

            RuleFor(u => u.OperationClaimId).NotEmpty().NotNull()
                .WithMessage(UserOperationClaimValidationMessages.UserOperationClaimsOperationIdEmptyMessage);

            RuleFor(u => u.UserId).NotEmpty().NotNull().NotEqual(0)
                .WithMessage(UserOperationClaimValidationMessages.UserOperationClaimsUserIdEmptyMessage);
        }
    }
}