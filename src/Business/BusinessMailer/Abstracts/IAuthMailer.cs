using System.Threading.Tasks;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;

namespace Business.BusinessMailer.Abstracts
{
    public interface IAuthMailer
    {
        Task<IResult> SendResetPasswordCode(User user, string code, string subject, string body = default);

        Task<IResult> SendVerifyEmail(User user, string accessToken, string subject, string body = default);
    }
}
