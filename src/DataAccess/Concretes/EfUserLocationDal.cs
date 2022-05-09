using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace DataAccess.Concretes
{
    public class EfUserLocationDal : EfEntityRepositoryBase<UserLocation, AppDbContext>, IUserLocationDal
    {
        public async Task<UserLocation> AddAsync(UserLocation userLocation)
        {
            using (var context = new AppDbContext())
            {
                var addedEntity = await context.UserLocations.AddAsync(userLocation);
                return addedEntity.Entity;
            }
        }

    }
}