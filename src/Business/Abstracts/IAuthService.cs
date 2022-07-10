﻿using System;
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
        IDataResult<AccessToken> CreateAccessToken(User user,DateTime dateTime = default);
        IResult SendResetPasswordCode(string email);
        IResult VerifyCode(string code,string email);
        IDataResult<User> IsLoggedIn();
    }
}