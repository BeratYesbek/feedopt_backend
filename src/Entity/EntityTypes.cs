using Entity.concretes;
using Entity.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EntityTypes
    {
        private  static Type[] types = { typeof(Advert), typeof(Color), typeof(Age), typeof(Animal), typeof(AdvertCategory), typeof(AnimalSpecies), typeof(AnimalCategory), typeof(Location) };
        public static Type GetEntityTypeByName(string name)
        {
            var type =  types.Where(t => t.Name == name).FirstOrDefault(); 
            if (type != null)
                return type;
            return null;
        }
    }
}
