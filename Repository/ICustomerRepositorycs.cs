using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;

namespace DotNetBankAPI_1.Repository
{
   public interface ICustomerRepositorycs
    {
        public bool AddCustomer(Customer customer);
        public List<Customer> GetCustomer(QueryParameters queryParameters, int id = 0);
        public bool EditCustomer(Customer customer);
        public void DeleteAllCustomer();
        public Customer DeleteCustomerID(int Id);
    }
}
