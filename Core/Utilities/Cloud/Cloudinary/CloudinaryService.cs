using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Cloud.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private CloudinaryDotNet.Cloudinary cloudinary;

        public CloudinaryService()
        {
            Account account = new Account(
                "afteb",
                "263478243847123",
                "TnN1Q8Tli3DcTZ-qDXw5zgZWid8");

            cloudinary = new CloudinaryDotNet.Cloudinary(account);
            cloudinary.Api.Secure = true;
            cloudinary.DeleteResources("xrba3uzkl5qiqpipzloo");
        }

        public IResult Add(IFormFile file)
        {
            var result = FileHelper.FileHelper.CheckFileTypeValid(file.)
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            
            ImageUploadResult imageUploadResult = cloudinary.Upload(uploadParams);
            return new SuccessResult();
        }

        public IResult Delete(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            cloudinary.Destroy(deletionParams);
            return new SuccessResult();
        }

        public DataResult<string> GetByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public IResult Update(IFormFile file, string publicId)
        {
            var result = Delete(publicId);
            if (result.Success)
            {
                var addedDelete = Add(file);
                if (addedDelete.Success)
                {
                    return new SuccessResult();
                }
                return new ErrorResult();
            }
            return new ErrorResult();
        }
    }
}