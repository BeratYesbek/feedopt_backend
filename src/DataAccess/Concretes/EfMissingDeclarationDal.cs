using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstracts;
using Entity;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfMissingDeclarationDal : EfEntityRepositoryBase<MissingDeclaration, NervioDbContext>, IMissingDeclarationDal
    {
        public List<MissingDeclarationDto> GetAllMissingDeclarationsDetail()
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from missing in context.MissingDeclarations
                             join animalSpecies in context.AnimalSpecies on missing.AnimalSpeciesId equals animalSpecies.Id
                             select new MissingDeclarationDto
                             {
                                 MissingDeclaration = missing,
                                 MissingDeclarationImages = (from image in context.MissingDeclarationImages where missing.Id == image.MissingDeclarationId select image).ToArray(),
                                 AnimalSpecies = animalSpecies

                             };
                return result.ToList();
            }
        }

        public List<MissingDeclarationDto> GetMissingDeclarationsDetailByFilter(Expression<Func<MissingDeclaration, bool>> filter)
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from missing in context.MissingDeclarations.Where(filter)
                             join animalSpecies in context.AnimalSpecies on missing.AnimalSpeciesId equals animalSpecies.Id
                             select new MissingDeclarationDto
                             {
                                 MissingDeclaration = missing,
                                 MissingDeclarationImages = (from image in context.MissingDeclarationImages where missing.Id == image.MissingDeclarationId select image).ToArray(),
                                 AnimalSpecies = animalSpecies

                             };
                return result.ToList();
            }
        }

        public MissingDeclarationDto GetMissingDeclarationDetailById(int id)
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from missingDeclaration in context.MissingDeclarations
                             where missingDeclaration.Id == id
                             join animalSpecies in context.AnimalSpecies on missingDeclaration.AnimalSpeciesId equals animalSpecies.Id
                             select new MissingDeclarationDto
                             {
                                 MissingDeclaration = missingDeclaration,
                                 AnimalSpecies = animalSpecies,
                                 MissingDeclarationImages = (from images in context.MissingDeclarationImages where images.MissingDeclarationId == missingDeclaration.Id select images).ToArray()
                             };
                return result.First();
            }
        }
    }
}
