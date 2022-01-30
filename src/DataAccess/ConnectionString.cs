using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ConnectionString
    {
        public static string DataBaseConnectionString { get; set; } =
            "Server=postgres;Database=nerviotestdbb;Port=5432;Username=postgres;Password=123456";
    }
}