using System;
using System.Collections.Generic;

namespace DotNetBankAPI_1.Models
{
    public partial class UserStore
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
