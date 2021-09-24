using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IMissingDeclarationImageService
    {   
        IResult Add(MissingDeclarationImage missingDeclarationImage);

        IResult Update(MissingDeclarationImage missingDeclarationImage);

        IResult Delete(MissingDeclarationImage missingDeclarationImage);

        IDataResult<MissingDeclarationImage> Get(int id);

        IDataResult<List<MissingDeclarationImage>> GetAll();
    }
}
