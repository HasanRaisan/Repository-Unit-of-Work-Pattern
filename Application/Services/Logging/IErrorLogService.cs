using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Logging
{
    public interface IErrorLogService
    {
        Task LogAsync(Exception Ex, string Path, string Method);
    }
}
