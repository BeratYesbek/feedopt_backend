using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Core.Entity;
using Core.Utilities.IoC;
using Core.Utilities.Mailer;
using Core.Utilities.Security.JWT;
using DataAccess.Concretes;
using FluentEmail.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessMailer
{
    public class VerifyEmailMailer //: IMailer
    {
        private static readonly string _currentDirectory = $"{Environment.CurrentDirectory}\\wwwroot\\static\\mailer\\";
        private const string _verifyHtmlPage = "VerifyEmail.cshtml";

        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public VerifyEmailMailer()
        {
            _userService = _userService = new UserManager(new EfUserDal());
            _tokenHelper = ServiceTool.ServiceProvider.GetService<ITokenHelper>();
        }

        public void SendEmail(EmailType emailType, User user)
        {
            switch (emailType)
            {
                case EmailType.VerifyEmail:
                    SendVerifyEmail(user, "Email Verification");
                    break;
                default:
                    throw new ArgumentException("This is not suitable type for this class");

            }
        }

        public async void SendVerifyEmail(User user, string subject)
        {
            var _email = Mailer.StartMailer(subject, user.Email);
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims, DateTime.Now.AddMinutes(10));

            await _email.UsingTemplateFromFile($"{_currentDirectory}{_verifyHtmlPage}", new { User = user, Token = accessToken.Token, Url = "" }).SendAsync();
        }


    }
}