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
            lblStatus = new Label();
            btncheck = new Button();
            btnCheckDb = new Button();
            lblDbStatus = new Label();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(332, 72);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(125, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status will appear here";
            // 
            // btncheck
            // 
            btncheck.Location = new Point(39, 68);
            btncheck.Name = "btncheck";
            btncheck.Size = new Size(201, 23);
            btncheck.TabIndex = 1;
            btncheck.Text = "Check Server";
            btncheck.UseVisualStyleBackColor = true;
            btncheck.Click += btncheck_Click;
            // 
            // btnCheckDb
            // 
            btnCheckDb.Location = new Point(39, 127);
            btnCheckDb.Name = "btnCheckDb";
            btnCheckDb.Size = new Size(201, 23);
            btnCheckDb.TabIndex = 3;
            btnCheckDb.Text = "Check DB Connection";
            btnCheckDb.UseVisualStyleBackColor = true;
            btnCheckDb.Click += btnCheckDb_Click;
            // 
            // lblDbStatus
            // 
            lblDbStatus.AutoSize = true;
            lblDbStatus.Location = new Point(332, 131);
            lblDbStatus.Name = "lblDbStatus";
            lblDbStatus.Size = new Size(125, 15);
            lblDbStatus.TabIndex = 2;
            lblDbStatus.Text = "Status will appear here";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCheckDb);
            Controls.Add(lblDbStatus);
            Controls.Add(btncheck);
            Controls.Add(lblStatus);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Button btncheck;
        private Button btnCheckDb;
        private Label lblDbStatus;
    }
}
