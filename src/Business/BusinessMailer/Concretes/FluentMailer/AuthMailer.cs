using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessMailer.Abstracts;
using Core.Entity;
using Core.Utilities.Mailer;
using Core.Utilities.Mailer.FluentMailer;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace Business.BusinessMailer.Concretes.FluentMailer
{
    internal class AuthMailer : IAuthMailer
    {
        public static string CurrentDirectory { get; } = $"{Environment.CurrentDirectory}\\wwwroot\\static\\mailer\\authMailer\\";
        public static string SendResetPasswordCodeHtmlPage { get; } = "SendResetPasswordCode.cshtml";
        public static string VerifyEmailHtmlPage { get; } = "VerifyEmail.cshtml";


        private readonly IFluentMailer _fluentMailer;

        public AuthMailer(IFluentMailer fluentMailer)
        {
            _fluentMailer = fluentMailer;
        }

        public async Task<IResult> SendResetPasswordCode(User user, string code,string subject,string body = default)
        {
            var mailer = _fluentMailer.StartMailer("Reset Your Password", user.Email);
            //var response = await mailer.UsingTemplateFromFile($"{CurrentDirectory}{SendResetPasswordCodeHtmlPage}", new { User = user, Code = code }).SendAsync();
             var response = await mailer.Body($"{code}").SendAsync();
            if (response.Successful)
                 return new SuccessResult("");
            return new ErrorResult(string.Join(", ", response.ErrorMessages));
        }

        public async Task<IResult> SendVerifyEmail(User user, string accessToken,string subject,string body = default)
        {
            var mailer = _fluentMailer.StartMailer(subject, user.Email);
            var response = await mailer.UsingTemplateFromFile($"{CurrentDirectory}{VerifyEmailHtmlPage}", new { User = user, Token = accessToken, Url = "" }).SendAsync();
            if (response.Successful)
                return new SuccessResult("");
            return new ErrorResult(string.Join(", ", response.ErrorMessages));
        }
    }
}
