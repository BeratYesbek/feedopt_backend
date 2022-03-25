using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class AgeRangeManager : IAgeRangeService
    {
        private readonly IAgeRangeDal _ageRangeDal;

        public AgeRangeManager(IAgeRangeDal ageRangeDal)
        {
            _ageRangeDal = ageRangeDal;
        }

        public IResult Add(Age age)
        {
            _ageRangeDal.Add(age);
            return new SuccessResult();
        }

        public IResult Update(Age age)
        {
            _ageRangeDal.Update(age);
            return new SuccessResult();
        }

        public IResult Delete(Age age)
        {
            _ageRangeDal.Delete(age);
            return new ErrorResult();
        }

        public IDataResult<Age> Get(int id)
        {
            var data = _ageRangeDal.Get(a => a.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<Age>(data);

            }

            return new ErrorDataResult<Age>(null);
        }

        public IDataResult<List<Age>> GetAll()
        {
            var data = _ageRangeDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Age>>(data);
            }

            return new SuccessDataResult<List<Age>>(null);
        }
    }
}
