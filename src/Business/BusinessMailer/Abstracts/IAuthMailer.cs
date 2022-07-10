using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Utilities.Result.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Business.BusinessMailer.Abstracts
{
    public interface IAuthMailer
    {
        Task<IResult> SendResetPasswordCode(User user, string code, string subject, string body = default);

        Task<IResult> SendVerifyEmail(User user, string accessToken, string subject, string body = default);
    }
}
