﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Entity.concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class AnimalSpeciesValidator : AbstractValidator<AnimalSpecies>
    {
        public AnimalSpeciesValidator()
        {
            RuleFor(a => a.AnimalCategoryId).NotNull().NotEmpty()
                .WithMessage(AnimalSpeciesValidationMessages.AnimalSpeciesAnimalCategoryEmptyMessage);

            RuleFor(a => a.Kind).NotEmpty()
                .WithMessage(AnimalSpeciesValidationMessages.AnimalSpeciesKindEmptyMessage);


            RuleFor(a => a.Kind).MinimumLength(2).MaximumLength(30)
                .WithMessage(AnimalSpeciesValidationMessages.AnimalSpeciesKindLengthMessage);
        }
    }
}