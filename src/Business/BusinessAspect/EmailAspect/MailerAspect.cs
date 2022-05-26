using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessMailer;
using Business.Concretes;
using Business.Utilities;
using Castle.DynamicProxy;
using Core.Entity;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Mailer;
using Core.Utilities.Security.JWT;
using DataAccess.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspect
{

    public class MailerAspect : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Type _mailerType;
        private readonly EmailType _emailtype;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;

        public MailerAspect(Type mailerType, EmailType emailType)
        {
            if (!typeof(IMailer).IsAssignableFrom(mailerType))
            {
                throw new ArgumentException($"{mailerType.Name} is not a IMailer type");
            }

            _emailtype = emailType;
            _mailerType = mailerType;
            _tokenHelper = ServiceTool.ServiceProvider.GetService<ITokenHelper>();
            _userService = new UserManager(new EfUserDal());
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnAfter(IInvocation invocation)
        {
            var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var mailer = (IMailer)Activator.CreateInstance(_mailerType);
            if (typeof(VerifyEmailMailer).IsAssignableFrom(_mailerType))
            {
                var user = (UserForRegisterDto)invocation.Arguments[0];
                email = user.Email;
                SendEmail(mailer, email);
            }
            else
            {
                SendEmail(mailer, email);
            }


        }

        private void SendEmail(IMailer mailer,string email)
        {
            var result = _userService.GetByMail(email);
            if (!result.Success)
            {
                throw new ArgumentException("User email has not been found");
            }
            mailer.SendEmail(_emailtype, result.Data);
        }
    }
}