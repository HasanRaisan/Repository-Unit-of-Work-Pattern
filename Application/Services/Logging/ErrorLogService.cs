using Castle.Core.Logging;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Application.Services.Logging
{
    public class ErrorLogService : IErrorLogService
    {

        private readonly ILogger<ErrorLogService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ErrorLogService(IUnitOfWork unitOfWork, ILogger<ErrorLogService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task LogAsync(Exception ex, string Path, string Method)
        {
            try
            {
                var log = new ErrorLogEntity
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace ?? "",
                    Path = Path,
                    Method = Method,
                    CreatedAt = DateTime.UtcNow
                };
                _unitOfWork.Clear();
                await _unitOfWork.ErrorLoggers.AddLogAsync(log);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception loggingEx)
            {
                // Log to ILogger if database logging fails
                _logger.LogError(loggingEx, "Failed to log error to database. Original error: {OriginalMessage}", ex.Message);
            }
        }
    }
}
