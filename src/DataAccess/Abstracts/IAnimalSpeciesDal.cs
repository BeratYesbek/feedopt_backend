using System.Collections.Generic;
using Core.DataAccess;
using Entity.concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IAnimalSpeciesDal : IEntityRepository<AnimalSpecies>
    {
    }
}
