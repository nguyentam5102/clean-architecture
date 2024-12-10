using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Domain.Interface.Services
{
    public interface ILogService
    {
        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message);
    }
}
