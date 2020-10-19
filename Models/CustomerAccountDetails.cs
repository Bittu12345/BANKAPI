using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBankAPI_1.Models
{
    public class CustomerAccountDetails
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set;}
        public  string AccountType { get; set; }
        public int Balance { get; set; }
    }
}
