using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Cloud.Cloudinary
{
    public interface ICloudinaryService 
    {
        Task<Result.Abstracts.IResult> DeleteAsync(string filePath,string publicId = default);

        Task<Result.Abstracts.IResult> UpdateAsync(IFormFile file, string filePath,FileExtension fileExtension,string publicId = default);

        Task<Result.Abstracts.IResult> UploadAsync(IFormFile file, FileExtension fileExtension);
    }
}