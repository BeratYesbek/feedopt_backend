using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity;

namespace Business.Concretes
{
    public class MissingDeclarationManager : IMissingDeclarationService
    {
        public IResult Add(MissingDeclaration missingDeclaration)
        {
            new EfMissingDeclarationDal().Add(missingDeclaration);
            return new SuccessResult();
        }

        public IResult Update(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IDataResult<MissingDeclaration> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<MissingDeclaration>> GetAll()
        {
            return new SuccessDataResult<List<MissingDeclaration>>(new EfMissingDeclarationDal().GetAll());
        }
    }
}