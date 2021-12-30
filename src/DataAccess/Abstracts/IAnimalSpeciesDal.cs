using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity.concretes;

namespace DataAccess.Abstracts
{
    public interface IAnimalSpeciesDal : IEntityRepository<AnimalSpecies>
    {
    }
}
