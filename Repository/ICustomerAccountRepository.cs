using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;
using DotNetBankAPI_1.Models.TransactionModels;

namespace DotNetBankAPI_1.Repository
{
    public interface ICustomerAccountRepository
    {
        public List<CustomerAccountDetails> getCustomerAccount(int id);
        public List<Transaction> Deposit(int id, int amount, string accountType);
        public List<Transaction> WithDraw(int id, int amount, string accountType);
        public List<TransferModel> Transfer(int sourceaccountid, int targetaccountid, int amount);

    }
}
