using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPinballTableReplacer.Exceptions
{
    public class RenameFileFailedException : Exception
    {
        public RenameFileFailedException(string originalFilePath, string newFilePath, Exception exc) : base($"Failed to rename file '{originalFilePath}' to '{newFilePath}': {exc.Message}", exc)
        {

        }
    }
}
