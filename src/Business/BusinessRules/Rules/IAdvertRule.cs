using Core.Entity.Concretes;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.BusinessRules.Rules
{
    public class AdvertRule : IRule
    {
        public IResult Run()
        {
           return Core.Utilities.Business.BusinessRules.Run(EmailConfirmedForCreateAdvert());
        }

        private IResult EmailConfirmedForCreateAdvert()
        {
            if (CurrentUser.User.EmailConfirmed)
                return new SuccessResult();

            return new ErrorResult("Email is not confirmed");
        }
    }
}
