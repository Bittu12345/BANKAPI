using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;

namespace DotNetBankAPI_1.Repository
{
    public interface IStatusRepository
    {
        public List<Status> GetStatuses();
    }
}
