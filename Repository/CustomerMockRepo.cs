using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetBankAPI_1.Repository
{
    public class CustomerMockRepo : ICustomerRepositorycs
    {
        private DotNetBankAPIContext _context;

        public CustomerMockRepo(DotNetBankAPIContext context)
        {
            _context = context;
            
        }
        public bool AddCustomer(Customer customer)
        {
            try
            {
                _context.Customer.Add(customer);               
                _context.SaveChanges();
            
                var createdCustomer = _context.Customer.Where(x => x.CustomerSsnId == customer.CustomerSsnId).FirstOrDefault();
                var status = new Status();
                status.CustomerId = createdCustomer.CustomerId;
                status.SsnId = createdCustomer.CustomerSsnId;
                status.Status1 = "Created Customer and checkings account";
                status.Message = $"Create At {DateTime.Now.Date}";
                status.LastUpdated = DateTime.Now;


                //NEED to check with the account creation process on how accountid is generated. 
                //This account Number can be used.

                status.AccountId = customer.CustomerId +  customer.CustomerSsnId;

                //We're doing the S  for savings, C  for current// by default current will be created.

                status.AccountType = "C";

                _context.Status.Add(status);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DeleteAllCustomer()
        {
            throw new NotImplementedException();
        }

        public Customer DeleteCustomerID(int Id)
        {
            //Deleting the customer we need to delete the status of the customr before as there is Foreign key relation.

            var status = _context.Status.Where(s => s.SsnId == Id).ToList();
            status.ForEach(x => _context.Status.Remove(x));
            var customer = _context.Customer.Find(Id);
            if (customer == null)
            {
                return customer;
            }
            _context.Customer.Remove(customer);
            _context.SaveChanges();



            return customer;
        }

        public bool EditCustomer(Customer customer)
        {
            //we shouldn;t be sending in the customer id here..as it's a auto generated.
            try
            {               
                _context.Update(customer).Property(x=>x.CustomerId).IsModified = false;
                _context.SaveChanges();

                //After edit is successfull we should update the status.to edited.


                var status = _context.Status.Where(s => s.SsnId == customer.CustomerSsnId).FirstOrDefault();

                status.Status1 = "Updated";
                status.Message = $"Updated the Customer at{DateTime.Today}";
                status.LastUpdated = DateTime.Now;
                _context.Update(status);
                _context.SaveChanges();

                return true;
            }
            catch (DBConcurrencyException e)
            {
                if (_context.Customer.Find(customer.CustomerSsnId) == null)
                {
                    return false;
                }
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Customer> GetCustomer(QueryParameters queryParameters, int id = 0)
        {
            IQueryable<Customer> customers = _context.Customer;

            if (id != 0)
            {
                return customers.Where(b => b.CustomerSsnId == id)?.ToList();
            }

            if (queryParameters?.customerID != null)
            {
                customers = customers.Where(x => x.CustomerId == queryParameters.customerID);
            }

            if (queryParameters?.customerSSN != null)
            {
                customers = customers.Where(x => x.CustomerSsnId == queryParameters.customerSSN);
            }

            return customers.OrderBy(x => x.CustomerName).ThenBy(x => x.Age).ToList();
        }
    }
}
