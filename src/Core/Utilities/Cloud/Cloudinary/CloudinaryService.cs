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

         public IResult Upload(IFormFile file, Image image = default)
         {
             var extension = Path.GetExtension(file.FileName);
             var result = FileHelper.FileHelper.CheckFileTypeValid(extension);
             if (!result.Success)
             {
                 return new ErrorResult(result.Message);
             }
 
             ImageUploadResult imageUploadResult = null;
 
             if (image != default)
             {
                 var stream = image.ToStream(ImageFormat.Jpeg);
 
                 var uploadParams = new ImageUploadParams()
                 {
                     File = new FileDescription(file.FileName, stream)
                 };
                 imageUploadResult = cloudinary.Upload(uploadParams);
             }
             else
             {
                 var uploadParams = new ImageUploadParams()
                 {
                     File = new FileDescription(file.FileName, file.OpenReadStream())
                 };
                 imageUploadResult = cloudinary.Upload(uploadParams);
             }
 
 
             return new SuccessResult(imageUploadResult.SecureUrl.ToString());
         }
 
         public IResult Delete(string publicId)
         {
             var deletionParams = new DeletionParams(publicId);
             cloudinary.Destroy(deletionParams);
             return new SuccessResult();
         }
 
         public IResult Update(IFormFile file, string publicId, Image image = default)
         {
          /*   var result = Delete(publicId);
             if (result.Success)
             {
                 var addedDelete = Upload(file);
                 if (addedDelete.Success)
                 {
                     return new SuccessResult(addedDelete.Message);
                 }
 
                 return new ErrorResult(addedDelete.Message);
             }
 
             return new ErrorResult(result.Message);*/
          return null;
         }
        public async Task<IResult> Upload(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(IFormFile file, string filePath)
        {
            throw new NotImplementedException();
        }

        IResult ICloudinaryService.Delete(string publicId)
        {
            throw new NotImplementedException();
        }

      /*  public IResult Upload(IFormFile file, Image image = default)
        {
            throw new NotImplementedException();
        }

        public IResult Update(IFormFile file, string publicId, Image image = default)
        {
            throw new NotImplementedException();
        }*/

        Task<IResult> IFileHelper.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        public void CheckDirectoryExists(string directory)
        {
            throw new NotImplementedException();
        }

        public IResult CheckFileTypeValid(string type)
        {
            throw new NotImplementedException();
        }

        public void DeleteOldImageFile(string directory)
        {
            throw new NotImplementedException();
        }
    }
}