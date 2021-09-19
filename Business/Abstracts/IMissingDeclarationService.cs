using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity;
using Entity.concretes;

namespace Business.Abstracts
{
    public interface IMissingDeclarationService
    {
        IResult Add(MissingDeclaration missingDeclaration);

        IResult Update(MissingDeclaration missingDeclaration);

        IResult Delete(MissingDeclaration missingDeclaration);

        IDataResult<MissingDeclaration> Get(MissingDeclaration missingDeclaration);

        IDataResult<List<MissingDeclaration>> GetAll(MissingDeclaration missingDeclaration);
    }
}
