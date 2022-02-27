using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CustomExceptions;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            var errorMessages = "";
            foreach (var error in result.Errors)
            {
                errorMessages = $"{errorMessages}*{error.ErrorMessage}";
            }

            if (!result.IsValid)
            {
             //   throw new ValidationException(errorMessages, result.Errors);
            }
        }
    }
}