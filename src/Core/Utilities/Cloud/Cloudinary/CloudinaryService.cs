using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Extensions;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Cloud.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryDotNet.Cloudinary cloudinary;

        public CloudinaryService()
        {

            Account account = new Account(
                CloudinaryOptions.Cloud,
                CloudinaryOptions.ApiKey,
                CloudinaryOptions.ApiSecret);

            cloudinary = new CloudinaryDotNet.Cloudinary(account);
            cloudinary.Api.Secure = true;
        }

         public IResult Upload(IFormFile file)
         {
             ImageUploadResult imageUploadResult = null;
             var uploadParams = new ImageUploadParams()
             {
                 File = new FileDescription(file.FileName, file.OpenReadStream())
             };

             imageUploadResult = cloudinary.Upload(uploadParams);


            return new SuccessResult(imageUploadResult.SecureUrl.ToString());
         }
 
         public IResult Delete(string filePath, string publicId)
         {
             var deletionParams = new DeletionParams(publicId);
             cloudinary.Destroy(deletionParams);
             return new SuccessResult();
         }
 
         public IResult Update(IFormFile file,string filePath ,string publicId)
         { 
             var result = Delete(null, publicId);
             if (result.Success)
             {
                 var addedDelete = Upload(file);
                 if (addedDelete.Success)
                 {
                     return new SuccessResult(addedDelete.Message);
                 }
 
                 return new ErrorResult(addedDelete.Message);
             }
 
             return new ErrorResult(result.Message);
         }
    


        
    }
}