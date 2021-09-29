using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entity;

namespace Business.Concretes
{
    public class MissingDeclarationManager : IMissingDeclarationService
    {

        private readonly IMissingDeclarationDal _missingDeclarationDal;

        public MissingDeclarationManager(IMissingDeclarationDal missingDeclarationDal)
        {
            _missingDeclarationDal = missingDeclarationDal;
        }

        public IDataResult<MissingDeclaration> Add(MissingDeclaration missingDeclaration)
        {
            var data = _missingDeclarationDal.Add(missingDeclaration);
            return new SuccessDataResult<MissingDeclaration>(data);
        }

        public IResult Update(MissingDeclaration missingDeclaration)
        {
            _missingDeclarationDal.Update(missingDeclaration);
            return new SuccessResult();
        }

        public IResult Delete(MissingDeclaration missingDeclaration)
        {
            _missingDeclarationDal.Delete(missingDeclaration);
            return new SuccessResult();
        }

        public IDataResult<MissingDeclaration> Get(int id)
        {
            var data = _missingDeclarationDal.Get(m => m.MissingDeclarationId == id);
            if (data != null)
            {
                return new SuccessDataResult<MissingDeclaration>(data);
            }

            return new ErrorDataResult<MissingDeclaration>(null);
        }

        public IDataResult<List<MissingDeclaration>> GetAll()
        {
            var data = _missingDeclarationDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<MissingDeclaration>>(data);
            }

            return new ErrorDataResult<List<MissingDeclaration>>(null);
        }
    }
}