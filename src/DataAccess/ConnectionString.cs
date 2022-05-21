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
           // "Server=localhost;Database=FeedoptDb;Port=5432;Username=postgres;Password=123456";
        "User ID=fisixumajozvqq;Password=52484d72b4ee82e5d65870e6cac72265006523d66efb46b9aa45e730d19c7ffd;Host=ec2-3-217-113-25.compute-1.amazonaws.com;Port=5432;Database=d8r6gm0vi2bmqh;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;";
    }
}