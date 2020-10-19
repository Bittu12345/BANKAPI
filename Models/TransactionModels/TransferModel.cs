using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBankAPI_1.Models.TransactionModels
{
    public class TransferModel
    {
        public int SourceAccountID { get; set; }
        public int TargetAccountID { get; set; }
        public int SourcePrevBalance { get; set; }
        public int TargetPrevBalance { get; set; }
        public int SourceCurrentBalance { get; set; }
        public int TargetCurrentBalance { get; set; }
    }
}
