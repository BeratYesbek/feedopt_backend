using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Core.Utilities.Security.Hashing;

namespace Business.BusinessRules;

internal static class AuthBusinessRules
{
    internal static IResult EmailConfirmation(string tokenEmail, string emailInParams)
    {
        if (tokenEmail == emailInParams)
        {
            return new SuccessResult();
        }
        return new ErrorResult("Token email is not matching with params email");
    }

    internal static IResult VerifyOldPassword(string oldPassword, byte[] passwordHash, byte[] passwordSalt)
    {
        var result = HashingHelper.verifPasswordHash(oldPassword, CurrentUser.User.PasswordHash, CurrentUser.User.PasswordSalt);
        if (result)
        {
            return new SuccessResult();
        }
        return new ErrorResult("Old password is not correct");
    }

    internal static IResult PasswordValidation(string password, string passwordConfirmation)
    {
        if (password.Equals(passwordConfirmation))
        {
            if (password.Length >= 6)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult("Password must be six character");
            }
        }
        return new ErrorResult("Passwords are not matching");
    }
}