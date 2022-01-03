using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IMissingDeclarationDal : IEntityRepository<MissingDeclaration>
    {
        List<MissingDeclarationDto> GetAllMissingDeclarationsDetail();
        List<MissingDeclarationDto> GetMissingDeclarationsDetailByFilter(Expression<Func<MissingDeclaration, bool>> filter);
        MissingDeclarationDto GetMissingDeclarationDetailById(int id);
    }
}
