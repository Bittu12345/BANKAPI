using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNetBankAPI_1.Models
{
    public partial class Customer
    {
      
        public Customer()
        {
          
            Status = new HashSet<Status>();
        }

        public int CustomerSsnId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Age { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [JsonIgnore]
        public virtual ICollection<Status> Status { get; set; }
    }
}
