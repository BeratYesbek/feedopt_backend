using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.concretes;

namespace DataAccess.Concretes
{
    public class EfAnimalSpeciesDal : EfEntityRepositoryBase<AnimalSpecies, AppDbContext>, IAnimalSpeciesDal
    {
    }
}