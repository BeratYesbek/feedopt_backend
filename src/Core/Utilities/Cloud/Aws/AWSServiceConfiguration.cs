using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Cloud.Aws
{

    public class AWSServiceConfiguration
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string BucketName { get; set; }

        public string Region { get; set; }

        public string AwsUrl { get; set; }
    }
}
