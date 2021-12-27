using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Entity.concretes;

namespace Entity.Concretes
{
    public class Animal : AnimalSpecies, IEntity
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public enum Gender
        {
            Male,
            Female
        }
    }
}