using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class OptionManager : IOptionService
    {

        private readonly IOptionDal _optionDal;
        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }

        public IDataResult<OptionDto> GetOptions()
        {
            return new SuccessDataResult<OptionDto>(_optionDal.GetOptions());
        }
    }
}
