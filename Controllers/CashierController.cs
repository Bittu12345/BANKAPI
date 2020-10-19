using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetBankAPI_1.Repository;
using Microsoft.AspNetCore.Authorization;
using DotNetBankAPI_1.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetBankAPI_1.Controllers
{
    [Authorize(Roles = "Cashier")]
    [Route("[controller]")]
    [ApiController]
    public class CashierController : ControllerBase
    {
        private ICustomerAccountRepository _customerAccountRepo;

        public CashierController(ICustomerAccountRepository customerAccountRepository)
        {
            _customerAccountRepo = customerAccountRepository;
        }
        
        // GET <CashierController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Uses the same Method GetBooks which returns all the books.but ID is used as optional Parameters.
            var AccountDetails = _customerAccountRepo.getCustomerAccount(id);

            if (AccountDetails == null)
            {
                return NotFound();
            }
            return Ok(AccountDetails);
        }
        //Put <CashierController>/deposit/1/2000
        [HttpPut("deposit/{id}/{amount}/{accountType}")]
        public IActionResult Deposit(int id, int amount, string accountType)
        {
            var AmountDeposited = _customerAccountRepo.Deposit(id, amount, accountType).FirstOrDefault();
            return Ok(AmountDeposited);
        }

        [HttpPut("withdraw/{id}/{amount}/{accountType}")]
        public IActionResult Withdraw(int id, int amount, string accountType)
        {
            var AmountWithdrawed = _customerAccountRepo.WithDraw(id, amount, accountType).FirstOrDefault();
            return Ok(AmountWithdrawed);

        }

        [HttpPut("transfer/{sourceid}/{targetid}/{amount}")]
        public IActionResult TransferAmount(int sourceid, int targetid, int amount)
        {
            var TransactionDetails = _customerAccountRepo.Transfer(sourceid, targetid, amount);
            return Ok(TransactionDetails);
        }

    }
}
