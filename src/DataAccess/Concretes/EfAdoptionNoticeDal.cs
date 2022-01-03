using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.concretes;
using Entity.Dtos;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DataAccess.Concretes
{
    public class EfAdoptionNoticeDal : EfEntityRepositoryBase<AdoptionNotice, NervioDbContext>, IAdoptionNoticeDal
    {
        public List<AdoptionNoticeDto> GetAllAdoptionNoticeDetail()
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from adoption in context.AdoptionNotices
                             join animalSpecies in context.AnimalSpecies on adoption.AnimalSpeciesId equals animalSpecies.Id
                             select new AdoptionNoticeDto
                             {
                                 AdoptionNotice = adoption,
                                 AdoptionNoticeImage = (from image in context.AdoptionNoticeImages where adoption.Id == image.AdoptionNoticeId select image).ToArray(),
                                 AnimalSpecies = animalSpecies

                             };
                return result.ToList();
            }

        }

        public List<AdoptionNoticeDto> GetAdoptionNoticeDetailsByFilter(Expression<Func<AdoptionNotice, bool>> filter)
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from adoption in context.AdoptionNotices.Where(filter)
                             join animalSpecies in context.AnimalSpecies on adoption.AnimalSpeciesId equals animalSpecies.Id
                             select new AdoptionNoticeDto
                             {
                                 AdoptionNotice = adoption,
                                 AdoptionNoticeImage = (from image in context.AdoptionNoticeImages where adoption.Id == image.AdoptionNoticeId select image).ToArray(),
                                 AnimalSpecies = animalSpecies

                             };
                return result.ToList();
            }
        }

        public AdoptionNoticeDto GetAdoptionNoticeDetailById(int id)
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from adoptionNotice in context.AdoptionNotices
                             where adoptionNotice.Id == id
                             join animalSpecies in context.AnimalSpecies on adoptionNotice.AnimalSpeciesId equals animalSpecies.Id
                             select new AdoptionNoticeDto
                             {
                                 AdoptionNotice = adoptionNotice,
                                 AdoptionNoticeImage = (from image in context.AdoptionNoticeImages where adoptionNotice.Id == image.AdoptionNoticeId select image).ToArray(),
                                 AnimalSpecies = animalSpecies

                             };
                return result.First();
            }
        }
    }
}
