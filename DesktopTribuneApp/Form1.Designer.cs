namespace DesktopTribuneApp
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
            SuspendLayout();
            // 
            // btnDB
            // 
            btnDB.Location = new Point(59, 34);
            btnDB.Name = "btnDB";
            btnDB.Size = new Size(192, 23);
            btnDB.TabIndex = 0;
            btnDB.Text = "Check DB Connection";
            btnDB.UseVisualStyleBackColor = true;
            btnDB.Click += btnDB_Click;
            // 
            // lblDB
            // 
            lblDB.AutoSize = true;
            lblDB.Location = new Point(288, 38);
            lblDB.Name = "lblDB";
            lblDB.Size = new Size(125, 15);
            lblDB.TabIndex = 1;
            lblDB.Text = "Status will appear here";
            lblDB.UseWaitCursor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblDB);
            Controls.Add(btnDB);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDB;
        private Label lblDB;
    }
}
