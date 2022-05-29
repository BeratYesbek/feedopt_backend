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
    public class EfColorDal : EfEntityRepositoryBase<Color, AppDbContext>, IColorDal
    {
        public List<Color> GetAllDetail()
        {
            try
            {
                using (var context = new AppDbContext())
                {

                    var data = context.Colors.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {

                throw;
            }
       
        }
    }
}
