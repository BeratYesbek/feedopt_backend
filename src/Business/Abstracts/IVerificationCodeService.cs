using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IVerificationCodeService
    {
        IDataResult<VerificationCode> Add(VerificationCode verificationCode);

        IDataResult<VerificationCode> Get(string code,string email);
    }
}
