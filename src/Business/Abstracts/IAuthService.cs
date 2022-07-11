using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Security.JWT;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto,string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IResult VerifyEmail(int userId);
        IDataResult<AccessToken> CreateAccessToken(User user,DateTime dateTime = default,TokenType tokenType = TokenType.Standard);
        IResult ResetPassword(string password,string passwordConfirmation);
        Task<IResult> SendResetPasswordCode(string email);
        IDataResult<User> VerifyCode(string code,string email);
        Task<IResult> ChangePassword(string oldPassword, string password, string passwordConfirmation);
        IDataResult<User> IsLoggedIn();
    }
}