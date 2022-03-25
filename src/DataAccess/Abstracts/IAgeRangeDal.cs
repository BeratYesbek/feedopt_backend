using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entity.Abstracts;
using Entity.Concretes;

namespace DataAccess.Abstracts
{
    public interface IAgeRangeDal : IEntityRepository<Age>
    {
    }
}
