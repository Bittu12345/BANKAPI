using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNetBankAPI_1.Models
{
    public partial class Status
    {
        public int SsnId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public string Status1 { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Balance { get; set; }
        [JsonIgnore]
        public virtual Customer Ssn { get; set; }
    }
}
