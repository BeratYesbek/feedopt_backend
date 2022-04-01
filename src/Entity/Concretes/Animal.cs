using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Google.Apis.Util;

namespace Entity.Concretes
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public class Animal
    {
        public string AnimalName { get; set; }

        public int ColorId { get; set; }

        public int AgeId { get; set; }

        public Gender Gender { get; set; }
    }
}
