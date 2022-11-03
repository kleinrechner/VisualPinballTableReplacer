using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualPinballTableReplacer.Exceptions;

namespace VisualPinballTableReplacer.Services
{
    public interface IFormLoggingService
    {
        void LogException(Exception exception);
        
        void LogInformation(string value);
        
        void LogSucceeded(string value);
        
        void LogWarning(string value);
    }
}
