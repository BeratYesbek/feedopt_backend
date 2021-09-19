using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Entity;

namespace Business.Concretes
{
    public class MissingDeclarationManager : IMissingDeclarationService
    {
        public IResult Add(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IResult Update(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IDataResult<MissingDeclaration> Get(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<MissingDeclaration>> GetAll(MissingDeclaration missingDeclaration)
        {
            throw new NotImplementedException();
        }
    }
}
