using System;
using System.Linq;
using System.Threading;
using DotNetBankAPI_1.Models;
using DotNetBankAPI_1.Repository;
using DotNetBankAPI_1.SecureWebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DotNetBankAPI_1.Controllers
{
    //this is only for the executive to perform the customer crud operations

    //ONLY ADMIN will have control to access this end point.
    
    [Authorize(Roles = "Admin")]
    [Route("customer")]
    [ApiController]    
    public class ExecutiveController : ControllerBase
    {
        private ICustomerRepositorycs _customerRepo;    

        public ExecutiveController(ICustomerRepositorycs customerRepositorycs)
        {
            _customerRepo = customerRepositorycs;            
        }

        // GET: <customer>
        [HttpGet]
        
        public IActionResult GetAllCustomers([FromQuery] QueryParameters queryParameters)
        {
            var customers = _customerRepo.GetCustomer(queryParameters);
            return Ok(customers);
        }

        // GET customer/5
        [HttpGet("{id}")]
        public IActionResult GetCustomerId(int id)
        {
            QueryParameters queryParameters = new QueryParameters();
            // Uses the same Method GetBooks which returns all the books.but ID is used as optional Parameters.
            var customer = _customerRepo.GetCustomer(queryParameters, id)?.FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST <Customer>
        [HttpPost]
        public IActionResult AddNewCustomer([FromBody] Customer customer)
        {

            var addedCustomer = _customerRepo.AddCustomer(customer);
            if (!addedCustomer)
            {
                return BadRequest();
            }
      
            return CreatedAtAction("GetCustomerId", new { id = customer.CustomerSsnId }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult EditBook(int id, [FromBody] Customer customer)
        {

            try
            {
                if (id != customer.CustomerSsnId || customer.CustomerId != 0)
                {
                    return BadRequest();
                }
                var isEdited = _customerRepo.EditCustomer(customer);
                return NoContent();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // DELETE <customer>/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteByID(int id)
        {
            var customer = _customerRepo.DeleteCustomerID(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }


    }
}
