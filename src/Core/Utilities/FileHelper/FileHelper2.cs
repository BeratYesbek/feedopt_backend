using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Cloud.Cloudinary;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileHelper
{
    public enum FileExtension
    {
        ImageExtension = 0,
        DocumentExtension = 1
    }

    public enum RecordType
    {
        Cloud = 0,
        Storage = 1
    }

    public class FileHelper2 : IFileHelper
    {
        private readonly IFileHelper _fileHelper;
        private string[] FileType { get; }

        public FileHelper2(RecordType recordType, FileExtension fileExtension)
        {
            FileType = GetExtensions(fileExtension);

            if (recordType == RecordType.Cloud)
            {
                _fileHelper = new CloudinaryService();
            }
            else
            {
            }
        }


        private string[] GetExtensions(FileExtension fileExtension)
        {
            switch (fileExtension)
            {
                case FileExtension.DocumentExtension:
                    return null;
                    break;
                case FileExtension.ImageExtension:
                    return FileExtensions.ImageExtensions;
                default:
                    return null;
            }
        }

        public async Task<IResult> Upload(IFormFile file)
        {
            var result = CheckFileTypeValid(Path.GetExtension(file.FileName));
            if (result.Success)
            {
                return await _fileHelper.Upload(file);
            }

            return result;
        }

        public async Task<IResult> Update(IFormFile file, string filePath)
        {
            var result = CheckFileTypeValid(Path.GetExtension(file.FileName));
            if (result.Success)
            {
                return await _fileHelper.Update(file, filePath);
            }

            return result;
        }

        public Task<IResult> Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        public void CheckDirectoryExists(string directory)
        {
            throw new NotImplementedException();
        }

        public IResult CheckFileTypeValid(string type)
        {
            foreach (var extension in FileType)
            {
                if (extension == type)
                {
                    return new SuccessResult();
                }
            }

            return new ErrorResult("File type is not suitable");
        }

        public void DeleteOldImageFile(string directory)
        {
            throw new NotImplementedException();
        }
    }
}