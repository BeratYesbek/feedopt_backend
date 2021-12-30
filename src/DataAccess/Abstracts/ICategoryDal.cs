using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity;

namespace DataAccess.Abstracts
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}
