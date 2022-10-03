using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPinballTableReplacer.Exceptions
{
    public class CopyFileFailedException : Exception
    {
        public CopyFileFailedException(string originalFilePath, string newFilePath, Exception exc) : base($"Failed to copy file '{originalFilePath}' to '{newFilePath}': {exc.Message}", exc)
        {

        }
    }
}
