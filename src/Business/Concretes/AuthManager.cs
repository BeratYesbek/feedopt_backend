using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.BusinessMailer;
using Core.Aspects.Autofac;
using Core.Entity;
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

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

       // [MailerAspect(typeof(VerifyEmailMailer), EmailType.VerifyEmail)]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreateHashPassword(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                PreferredLanguage = userForRegisterDto.PreferredLanguage,

                // change later
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
       
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<User>(null, "Password or email is wrong");
            }
            if (userToCheck.Data.EmailConfirmed != true && userToCheck.Data.PhoneNumberConfirmed != true)
            {
                return new ErrorDataResult<User>(null, "You have to verify your email");
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

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var access_token = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(access_token, "Token has been created");
        }
    }
}