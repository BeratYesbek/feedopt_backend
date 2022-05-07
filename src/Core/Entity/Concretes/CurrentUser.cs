using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concretes
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string UserEmail { get; set; }
    }
}