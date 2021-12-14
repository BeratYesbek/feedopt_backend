using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
