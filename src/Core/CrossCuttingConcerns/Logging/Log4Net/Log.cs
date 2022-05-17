using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class Log : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Thread { get; set; }

        public string Message { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string Exception { get; set; }

        public DateTime Date { get; set; }

    }
}
