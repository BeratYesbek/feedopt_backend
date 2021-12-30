using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class Seller : IEntity
    {
        public int Id { get; set; }

        public string SellerName { get; set; }

        public string SellerDescription { get; set; }

        public DateTime SellerJoinDate { get; set; }
    }
}