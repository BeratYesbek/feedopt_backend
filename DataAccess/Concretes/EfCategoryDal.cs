using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entity;

namespace DataAccess.Concretes
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NervioDbContext>, ICategoryDal
    {
    }
}