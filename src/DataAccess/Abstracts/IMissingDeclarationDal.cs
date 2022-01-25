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
        List<MissingDeclarationDto> GetAllMissingDeclarationsDetail(int pageNumber, int pageSize = 20);
        List<MissingDeclarationDto> GetMissingDeclarationsDetailByFilter(Expression<Func<MissingDeclaration, bool>> filter, int pageNumber, int pageSize = 10);
        MissingDeclarationDto GetMissingDeclarationDetailById(int id);
    }
}
