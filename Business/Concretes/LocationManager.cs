using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class LocationManager : ILocationService
    {
        public IResult Add(Location location)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Location location)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Location location)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Location> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Location>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
