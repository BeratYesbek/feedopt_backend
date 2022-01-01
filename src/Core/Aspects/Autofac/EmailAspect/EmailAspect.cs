using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac
{
    public enum EmailType
    {
        TicketEmail,
        TicketAnswer,
        VerifyEmail,

    }
    public class EmailAspect : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private EmailType _emailType;
        public EmailAspect(EmailType emailType)
        {
            _emailType = emailType;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnAfter(IInvocation invocation)
        { 
            
        }
    }
}
