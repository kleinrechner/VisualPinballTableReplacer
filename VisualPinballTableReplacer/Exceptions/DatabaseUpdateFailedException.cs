using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPinballTableReplacer.Exceptions
{
    public class DatabaseUpdateFailedException : Exception
    {
        public DatabaseUpdateFailedException(string dbFilePath, Exception innerException) : base($"Failed to update database file '{dbFilePath}': {innerException.Message}", innerException)
        {
        }
    }
}
