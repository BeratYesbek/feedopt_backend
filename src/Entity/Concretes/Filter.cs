using Core.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concretes
{
    public class Filter : IEntity
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public string InputType { get; set; }

        public string DataType { get; set; }

        public string FilterType { get; set; }

    }
}
