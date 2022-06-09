using Core.Entity.Concretes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {

        }
    }
}
