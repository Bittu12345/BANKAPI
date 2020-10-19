using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;
using DotNetBankAPI_1.SecureWebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBankAPI_1.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
            // If you can access this, you're essentially the authorized user. so give success response
            var userName = HttpContext.User.Identity.Name;
            var user = APISecurity.Login(userName);
            return Ok(user);
        }
    }
}
