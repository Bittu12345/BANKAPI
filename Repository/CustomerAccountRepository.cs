using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;
using DotNetBankAPI_1.Models.TransactionModels;
using Microsoft.EntityFrameworkCore;

namespace DotNetBankAPI_1.Repository
{
    public class CustomerAccountRepository : ICustomerAccountRepository
    {
        private DotNetBankAPIContext _context;

        public CustomerAccountRepository(DotNetBankAPIContext context)
        {
            _context = context;

        }
        public List<CustomerAccountDetails> getCustomerAccount(int id)
        {
            IQueryable<Status> customerAccount = _context.Status;
            DotNetBankAPIContext cs = new DotNetBankAPIContext();
            var list = customerAccount
                         .Where(i => i.CustomerId == id)
                      .Select(i => new CustomerAccountDetails
                         {
                              CustomerId = i.CustomerId,
                              AccountId = i.AccountId,
                              AccountType = i.AccountType,
                              Balance = i.Balance
                          }).ToList();
              return list; 
        }
        public List<Transaction> Deposit(int id, int amount, string accountType)
        {
           // Balance bal = new Balance();
            IQueryable<Status> customerAccount = _context.Status;
            int currentBalance;
            var prevBalance = customerAccount
                                .Where(i => i.CustomerId == id && i.AccountType == accountType)
                                 .Select(i => i.Balance).FirstOrDefault();
           
            currentBalance = prevBalance + amount;
            var item = customerAccount
                         .Where(i => i.CustomerId == id)
                         .Select(i => new Transaction
                         {
                             CustomerID = i.CustomerId,
                             AccountID = i.AccountId,
                             Date = DateTime.Now,
                             PrevBalance = prevBalance,
                             currentBalance = currentBalance
                         }).ToList();

            var bal = (from i in customerAccount where i.CustomerId == id select i.Balance).FirstOrDefault();
            bal = currentBalance;
            var a = customerAccount.FirstOrDefault();
            a.Balance = currentBalance;
            _context.SaveChanges();
            return item;
        }
        
        public List<Transaction> WithDraw(int id, int amount, string accountType)
        {
            IQueryable<Status> customerAccount = _context.Status;
            int currentBalance;
            var prevBalance = customerAccount
                                .Where(i => i.CustomerId == id && i.AccountType == accountType)
                                 .Select(i => i.Balance).FirstOrDefault();

            currentBalance = prevBalance - amount;
            var item = customerAccount
                         .Where(i => i.CustomerId == id)
                         .Select(i => new Transaction
                         {
                             CustomerID = i.CustomerId,
                             AccountID = i.AccountId,
                             Date = DateTime.Now,
                             PrevBalance = prevBalance,
                             currentBalance = currentBalance
                         }).ToList();
           
            var bal = (from i in customerAccount where i.CustomerId == id select i.Balance).FirstOrDefault();
            bal = currentBalance;
            var a = customerAccount.FirstOrDefault();
            a.Balance = currentBalance;
            
            _context.SaveChanges();
             return item;
        }

        public List<TransferModel> Transfer(int sourceaccountid, int targetaccountid, int amount)
        {
            IQueryable<Status> customerAccount = _context.Status;

            var SourcePrevBalance = (from i in customerAccount where i.AccountId == sourceaccountid select i.Balance).FirstOrDefault();
            var TargetPrevBalance = (from i in customerAccount where i.AccountId == targetaccountid select i.Balance).FirstOrDefault();
            int SourceCurrentBalance, TargetCurrentBalance;
            SourceCurrentBalance = SourcePrevBalance - amount;
            TargetCurrentBalance = TargetPrevBalance + amount;
            var transactiondetails = customerAccount
                         .Where(i => i.AccountId == sourceaccountid)
                         .Select(i => new TransferModel
                         {
                             SourceAccountID = sourceaccountid,
                             TargetAccountID = targetaccountid,
                             SourcePrevBalance = SourcePrevBalance,
                             TargetPrevBalance = TargetPrevBalance,
                             SourceCurrentBalance = SourceCurrentBalance,
                             TargetCurrentBalance = TargetCurrentBalance
                         }).ToList();

            //var sourcebal = (from i in customerAccount where i.AccountId == sourceaccountid select i.Balance).FirstOrDefault();
            //sourcebal = SourceCurrentBalance;
            var a = customerAccount.Where(i => i.AccountId == sourceaccountid).FirstOrDefault();
            a.Balance = SourceCurrentBalance;
            _context.SaveChanges();

            //var targetbal = (from i in customerAccount where i.AccountId == targetaccountid select i.Balance).FirstOrDefault();
            //targetbal = TargetCurrentBalance;
            var b = customerAccount.Where(i => i.AccountId == targetaccountid).FirstOrDefault();
            b.Balance = TargetCurrentBalance;
            _context.SaveChanges();

            return transactiondetails;

        }
    }
}
