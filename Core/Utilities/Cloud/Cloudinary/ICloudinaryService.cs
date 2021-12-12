using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Cloud.Cloudinary
{
    public interface ICloudinaryService
    {
        IResult Add(IFormFile file);
        IResult Update(IFormFile file, string url);
        IResult Delete(string url);
        DataResult<String> GetByUrl(string url);
    }
}