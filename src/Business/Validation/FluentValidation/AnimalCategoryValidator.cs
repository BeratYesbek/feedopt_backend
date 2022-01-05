﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Validation;
using Entity.concretes;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Business.Validation.FluentValidation
{
    public class AnimalCategoryValidator : AbstractValidator<AnimalCategory>
    {
        public AnimalCategoryValidator()
        {
            RuleFor(a => a.AnimalCategoryName).NotNull().NotEmpty().WithMessage("Camonnnnnnnnnnnnnn");
            RuleFor(a => a.AnimalCategoryName).MinimumLength(2).WithMessage("Camonnnnnnnnnnnnnn");
        }
    }
}