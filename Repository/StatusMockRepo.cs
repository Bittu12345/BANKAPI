using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;

namespace DotNetBankAPI_1.Repository
{
    public class StatusMockRepo : IStatusRepository
    {
        private DotNetBankAPIContext _context;

        public StatusMockRepo(DotNetBankAPIContext context)
        {
            _context = context;

        }
        public List<Status> GetStatuses()
        {
            return _context.Status.ToList();
        }

    }
}
