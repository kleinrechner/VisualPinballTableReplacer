using VisualPinballTableReplacer.Services;

namespace VisualPinballTableReplacer
{
    public partial class frmMain : Form
    {
        private string oldFilePath = string.Empty;
        private string newFilePath = string.Empty;
        
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnFileToReplace_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void btnSourceFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void btnFileToReplace_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            SetNewFilePath(files.First());
        }

        private void btnFileToReplace_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetNewFilePath(openFileDialog.FileName);
            }
        }

        private void btnSourceFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            SetOldFilePath(files.First());
        }

        private void btnSourceFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetOldFilePath(openFileDialog.FileName);
            }
        }

        private void btnSelectPinUpFolder_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
               txtPinUpFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void SetNewFilePath(string path)
        {
            newFilePath = path;
            btnNewFile.Text = Path.GetFileName(newFilePath);
        }

        private void SetOldFilePath(string path)
        {
            oldFilePath = path;
            btnOldFile.Text = Path.GetFileName(oldFilePath);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var _replaceVirtualPinballTableService = new ReplaceVirtualPinballTableService();
            _replaceVirtualPinballTableService.ReplaceTable(txtPinUpFolder.Text, oldFilePath, newFilePath)
                .ContinueWith(x =>
                {
                    if (x.IsCompletedSuccessfully)
                    {
                        MessageBox.Show("Table replaced successfully!", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (x.Exception != null)
                    {
                        var exception = x.Exception.InnerException ?? x.Exception;
                        MessageBox.Show(exception.Message, "Replace table failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
        }
    }
}