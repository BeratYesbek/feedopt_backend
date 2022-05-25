using Business.Abstracts;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionsController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet("getOptions")]
        public IActionResult GetOptions()
        {
            var result = _optionService.GetOptions();
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPost]
        public  IActionResult StartMailer(string subject, string email)
        {
            var smtp = new SmtpSender(() => new SmtpClient()
            {
                Host = "mail.feedopt.com",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "info@feedopt.com",
                    Password = "feedopt123awex."
                }
            });

            Email.DefaultSender = smtp;
            Email.DefaultRenderer = new RazorRenderer();

            var fluentEmail = Email.From("info@feedopt.com")
                 .To(email)
                 .Body("dojsaoıdjsaıodıoasıodsaıodjıoasjdıosajıodsajıdofıofjasıodjıoasıodsaıojdıoasıodjasıjdıoasjoıdjıoasjoıdjsaıodsadas")
                 .Subject(subject).Send();
            return Ok("Gönderildi.");
        }
    }
}
