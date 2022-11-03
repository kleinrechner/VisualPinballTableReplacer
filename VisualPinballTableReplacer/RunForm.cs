using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualPinballTableReplacer.Services;

namespace VisualPinballTableReplacer
{
    public partial class RunForm : Form
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public string? PinUpFolderPath { get; set; }

        public string? OldFilePath { get; set; }

        public string? NewFilePath { get; set; }


        public RunForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RunForm_Load(object sender, EventArgs e)
        {
            btnCancel.Enabled = true;
            btnClose.Enabled = false;

            var _formLoggingService = new FormLoggingService(logTextBox);
            var _replaceVirtualPinballTableService = new ReplaceVirtualPinballTableService(_formLoggingService);
            Task.Run(() => _replaceVirtualPinballTableService.ReplaceTable(PinUpFolderPath, OldFilePath, NewFilePath, _cancellationTokenSource.Token)
                                .ContinueWith(x => {      
                                    if (_cancellationTokenSource.IsCancellationRequested)
                                    {
                                        _formLoggingService.LogInformation("Task has been cancelled");
                                    }

                                    Invoke(() =>
                                    {
                                        btnCancel.Enabled = false;
                                        btnClose.Enabled = true;
                                    });
                                }));
        }
    }
}
