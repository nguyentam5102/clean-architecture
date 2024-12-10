using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.Domain.Interface.Services;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BookManagement.Application.Services
{
    public class LogService : ILogService
    {
        public void LogInfo(string message)
        {
            Log.Logger.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Logger.Warning(message);
        }

        public void LogError(string message) 
        {
            Log.Logger.Error(message);
        }
    }
}
