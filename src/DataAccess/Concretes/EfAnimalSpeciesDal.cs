using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.concretes;

namespace DataAccess.Concretes
{
    public class EfAnimalSpeciesDal : EfEntityRepositoryBase<AnimalSpecies, AppDbContext>, IAnimalSpeciesDal
    {
    }
}