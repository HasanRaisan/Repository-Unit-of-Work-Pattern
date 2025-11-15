using Infrastructure.Data.Entities;
using Infrastructure.UnitOfWork;

namespace Application.Services.Logging
{
    public class ErrorLogService : IErrorLogService
    {

        public ErrorLogService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task LogAsync(Exception ex, string Path, string Method)
        {
            var log = new ErrorLogEntity
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace?? "",
                Path = Path,
                Method = Method,
                CreatedAt = DateTime.UtcNow
            };

            await UnitOfWork.ErrorLoggers.AddLogAsync(log);
        }
    }
}
