using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstracts;
using Entity;

namespace DataAccess.Concretes
{
    public class EfMissingDeclarationDal : EfEntityRepositoryBase<MissingDeclaration, NervioDbContext>, IMissingDeclarationDal
    {

    }
}
