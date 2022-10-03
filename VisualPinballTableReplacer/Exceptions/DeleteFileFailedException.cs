using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPinballTableReplacer.Exceptions
{
    public class DeleteFileFailedException : Exception
    {
        public DeleteFileFailedException(string fileName, Exception innerException) : base($"Failed to remove file '{fileName}': {innerException.Message}", innerException)
        {
        }
    }
}
