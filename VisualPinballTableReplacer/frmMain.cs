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

        private void txtPinUpFolder_TextChanged(object sender, EventArgs e)
        {
            ValidatePath();
        }

        private void SetNewFilePath(string path)
        {
            newFilePath = path;
            btnNewFile.Text = Path.GetFileName(newFilePath);
            ValidatePath();
        }

        private void SetOldFilePath(string path)
        {
            oldFilePath = path;
            btnOldFile.Text = Path.GetFileName(oldFilePath);
            ValidatePath();
        }

        private void ValidatePath()
        {
            btnRun.Enabled = !string.IsNullOrWhiteSpace(oldFilePath) && !string.IsNullOrWhiteSpace(newFilePath) && !string.IsNullOrWhiteSpace(txtPinUpFolder.Text);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var pinUpFolderPath = txtPinUpFolder.Text;
            var valid = true;

            if (!File.Exists(oldFilePath))
            {
                MessageBox.Show($"File {oldFilePath} could not be found!", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (!File.Exists(newFilePath))
            {
                MessageBox.Show($"File {newFilePath} could not be found!", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            var pupDatabasePath = Path.Combine(pinUpFolderPath, "PUPDatabase.db");
            if (!File.Exists(pupDatabasePath))
            {
                MessageBox.Show($"Database file {pupDatabasePath} could not be found!", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (!Directory.Exists(pinUpFolderPath))
            {
                MessageBox.Show($"Folder {pinUpFolderPath} could not be found!", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (Path.GetFileName(oldFilePath) == Path.GetFileName(newFilePath))
            {
                MessageBox.Show($"Target and source file name are same, you don't need this tool, just copy the file into the target folder by Windows Explorer.", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valid = false;
            }

            if (valid)
            {
                btnRun.Enabled = false;
                var _replaceVirtualPinballTableService = new ReplaceVirtualPinballTableService();
                _replaceVirtualPinballTableService.ReplaceTable(pinUpFolderPath, oldFilePath, newFilePath)
                    .ContinueWith(x =>
                    {                        
                        Invoke(() => btnRun.Enabled = true);
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
}