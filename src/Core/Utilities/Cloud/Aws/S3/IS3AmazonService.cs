using Core.Utilities.FileHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Cloud.Aws.S3
{
    public interface IS3AmazonService 
    {
        Task<Result.Abstracts.IResult> DeleteAsync(string filePath);

        Task<Result.Abstracts.IResult> UpdateAsync(IFormFile file, string filePath);

        Task<Result.Abstracts.IResult> UploadAsync(IFormFile file, FileExtension fileExtension);


    }
}
