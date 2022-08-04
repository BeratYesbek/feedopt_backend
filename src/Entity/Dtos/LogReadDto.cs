using Core.Entity.Abstracts;
using System;
using Core.Entity.Concretes;

namespace Entity.Dtos
{
    public class LogReadDto : IDto
    {
        public int Id { get; set; }

        public string level { get; set; }

        public string message { get; set; }

        public string machinename { get; set; }

        public string logger { get; set; }

        public string email { get; set; }

        public string claims { get; set; }

        public string fullname { get; set; }

        public string userid { get; set; }

        public string methodname { get; set; }

        public dynamic LogDetail { get; set; }

        public dynamic Parameters { get; set; }
        public string stacktrace { get; set; }

        public User User { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
