using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Business.Abstracts
{
    public interface IMissingDeclarationImageService
    {
         IResult Add(MissingDeclarationImage missingDeclarationImage, IFormFile[] formFiles);

         IResult Update(MissingDeclarationImage[] missingDeclarationImage, IFormFile[] formFiles);

         IResult Delete(MissingDeclarationImage[] missingDeclarationImage);

        IDataResult<List<MissingDeclarationImage>> GetByMissingDeclarationId(int id);

        IDataResult<MissingDeclarationImage> Get(int id);

        IDataResult<List<MissingDeclarationImage>> GetAll();
    }
}