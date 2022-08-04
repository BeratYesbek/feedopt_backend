using Core.Entity.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;


namespace Core.Entity.Concretes
{
    public class Log : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string level { get; set; }
        public string message { get; set; }
        public string machinename { get; set; }
        public string logger { get; set; }
        public string email { get; set; }
        public string claims { get; set; }
        public string fullname { get; set; }
        public string userid { get; set; }
        public string logdetail { get; set; }
        public string methodname { get; set; }
        public string logparameters { get; set; }
        public string stacktrace { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
    }
}
