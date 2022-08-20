using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.concretes;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfAnimalSpeciesDal : EfEntityRepositoryBase<AnimalSpecies, AppDbContext>, IAnimalSpeciesDal
    {
    }
}