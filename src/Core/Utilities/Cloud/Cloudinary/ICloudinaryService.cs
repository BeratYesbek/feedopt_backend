using System.Threading.Tasks;
using Core.Utilities.FileHelper;
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