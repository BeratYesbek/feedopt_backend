using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Entity.Concretes
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string ProductTitle { get; set; }

        public string ProductDescription { get; set; }

        public int ProductQuantity { get; set; }

        public float ProductPrice { get; set; }

        public int SellerId { get; set; }

        public int CategoryId { get; set; }
    }
}