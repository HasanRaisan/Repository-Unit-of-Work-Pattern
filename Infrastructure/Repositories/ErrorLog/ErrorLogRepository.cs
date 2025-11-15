using Infrastructure.Data;
using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ErrorLog
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly AppDbContext _context;

        public ErrorLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(ErrorLogEntity log)
        {
            await _context.ErrorLogs.AddAsync(log);
        }
    }
}
