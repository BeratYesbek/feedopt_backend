using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity;
using Entity.concretes;
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Abstracts
{
    public interface IMissingDeclarationService
    {
        IDataResult<MissingDeclaration> Add(MissingDeclaration missingDeclaration);

        IResult Update(MissingDeclaration missingDeclaration);

        IResult Delete(MissingDeclaration missingDeclaration);

        IDataResult<MissingDeclaration> Get(int id);

        IDataResult<List<MissingDeclaration>> GetAll();

        IDataResult<List<MissingDeclarationDto>> GetAllMissingDeclarationDetail(int pageNumber);

        IDataResult<MissingDeclarationDto> GetMissingDeclarationDetailById(int id);
    }
}