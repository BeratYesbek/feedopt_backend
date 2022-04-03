using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public List<string> Messages { get; set; }
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}