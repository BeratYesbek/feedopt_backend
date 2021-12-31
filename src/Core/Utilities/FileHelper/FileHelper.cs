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

    public enum FolderName
    {
        Files = 0,
        Images = 1,
        Documents = 2
    }

    public class FileHelper 
    {
        private readonly IFileHelper _fileHelper;
        private string[] FileType { get; }

        public FileHelper(RecordType recordType, FileExtension fileExtension, FolderName folderName = default)
        {
            FileType = GetExtensions(fileExtension);

            if (recordType == RecordType.Cloud)
            {
                _fileHelper = new CloudinaryService();
            }
            else
            {
                _fileHelper = new StorageHelper(folderName.ToString());
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



        /// <summary>
        ///    if this method success, message will return which contains file path
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns> error message or file path   </returns>
        public IResult Update(IFormFile file, string filePath, string publicId = null)
        {
            var result = CheckFileTypeValid(Path.GetExtension(file.FileName));
            if (result.Success)
            {
                return _fileHelper.Update(file, filePath, publicId);
            }

            return result;
        }

        public IResult Delete(string filePath, string publicId = null)
        {
           return  _fileHelper.Delete(filePath, publicId);
        }

        /// <summary>
        /// 
        ///     if this method success, message will return which contains file path
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns> error message or file path </returns>
        public IResult Upload(IFormFile file)
        {
            var result = CheckFileTypeValid(Path.GetExtension(file.FileName));
            if (result.Success)
            {
                return _fileHelper.Upload(file);
            }

            return result;
        }
    }
}