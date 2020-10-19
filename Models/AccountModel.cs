using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace DotNetBankAPI_1.Models
{
    public class AccountModel
    {
        public int CustomerID { get; set; }
        public string AccountType { get; set; }
        public int Deposit { get; set; }

    }
}
