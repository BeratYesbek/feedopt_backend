using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Core.Utilities.Security.Hashing.BCrypt;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    internal class VerificationCodeManager : IVerificationCodeService
    {
        private readonly IVerificationCodeDal _verificationCodeDal;
        public VerificationCodeManager(IVerificationCodeDal verificationCodeDal)
        {
            _verificationCodeDal = verificationCodeDal;
        }
        public IDataResult<VerificationCode> Add(VerificationCode verificationCode)
        {
            verificationCode.CodeHash = BCryptHashingHelper.HashValue(verificationCode.CodeHash);
            var data = _verificationCodeDal.Add(verificationCode);
            return new SuccessDataResult<VerificationCode>(data);
        }

        public IDataResult<VerificationCode> Get(string code,string email)
        {
            var verificationCode = _verificationCodeDal.GetAll(t => t.Email == email)?.Last();
            if (verificationCode is null)
                return new ErrorDataResult<VerificationCode>(null, "Your email is not able to paired");

            var timeSpan = verificationCode.Expiry - DateTime.Now;
            if (!BCryptHashingHelper.VerifyHashValue(code, verificationCode.CodeHash))
                return new ErrorDataResult<VerificationCode>(null, "Code could not be verified");
            if (timeSpan.Minutes <= 0)
            {
                return new ErrorDataResult<VerificationCode>(null,"Code expired");
            }
            return new SuccessDataResult<VerificationCode>(null,"Code has been verified");

        }
    }
}
