﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.BusinessMailer;
using Business.Security.Role;
using Core.Aspects.Autofac;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.Mailer;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.Dtos;

namespace Business.Concretes
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly IUserOperationClaimService _userOperationService;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper,IUserOperationClaimService userOperationClaimService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperationService = userOperationClaimService;
        }

        [MailerAspect(typeof(VerifyEmailMailer), EmailType.VerifyEmail)]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreateHashPassword(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FullName = userForRegisterDto.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                PreferredLanguage = Core.Utilities.Language.PreferredLanguage.tr,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false

            };

            var createdUser = _userService.Add(user);
            var result = _userOperationService.AddDefaultRole(createdUser.Data);
            if (result.Success)
                return new SuccessDataResult<User>(createdUser.Data);
            
            _userService.Delete(createdUser.Data);
            return new ErrorDataResult<User>(null);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);

            if (!userToCheck.Success)
            {
                Debug.Print("");
                return new ErrorDataResult<User>(null, "Password or email is wrong");
            }
            if (!HashingHelper.verifPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash,
                userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(null, "Password or email is wrong");
            }

            return new SuccessDataResult<User>(userToCheck.Data);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Success)
            {
                return new ErrorResult("User exists");
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user, DateTime dateTime = default)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims, dateTime);
            return new SuccessDataResult<AccessToken>(accessToken, "Token has been created");
        }

        [SecuredOperation($"{Role.IsLoggedIn},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        public IDataResult<User> IsLoggedIn()
        {
            return new SuccessDataResult<User>(CurrentUser.User);
        }

        public IResult VerifyEmail(int userId)
        {
            var userResult = _userService.Get(userId);
            if (!userResult.Success)
                return new SuccessResult("Email has not been confirmed");

            var user = userResult.Data;
            user.EmailConfirmed = true;
            _userService.Update(user);
            return new SuccessResult("Email has been confirmed successfully");

        }
    }
}