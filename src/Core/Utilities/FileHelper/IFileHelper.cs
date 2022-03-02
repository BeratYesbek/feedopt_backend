using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;

namespace Core.Utilities.FileHelper
{
    public interface IFileHelper
    {
        IResult Upload(IFormFile file);
        IResult Update(IFormFile file, string filePath, string publicId = default);
        IResult Delete(string filePath, string publicId = default);

        Task<IResult> UploadAsync(IFormFile file);
        Task<IResult> UpdateAsync(IFormFile file, string filePath, string publicId = default);
        Task<IResult> DeleteAsync(string filePath, string publicId = default);
    }
}