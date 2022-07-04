﻿using Core.Entity;
using Core.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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


    /*    public string logdetail
        {
            get => LogDetail;
            set
            {
                LogDetail = JsonSerializer.Deserialize(value);
            }
        }*/

       /* public string logparameters
        {
            get => Parameters;
            set
            {
                Parameters = JsonSerializer.Serialize(value);
            }
        }*/
        public string stacktrace { get; set; }

        public User User { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
