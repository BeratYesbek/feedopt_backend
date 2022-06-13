using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Business.Abstracts;
using Business.Concretes;
using Core.Utilities.Cloud.Aws;
using DataAccess;
using DataAccess.Concretes;
using Entity.Dtos;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionService _optionService;
        private readonly IAmazonS3 _amazonS3;
        private readonly AWSServiceConfiguration _awsSettings;


        public OptionsController(IOptionService optionService,IAmazonS3 amazonS3, IOptions<AWSServiceConfiguration> awsSettings)
        {
            _optionService = optionService;
            _amazonS3 = amazonS3;
            _awsSettings = awsSettings.Value;
        }

        [HttpGet("getOptions")]
        public IActionResult GetOptions()
        {
            var result = _optionService.GetOptions();
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("/add")]
        public async Task<IActionResult> Add([FromForm]  AdvertCreateDto advertCreateDto)
        {
            https://feedopt.s3.eu-central-1.amazonaws.com/Animal+Images/
            var file = advertCreateDto.Files[0];
            Guid uuid = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new PutObjectRequest
                {
                    InputStream = newMemoryStream,
                 
                    Key = "Animal Images/" + uuid.ToString() + extension,
                    BucketName = _awsSettings.BucketName,
                    ContentType = file.ContentType
                };

                //var fileTransferUtility = new TransferUtility(_amazonS3);

                //await fileTransferUtility.UploadAsync(uploadRequest);
                var respone = await _amazonS3.PutObjectAsync(uploadRequest);

                return Ok(respone);
            }

            /*   var file = advertCreateDto.Files[0];

               using (var newMemoryStream = new MemoryStream())
               {
                   file.CopyTo(newMemoryStream);

                   var request = new PutObjectRequest()
                   {

                       InputStream = newMemoryStream,
                       BucketName = _awsSettings.BucketName,
                       Key = advertCreateDto.Files[0].FileName,
                       CannedACL = S3CannedACL.PublicRead
                   };
                   var result = _amazonS3.UploadF
                   return Ok(result);

               }*/

        }
        private Stream GetFileStream(IFormFile file)
        {

            return new MemoryStream(file.OpenReadStream().ReadByte());
        }

        private string GetUniqueFileName(string fileName)
        {
            return string.Concat(Path.GetFileNameWithoutExtension(fileName), "_",
                Path.GetRandomFileName().Replace(".", ""), Path.GetExtension(fileName));
        }

        [HttpGet]
        public  IActionResult StartMailer()
        {
            var colors = new AppDbContext().Colors.Include(t => t.ColorTranslations).ToList();
            return Ok(colors);
        }
    }
}
