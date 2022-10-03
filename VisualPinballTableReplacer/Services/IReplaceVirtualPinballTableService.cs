using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPinballTableReplacer.Services
{
    public interface IReplaceVirtualPinballTableService
    {
        Task ReplaceTable(string text, string sourceFilePath, string targetFilePath);
    }
}
