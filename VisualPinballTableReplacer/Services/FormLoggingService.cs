using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualPinballTableReplacer.Services
{
    public class FormLoggingService : IFormLoggingService
    {
        private readonly RichTextBox _richTextBox;

        public FormLoggingService(RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
        }

        public void LogException(Exception exception)
        {
            Handle(() => AddLine(exception.Message, CaseEnum.Exception));
        }

        public void LogInformation(string value)
        {
            Handle(() => AddLine(value, CaseEnum.Info));
        }

        public void LogSucceeded(string value)
        {
            Handle(() => AddLine(value, CaseEnum.Success));
        }

        public void LogWarning(string value)
        {
            Handle(() => AddLine(value, CaseEnum.Warning));
        }

        private void Handle(Action action)
        {
            if (_richTextBox.IsHandleCreated && _richTextBox.InvokeRequired)
            {
                _richTextBox.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
        
        private void AddLine(string value, CaseEnum textCase)
        {
            _richTextBox.SelectionStart = _richTextBox.TextLength;
            _richTextBox.SelectionLength = 0;

            switch (textCase)
            {
                case CaseEnum.Success:
                    _richTextBox.SelectionColor = Color.Green;
                    _richTextBox.AppendText("✔");
                    break;
                case CaseEnum.Exception:
                    _richTextBox.SelectionColor = Color.Red;
                    _richTextBox.AppendText("✖");
                    break;
                case CaseEnum.Warning:
                    _richTextBox.SelectionColor = Color.Orange;
                    _richTextBox.AppendText("⚠");
                    break;
                case CaseEnum.Info:
                    //richTextBox.SelectionColor = Color.Yellow;
                    _richTextBox.AppendText("-");
                    break;
                default:
                    break;
            }
            _richTextBox.SelectionColor = _richTextBox.ForeColor;

            _richTextBox.AppendText(value + Environment.NewLine);
            _richTextBox.ScrollToCaret();
        }

        private enum CaseEnum
        {
            Info,
            Warning,
            Success,
            Exception
        }
    }
}
