using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileHelper
{
    public interface IFileHelper
    {
        Task<IResult> Upload(IFormFile file);
        Task<IResult> Update(IFormFile file, string filePath);
        Task<IResult> Delete(string filePath);
        void CheckDirectoryExists(string directory);
        IResult CheckFileTypeValid(string type);
        void DeleteOldImageFile(string directory);
    }
}
