﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.FileHelper;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Cloud.Cloudinary
{
    public interface ICloudinaryService : IFileHelper
    {
        IResult Upload(IFormFile file, Image image = default);
        IResult Update(IFormFile file, string publicId, Image image = default);
        IResult Delete(string publicId);
    }
}