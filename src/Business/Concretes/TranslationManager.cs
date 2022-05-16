using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class TranslationManager : ITranslationService
    {
        private readonly ITranslationDal _translationDal;

        public TranslationManager(ITranslationDal translationDal)
        {
            _translationDal = translationDal;
        }

        public IDataResult<Translation> Add(Translation translation)
        {
            var data = _translationDal.Add(translation);
            if (data is not null)
            {
                return new SuccessDataResult<Translation>(data);
            }

            return new ErrorDataResult<Translation>(null);
        }

        public IResult Update(Translation translation)
        {
            _translationDal.Update(translation);
            return new SuccessResult();

        }

        public IResult Delete(Translation translation)
        {
            _translationDal.Delete(translation);
            return new SuccessResult();
        }

        public IDataResult<Translation> GetById(int id)
        {
            var data = _translationDal.Get(t => t.Id == id);
            if (data is not null)
            {
                return new SuccessDataResult<Translation>(data);
            }

            return new ErrorDataResult<Translation>(null);
        }

        public IDataResult<List<Translation>> GetByType(string type)
        {
            var data = _translationDal.GetAll(t => t.Type.Equals(type));
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Translation>>(data);
            }

            return new ErrorDataResult<List<Translation>>(data);
        }

        public IDataResult<List<Translation>> GetAll()
        {
            var data = _translationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Translation>>(data);
            }

            return new ErrorDataResult<List<Translation>>(null);
        }
    }
}
