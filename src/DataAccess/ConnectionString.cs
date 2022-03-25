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
            "User ID=tpctvpaasfedcg;Password=654fba92b044e293bc998d7f4b8971b3bfadf19f5acf15faa7510b73672fd250;Host=ec2-3-230-238-86.compute-1.amazonaws.com;Port=5432;Database=d5rkacah2m4r42;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;";
    }
}