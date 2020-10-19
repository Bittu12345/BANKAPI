using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetBankAPI_1.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DotNetBankAPIContext _context;
        public AccountController(DotNetBankAPIContext dotNetBankAPIContext)
        {
            _context = dotNetBankAPIContext;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return NotFound();
        }

        // POST api/<AccountController>

        //For Creating the new account - We will ask user to enter the Customer ID 
        //That Customer ID needs to be in the Customer table.
        //Be default the customer will have the Current account
        //When Creating the new savings account we will add the new record
        //if it's the same Current account we will just update the record with the balance in status table.

        [HttpPost]
        public IActionResult Post([FromBody] AccountModel model)
        {
            //get the status of the customer ins status table.
            // There should be the customer with that customerID as customer is created.
            if(model.AccountType != "S" && model.AccountType != "C")
            {
                return BadRequest();
            }
            var status = _context.Status.Where(x=>x.CustomerId == model.CustomerID).ToList();

            if(!status.Any())
            {
                return BadRequest();
            }

            var newStatus = status.Where(x => x.AccountType == model.AccountType).FirstOrDefault();

            if(newStatus == null)
            {
                //this account type is not present so it should be the Savings account
                //so add it
                var statusTobeAdded = new Status();
                statusTobeAdded.AccountId = status.FirstOrDefault().AccountId;
                statusTobeAdded.AccountType = "S";
                statusTobeAdded.CustomerId = model.CustomerID;
                statusTobeAdded.SsnId = status.FirstOrDefault().SsnId;
                statusTobeAdded.Status1 = "Added the Savings Account!";
                statusTobeAdded.Balance = model.Deposit;
                statusTobeAdded.Message = $"Added the savings account on {DateTime.Now.Date}";
                statusTobeAdded.LastUpdated = DateTime.Now;
                _context.Status.Add(statusTobeAdded);
                _context.SaveChanges();
                return Ok(statusTobeAdded);
            }

            else
            {
                //we're just updating the balance for the checkings account then.
                newStatus.Balance = model.Deposit;
                newStatus.Status1 = "balance update for the Checkings Acocount";
                newStatus.Message = $"balance updated on{DateTime.Now.Date}";
                _context.Status.Update(newStatus);
                _context.SaveChanges();
                return Ok(newStatus);
            }
                   
             
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return NotFound();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //get the customer id. and check it. We only delete the Savings account.

            var accountStatus = _context.Status.Where(s => s.CustomerId == id).ToList();

            if(accountStatus.Count == 0)
            {
                return BadRequest("The customer Not Found");
            }

            if(accountStatus.Count == 1 && accountStatus.FirstOrDefault()?.AccountType == "C") 
            {
                //it's the checkings account. We don't delete that
                return BadRequest("Cannot delete the Checkings account");
            }

            else
            {
                _context.Status.Remove(accountStatus.First());
                return Ok(accountStatus);
            }
        }
    }
}
