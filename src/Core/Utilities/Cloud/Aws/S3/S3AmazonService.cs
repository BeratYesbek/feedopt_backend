using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Extensions.Configuration;
using Core.Utilities.FileHelper;

namespace Core.Utilities.Cloud.Aws.S3
{
    public class S3AmazonService : FileHelper.FileHelper, IS3AmazonService
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly AWSServiceConfiguration _awsSettings;
        private readonly IConfiguration _configuration;

        public S3AmazonService(IAmazonS3 amazonS3, IOptions<AWSServiceConfiguration> awsSettings, IConfiguration configuration)
        {
            _amazonS3 = amazonS3;
            _awsSettings = awsSettings.Value;
            _configuration = configuration;

        }

        public Task<Result.Abstracts.IResult> DeleteAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<Result.Abstracts.IResult> UpdateAsync(IFormFile file, string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<Result.Abstracts.IResult> UploadAsync(IFormFile file,FileExtension fileExtension)
        {
            var result = CheckFileTypeValid(Path.GetExtension(file.FileName), fileExtension);
                
            if(!result.Success)
                return result;
            
            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);
                var dictionary = GetKeyAndUrl(file);
                
                var uploadRequest = new PutObjectRequest
                {
                    InputStream = newMemoryStream,
                    Key = dictionary["key"],
                    BucketName = _awsSettings.BucketName,
                    ContentType = file.ContentType
                };

                var respone = await _amazonS3.PutObjectAsync(uploadRequest);
                if (respone.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return new SuccessResult(dictionary["url"]);

                return new ErrorResult("");
            }
        }

        public Dictionary<string, string> GetKeyAndUrl(IFormFile file)
        {
            Guid uuid = Guid.NewGuid();
            var folder = _configuration.GetSection("AwsFolders")["AnimalImage"];
            var extension = Path.GetExtension(file.FileName);
            var key = $"{folder}/{uuid}.{extension}";
            var url = $"https://{_awsSettings.BucketName}.s3.{_awsSettings.Region}.{_awsSettings.AwsUrl}/{key}";
            return new Dictionary<string, string>
            {
                { "key",key},
                {"url",url }
            };
        }


    }
}
