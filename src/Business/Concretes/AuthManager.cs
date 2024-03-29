﻿using System;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect.SecurityAspect;
using Business.BusinessRules;
using Business.BusinessRules.BusinessMailer.Abstracts;
using Business.Security.Role;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entity.Concretes;
using Core.Utilities.Generator;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Concretes
{
    /// <summary>
    /// This class manage completely all auth process. Whenever need to manage something about auth, everything should do in this class because of SOLID - Single Responsibility Principle
    /// </summary>
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly IUserOperationClaimService _userOperationService;
        private readonly IAuthMailer _authMailer;
        private readonly IVerificationCodeService _verificationCodeService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper,
            IUserOperationClaimService userOperationClaimService, IAuthMailer authMailer,
            IVerificationCodeService verificationCodeService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperationService = userOperationClaimService;
            _authMailer = authMailer;
            _verificationCodeService = verificationCodeService;
        }

        /// <summary>
        /// User is created by this method. And It will send verify email to user
        /// </summary>
        /// <param name="userForRegisterDto"></param>
        /// <param name="password"></param>
        /// <returns>IDataResult</returns>
        [LogAspect(typeof(DatabaseLogger))]
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
                PreferredLanguage = Core.Utilities.Language.PreferredLanguage.tr,
                EmailConfirmed = false,
            };

            var createdUser = _userService.Add(user);
            var result = _userOperationService.AddDefaultRole(createdUser.Data);
            if (result.Success)
            {
                var claims = _userService.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, claims, DateTime.Now.AddMinutes(10));
                _authMailer.SendVerifyEmail(createdUser.Data, accessToken.Token, "Verify Your Account");
                return new SuccessDataResult<User>(createdUser.Data);
            }

            _userService.Delete(createdUser.Data);
            return new ErrorDataResult<User>(null);
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns>IDataResult</returns>
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);

            if (!userToCheck.Success)
            {
                return new ErrorDataResult<User>(null, "Password or email is wrong");
            }

            if (!HashingHelper.VerifPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash,
                    userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(null, "Password or email is wrong");
            }

            return new SuccessDataResult<User>(userToCheck.Data);
        }

        /// <summary>
        /// Whether this method controls the user exists or not.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>IResult</returns>
        [LogAspect(typeof(DatabaseLogger))]
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Success)
            {
                return new ErrorResult("User exists");
            }

            return new SuccessResult();
        }

        /// <summary>
        /// Access Token is created by this method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dateTime"></param>
        /// <param name="tokenType"></param>
        /// <returns>IDataResult</returns>
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<AccessToken> CreateAccessToken(User user, DateTime dateTime = default,TokenType tokenType = TokenType.Standard)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims, dateTime,tokenType);
            return new SuccessDataResult<AccessToken>(accessToken, "Token has been created");
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        public IResult ResetPassword(string password, string passwordConfirmation)
        {
            if (!password.Equals(passwordConfirmation)) return new ErrorResult("Passwords are not matching");

            var result = _tokenHelper.GetIdentifier(TokenType.ResetPassword.ToString());
            if (!result.Success) return new ErrorResult("User couldn't be verified");

            var identifier = (string) result.Data;
            var userResult = _userService.Get(int.Parse(identifier));
            if (!userResult.Success) return new ErrorResult("User couldn't be found");
            var user = userResult.Data;
            
            HashingHelper.CreateHashPassword(password, out var passwordHash, out var passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _userService.Update(user);
            return new SuccessResult();

        }

        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> SendResetPasswordCode(string email)
        {
            var userResult = _userService.GetByMail(email);

            if (!userResult.Success) return new ErrorResult("User has not been found");
            var verifyCode = RandomGenerator.Get6Digits();
            _verificationCodeService.Add(new VerificationCode
            {
                CodeHash = verifyCode,
                Email = userResult.Data.Email,
                Expiry = DateTime.Now.AddMinutes(3),
                Type = CodeType.ResetPassword
            });
            await _authMailer.SendResetPasswordCode(userResult.Data, verifyCode, "Reset Your Password");
            return new SuccessResult();
        }

        /// <summary>
        /// This method verified everything about auth for instance reset password etc. 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="email"></param>
        /// <returns>IResult</returns>
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<User> VerifyCode(string code, string email)
        {
            var result = _verificationCodeService.Get(code, email);
            if (!result.Success) return new ErrorDataResult<User>(null, result.Message);
            var user = _userService.GetByMail(email);
            return new SuccessDataResult<User>(user.Data, result.Message);
        }

        /// <summary>
        /// This method is able to change password of users
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirmation"></param>
        /// <param name="code"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [SecuredOperation($"{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger),Priority = 2)]
        public async Task<IResult> ChangePassword(string oldPassword, string password, string passwordConfirmation,string code,string email)
        {
            var result = Core.Utilities.Business.BusinessRules.Run(
                AuthBusinessRules.EmailConfirmation(CurrentUser.User.Email,email),
                AuthBusinessRules.PasswordValidation(password,passwordConfirmation),
                AuthBusinessRules.VerifyOldPassword(oldPassword,CurrentUser.User.PasswordHash,CurrentUser.User.PasswordSalt)
                );
            if (result.Success)
            {
                var verificationCode = _verificationCodeService.Get(code, email);
                if (!verificationCode.Success) return new ErrorDataResult<User>(null, result.Message);

                byte[] passwordSalt, passwordHash;
                HashingHelper.CreateHashPassword(password,out passwordHash,out passwordSalt);
                var user = CurrentUser.User;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                var updatedResult = await _userService.Update(user);
                if (updatedResult.Success) return new SuccessResult("Password has been updated");
            }

            return result;
        }

        /// <summary>
        /// This method control user and session by checking the access token
        /// </summary>
        /// <returns>IDataResult</returns>
        [SecuredOperation($"{Role.IsLoggedIn},{Role.User},{Role.SuperAdmin},{Role.Admin}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger),Priority = 2)]
        public IDataResult<User> IsLoggedIn()
        {
            return new SuccessDataResult<User>(CurrentUser.User);
        }

        /// <summary>
        /// User is verified by this method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>IResult</returns>
        [LogAspect(typeof(DatabaseLogger),Priority = 2)]
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