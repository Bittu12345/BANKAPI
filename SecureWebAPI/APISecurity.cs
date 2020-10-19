using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;

namespace DotNetBankAPI_1.SecureWebAPI
{
    public class APISecurity
    {
        public static UserStore Login(string username)
        {
            using (var context = new DotNetBankAPIContext()) 
            {
                var user = context.UserStore.Where(user => user.Login == username);
                if (user == null)
                {
                    return null;
                }
                return user.FirstOrDefault();
            }
        }
    }
}
