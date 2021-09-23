using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Core.Entity
{
    public class User : IEntity
    {
        public int UserId { get; set; }
    }
}