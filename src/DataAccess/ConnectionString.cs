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
            "Server=(localdb)\\MSSQLLocalDB;Database=NervioTestDb;Trusted_Connection=true  ";
    }
}