using System;
using System.IO;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;

namespace Core.Utilities.FileHelper
{
    public enum FileExtension
    {
        DocumentExtension = 0,
        ImageExtension = 1,
    }

    public abstract class FileHelper
    {
        private readonly string _currentDirectory = Environment.CurrentDirectory + "\\wwwroot";
        private string _folderName = "Files";




        protected virtual IResult Update(IFormFile file, string filePath,string folderName)
        {
            _folderName = folderName;
            if (file == null && file.Length <= 0)
            {
                return new ErrorResult("File doesn't exist");
            }
            var type = Path.GetExtension(file.FileName);
            var randomName = Guid.NewGuid().ToString();

            DeleteOldFile((_currentDirectory + filePath).Replace("/", "\\"));
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateFile(_currentDirectory + _folderName + randomName + type, file);
            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
        }

        protected virtual IResult Delete(string filePath)
        {
            DeleteOldFile((_currentDirectory + filePath).Replace("/", "\\"));
            return new SuccessResult();
        }

        protected virtual IResult CheckFileTypeValid(string type,FileExtension fileExtension)
        {
            var extensions = GetExtensions(fileExtension);
            foreach (var extension in extensions)
            {
                if (extension == type)
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult("File type is not suitable");
        }

        protected virtual string[] GetExtensions(FileExtension fileExtension)
        {
            return fileExtension switch
            {
                FileExtension.DocumentExtension => FileExtensions.DocumentExtensions,
                FileExtension.ImageExtension => FileExtensions.ImageExtensions,
                _ => null
            };
        }

        protected virtual IResult Upload(IFormFile file, string folderName)
        {
            _folderName = folderName;
            if (file == null && file.Length <= 0)
            {
                return new ErrorResult("File doesn't exist");
            }

            var randomName = Guid.NewGuid().ToString();
            var type = Path.GetExtension(file.FileName);
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateFile(_currentDirectory + _folderName + randomName + type, file);

            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
        }


        private static void DeleteOldFile(string directory)
        {
            if (File.Exists(directory.Replace("/", "\\")))
            {
                File.Delete(directory.Replace("/", "\\"));
            }
        }

        private static void CreateFile(string directory, IFormFile file)
        {
            using FileStream fileStream = File.Create(directory);
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        private static void CheckDirectoryExists(string directory)
        {
            if (!Directory.Exists((directory)))
            {
                Directory.CreateDirectory(directory);
            }
        }


    }
}