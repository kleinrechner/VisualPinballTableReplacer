namespace VisualPinballTableReplacer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnNewFile = new System.Windows.Forms.Button();
            this.btnOldFile = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtPinUpFolder = new System.Windows.Forms.TextBox();
            this.lblPinUpFolder = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectPinUpFolder = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnNewFile
            // 
            this.btnNewFile.AllowDrop = true;
            this.btnNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewFile.Location = new System.Drawing.Point(10, 49);
            this.btnNewFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(472, 367);
            this.btnNewFile.TabIndex = 2;
            this.btnNewFile.Text = "New table file";
            this.btnNewFile.UseVisualStyleBackColor = true;
            this.btnNewFile.Click += new System.EventHandler(this.btnFileToReplace_Click);
            this.btnNewFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnFileToReplace_DragDrop);
            this.btnNewFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnFileToReplace_DragEnter);
            // 
            // btnOldFile
            // 
            this.btnOldFile.AllowDrop = true;
            this.btnOldFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOldFile.Location = new System.Drawing.Point(488, 49);
            this.btnOldFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOldFile.Name = "btnOldFile";
            this.btnOldFile.Size = new System.Drawing.Size(315, 367);
            this.btnOldFile.TabIndex = 3;
            this.btnOldFile.Text = "Old table file";
            this.btnOldFile.UseVisualStyleBackColor = true;
            this.btnOldFile.Click += new System.EventHandler(this.btnSourceFile_Click);
            this.btnOldFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnSourceFile_DragDrop);
            this.btnOldFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnSourceFile_DragEnter);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(628, 424);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(175, 22);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Start";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtPinUpFolder
            // 
            this.txtPinUpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPinUpFolder.Location = new System.Drawing.Point(10, 24);
            this.txtPinUpFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPinUpFolder.Name = "txtPinUpFolder";
            this.txtPinUpFolder.Size = new System.Drawing.Size(673, 23);
            this.txtPinUpFolder.TabIndex = 5;
            this.txtPinUpFolder.Text = "C:\\vPinball\\PinUPSystem";
            this.txtPinUpFolder.TextChanged += new System.EventHandler(this.txtPinUpFolder_TextChanged);
            // 
            // lblPinUpFolder
            // 
            this.lblPinUpFolder.AutoSize = true;
            this.lblPinUpFolder.Location = new System.Drawing.Point(10, 7);
            this.lblPinUpFolder.Name = "lblPinUpFolder";
            this.lblPinUpFolder.Size = new System.Drawing.Size(75, 15);
            this.lblPinUpFolder.TabIndex = 6;
            this.lblPinUpFolder.Text = "PinUp Folder";
            // 
            // btnSelectPinUpFolder
            // 
            this.btnSelectPinUpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPinUpFolder.Location = new System.Drawing.Point(688, 24);
            this.btnSelectPinUpFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectPinUpFolder.Name = "btnSelectPinUpFolder";
            this.btnSelectPinUpFolder.Size = new System.Drawing.Size(115, 22);
            this.btnSelectPinUpFolder.TabIndex = 7;
            this.btnSelectPinUpFolder.Text = "Select PinUp Folder";
            this.btnSelectPinUpFolder.UseVisualStyleBackColor = true;
            this.btnSelectPinUpFolder.Click += new System.EventHandler(this.btnSelectPinUpFolder_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 455);
            this.Controls.Add(this.btnSelectPinUpFolder);
            this.Controls.Add(this.lblPinUpFolder);
            this.Controls.Add(this.txtPinUpFolder);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnOldFile);
            this.Controls.Add(this.btnNewFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMain";
            this.Text = "Replace VirtualPinball table";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnNewFile;
        private Button btnOldFile;
        private Button btnRun;
        private TextBox txtPinUpFolder;
        private Label lblPinUpFolder;
        private FolderBrowserDialog folderBrowserDialog;
        private Button btnSelectPinUpFolder;
        private OpenFileDialog openFileDialog;
    }
}