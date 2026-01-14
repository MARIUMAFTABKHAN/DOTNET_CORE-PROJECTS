namespace NetworkCheckApp
{
    partial class Form1
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
            btnDB = new Button();
            lblDB = new Label();
            lblNetworkStatus = new Label();
            btnchecknetwork = new Button();
            lblFileStatus = new Label();
            btnCheckFile = new Button();
            txtArchiveId = new TextBox();
            lblStatus = new Label();
            btnPlay = new Button();
            btnBuildIndex = new Button();
            SuspendLayout();
            // 
            // btnDB
            // 
            btnDB.Location = new Point(72, 45);
            btnDB.Name = "btnDB";
            btnDB.Size = new Size(175, 30);
            btnDB.TabIndex = 0;
            btnDB.Text = "Check DB Connection";
            btnDB.UseVisualStyleBackColor = true;
            btnDB.Click += btnDB_Click;
            // 
            // lblDB
            // 
            lblDB.AutoSize = true;
            lblDB.Location = new Point(264, 53);
            lblDB.Name = "lblDB";
            lblDB.Size = new Size(125, 15);
            lblDB.TabIndex = 1;
            lblDB.Text = "Status will appear here";
            // 
            // lblNetworkStatus
            // 
            lblNetworkStatus.AutoSize = true;
            lblNetworkStatus.Location = new Point(264, 120);
            lblNetworkStatus.Name = "lblNetworkStatus";
            lblNetworkStatus.Size = new Size(125, 15);
            lblNetworkStatus.TabIndex = 3;
            lblNetworkStatus.Text = "Status will appear here";
            // 
            // btnchecknetwork
            // 
            btnchecknetwork.Location = new Point(72, 112);
            btnchecknetwork.Name = "btnchecknetwork";
            btnchecknetwork.Size = new Size(175, 30);
            btnchecknetwork.TabIndex = 2;
            btnchecknetwork.Text = "Check Network Connection";
            btnchecknetwork.UseVisualStyleBackColor = true;
            btnchecknetwork.Click += btnchecknetwork_Click;
            // 
            // lblFileStatus
            // 
            lblFileStatus.AutoSize = true;
            lblFileStatus.Location = new Point(264, 193);
            lblFileStatus.Name = "lblFileStatus";
            lblFileStatus.Size = new Size(125, 15);
            lblFileStatus.TabIndex = 5;
            lblFileStatus.Text = "Status will appear here";
            // 
            // btnCheckFile
            // 
            btnCheckFile.Location = new Point(72, 185);
            btnCheckFile.Name = "btnCheckFile";
            btnCheckFile.Size = new Size(175, 30);
            btnCheckFile.TabIndex = 4;
            btnCheckFile.Text = "Check File";
            btnCheckFile.UseVisualStyleBackColor = true;
            btnCheckFile.Click += btnCheckFile_Click;
            // 
            // txtArchiveId
            // 
            txtArchiveId.Location = new Point(264, 247);
            txtArchiveId.Name = "txtArchiveId";
            txtArchiveId.Size = new Size(100, 23);
            txtArchiveId.TabIndex = 6;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(264, 319);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(125, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status will appear here";
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(72, 311);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(175, 30);
            btnPlay.TabIndex = 7;
            btnPlay.Text = "Play Video";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnBuildIndex
            // 
            btnBuildIndex.Location = new Point(72, 383);
            btnBuildIndex.Name = "btnBuildIndex";
            btnBuildIndex.Size = new Size(175, 30);
            btnBuildIndex.TabIndex = 9;
            btnBuildIndex.Text = "Build File Index";
            btnBuildIndex.UseVisualStyleBackColor = true;
            btnBuildIndex.Click += btnBuildIndex_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBuildIndex);
            Controls.Add(lblStatus);
            Controls.Add(btnPlay);
            Controls.Add(txtArchiveId);
            Controls.Add(lblFileStatus);
            Controls.Add(btnCheckFile);
            Controls.Add(lblNetworkStatus);
            Controls.Add(btnchecknetwork);
            Controls.Add(lblDB);
            Controls.Add(btnDB);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDB;
        private Label lblDB;
        private Label lblNetworkStatus;
        private Button btnchecknetwork;
        private Label lblFileStatus;
        private Button btnCheckFile;
        private TextBox txtArchiveId;
        private Label lblStatus;
        private Button btnPlay;
        private Button btnBuildIndex;
    }
}
