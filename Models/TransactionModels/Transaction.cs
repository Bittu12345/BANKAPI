using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBankAPI_1.Models.TransactionModels
{
    public class Transaction
    {
        public int CustomerID { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public int PrevBalance { get; set; }
        public int currentBalance { get; set; }
    }
}
