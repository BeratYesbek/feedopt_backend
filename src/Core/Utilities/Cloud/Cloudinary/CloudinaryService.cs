using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.FileHelper;
using Core.Utilities.IoC;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IResult = Core.Utilities.Result.Abstracts.IResult;

namespace Core.Utilities.Cloud.Cloudinary
{
    public class CloudinaryService :FileHelper.FileHelper, ICloudinaryService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;
        private IConfiguration Configuration { get; set; }
        public CloudinaryService()
        {
            Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            CloudinaryOptions options = Configuration.GetSection("CloudinaryOptions").Get<CloudinaryOptions>();
            var account = new Account(options.Cloud, options.ApiKey, options.ApiSecret);

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<IResult> DeleteAsync(string filePath, string publicId = null)
        {
            var deletionParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deletionParams);
            return new SuccessResult();
        }

        public async Task<IResult> UploadAsync(IFormFile file, FileExtension fileExtension)
        {
            var result = CheckFileTypeValid(Path.GetExtension(file.FileName), fileExtension);
            if (!result.Success)
                return result;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            var imageUploadResult = await _cloudinary.UploadAsync(uploadParams);
            return new SuccessResult($"{imageUploadResult.SecureUrl}&&{imageUploadResult.PublicId}");
        }

        public async Task<IResult> UpdateAsync(IFormFile file, string filePath, FileExtension fileExtension, string publicId = null)
        {
                await DeleteAsync(null, publicId);
                var addedDelete = await UploadAsync(file, fileExtension);
                if (addedDelete.Success)
                {
                    return new SuccessResult(addedDelete.Message);
                }

                return new ErrorResult(addedDelete.Message);
        }

        protected override IResult CheckFileTypeValid(string type, FileExtension fileExtension)
        {
            return base.CheckFileTypeValid(type, fileExtension);
        }



    }
}