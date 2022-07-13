using System.ComponentModel.DataAnnotations;
using System.Data;
using Core.Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(t => t.Email).NotNull().NotEmpty();
        RuleFor(t => t.FullName).NotNull().NotEmpty();
        RuleFor(t => t.FullName).MinimumLength(3);
        RuleFor(t => t.PasswordHash).NotNull().NotEmpty();
        RuleFor(t => t.PasswordSalt).NotNull().NotEmpty();
        RuleFor(t => t.Email).Must(EmailRegex);
    }

    private bool EmailRegex(string email)
    {
        var emailAttribute = new EmailAddressAttribute();
        var result = emailAttribute.IsValid(email);
        return result;
    }
}