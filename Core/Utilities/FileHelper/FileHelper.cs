using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileHelper
{
    public class FileHelper
    {
        private static readonly string _currentDirectory = Environment.CurrentDirectory + "\\wwwroot";
        private static string _folderName = "";
        private static string[] _fileExtension;

        public static IResult Upload(IFormFile formFile)
        {
            if (formFile == null && formFile.Length <= 0)
            {
                return new ErrorResult("File doesn't exist");
            }

            var type = Path.GetExtension(formFile.FileName);
            var typeValid = CheckFileTypeValid(type);
            var randomName = Guid.NewGuid().ToString();

            if (!typeValid.Success)
            {
                return new ErrorResult(typeValid.Message);
            }
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateImageFile(_currentDirectory + _folderName + randomName + type, formFile);
            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
        }


        public static IResult Update(IFormFile formFile, string imagePath)
        {
            if (formFile == null && formFile.Length <= 0)
            {
                return new ErrorResult("File doesn't exist");
            }

            var type = Path.GetExtension(formFile.FileName);
            var typeValid = CheckFileTypeValid(type);
            var randomName = Guid.NewGuid().ToString();

            if (typeValid.Message != null)
            {
                return new ErrorResult(typeValid.Message);
            }

            DeleteOldImageFile((_currentDirectory + imagePath).Replace("/", "\\"));
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateImageFile(_currentDirectory + _folderName + randomName + type, formFile);
            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
        }

        public static IResult Delete(string path)
        {
            DeleteOldImageFile((_currentDirectory + path).Replace("/", "\\"));
            return new SuccessResult();
        }



        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream fileStream = File.Create(directory))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
        }

        private static void CheckDirectoryExists(string directory)
        {
            if (!Directory.Exists((directory)))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static IResult CheckFileTypeValid(string type)
        {
            foreach (var extension in _fileExtension)
            {
                if (extension == type)
                {
                    return new SuccessResult();
                }
            }

            return new ErrorResult("File type is not suitable");
        }

        private static void DeleteOldImageFile(string directory)
        {
            if (File.Exists(directory.Replace("/", "\\")))
            {
                File.Delete(directory.Replace("/", "\\"));
            }

        }

        public static void SetFileExtension(string folderName, params string[] fileExtension)
        {
            _folderName = $"\\{folderName}\\";
            _fileExtension = fileExtension;
        }
    }
}