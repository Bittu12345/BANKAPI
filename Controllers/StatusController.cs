using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBankAPI_1.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IStatusRepository _statusRepo;

        //  string userName = Thread.CurrentPrincipal.Identity.Name;
        public StatusController(IStatusRepository statusRepositorycs)
        {
            _statusRepo = statusRepositorycs;
        }

        [HttpGet]
        public IActionResult GetCustomerStatus()
        {
            var status = _statusRepo.GetStatuses();
            return Ok(status);
        }

    }
}
