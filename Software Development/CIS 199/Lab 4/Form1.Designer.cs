namespace Lab4
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hsGPALbl = new System.Windows.Forms.Label();
            this.admissionsTSLbl = new System.Windows.Forms.Label();
            this.hsGPATxtBox = new System.Windows.Forms.TextBox();
            this.admissionsTSTxtBox = new System.Windows.Forms.TextBox();
            this.applicationStatusLbl = new System.Windows.Forms.Label();
            this.applicationStatusOutLbl = new System.Windows.Forms.Label();
            this.runningTotalLbl = new System.Windows.Forms.Label();
            this.runningTotalOutLbl = new System.Windows.Forms.Label();
            this.enterBtn = new System.Windows.Forms.Button();
            this.runningRejectTotalLbl = new System.Windows.Forms.Label();
            this.runningRejectedTotalLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hsGPALbl
            // 
            this.hsGPALbl.AutoSize = true;
            this.hsGPALbl.Location = new System.Drawing.Point(117, 50);
            this.hsGPALbl.Name = "hsGPALbl";
            this.hsGPALbl.Size = new System.Drawing.Size(137, 20);
            this.hsGPALbl.TabIndex = 0;
            this.hsGPALbl.Text = "High School GPA:";
            // 
            // admissionsTSLbl
            // 
            this.admissionsTSLbl.AutoSize = true;
            this.admissionsTSLbl.Location = new System.Drawing.Point(79, 93);
            this.admissionsTSLbl.Name = "admissionsTSLbl";
            this.admissionsTSLbl.Size = new System.Drawing.Size(175, 20);
            this.admissionsTSLbl.TabIndex = 1;
            this.admissionsTSLbl.Text = "Admissions Test Score:";
            // 
            // hsGPATxtBox
            // 
            this.hsGPATxtBox.Location = new System.Drawing.Point(293, 47);
            this.hsGPATxtBox.Name = "hsGPATxtBox";
            this.hsGPATxtBox.Size = new System.Drawing.Size(100, 26);
            this.hsGPATxtBox.TabIndex = 2;
            // 
            // admissionsTSTxtBox
            // 
            this.admissionsTSTxtBox.Location = new System.Drawing.Point(293, 90);
            this.admissionsTSTxtBox.Name = "admissionsTSTxtBox";
            this.admissionsTSTxtBox.Size = new System.Drawing.Size(100, 26);
            this.admissionsTSTxtBox.TabIndex = 3;
            // 
            // applicationStatusLbl
            // 
            this.applicationStatusLbl.AutoSize = true;
            this.applicationStatusLbl.Location = new System.Drawing.Point(112, 244);
            this.applicationStatusLbl.Name = "applicationStatusLbl";
            this.applicationStatusLbl.Size = new System.Drawing.Size(142, 20);
            this.applicationStatusLbl.TabIndex = 4;
            this.applicationStatusLbl.Text = "Application Status:";
            // 
            // applicationStatusOutLbl
            // 
            this.applicationStatusOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.applicationStatusOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.applicationStatusOutLbl.Location = new System.Drawing.Point(293, 243);
            this.applicationStatusOutLbl.Name = "applicationStatusOutLbl";
            this.applicationStatusOutLbl.Size = new System.Drawing.Size(100, 26);
            this.applicationStatusOutLbl.TabIndex = 5;
            // 
            // runningTotalLbl
            // 
            this.runningTotalLbl.AutoSize = true;
            this.runningTotalLbl.Location = new System.Drawing.Point(104, 288);
            this.runningTotalLbl.Name = "runningTotalLbl";
            this.runningTotalLbl.Size = new System.Drawing.Size(150, 20);
            this.runningTotalLbl.TabIndex = 6;
            this.runningTotalLbl.Text = "Accepted Students:";
            // 
            // runningTotalOutLbl
            // 
            this.runningTotalOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.runningTotalOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.runningTotalOutLbl.Location = new System.Drawing.Point(293, 287);
            this.runningTotalOutLbl.Name = "runningTotalOutLbl";
            this.runningTotalOutLbl.Size = new System.Drawing.Size(100, 26);
            this.runningTotalOutLbl.TabIndex = 7;
            // 
            // enterBtn
            // 
            this.enterBtn.Location = new System.Drawing.Point(217, 157);
            this.enterBtn.Name = "enterBtn";
            this.enterBtn.Size = new System.Drawing.Size(94, 39);
            this.enterBtn.TabIndex = 8;
            this.enterBtn.Text = "Enter";
            this.enterBtn.UseVisualStyleBackColor = true;
            this.enterBtn.Click += new System.EventHandler(this.enterBtn_Click);
            // 
            // runningRejectTotalLbl
            // 
            this.runningRejectTotalLbl.AutoSize = true;
            this.runningRejectTotalLbl.Location = new System.Drawing.Point(108, 331);
            this.runningRejectTotalLbl.Name = "runningRejectTotalLbl";
            this.runningRejectTotalLbl.Size = new System.Drawing.Size(146, 20);
            this.runningRejectTotalLbl.TabIndex = 9;
            this.runningRejectTotalLbl.Text = "Rejected Students:";
            // 
            // runningRejectedTotalLbl
            // 
            this.runningRejectedTotalLbl.BackColor = System.Drawing.SystemColors.Window;
            this.runningRejectedTotalLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.runningRejectedTotalLbl.Location = new System.Drawing.Point(293, 330);
            this.runningRejectedTotalLbl.Name = "runningRejectedTotalLbl";
            this.runningRejectedTotalLbl.Size = new System.Drawing.Size(100, 26);
            this.runningRejectedTotalLbl.TabIndex = 10;
            // 
            // Form1
            // 
            this.AcceptButton = this.enterBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 390);
            this.Controls.Add(this.runningRejectedTotalLbl);
            this.Controls.Add(this.runningRejectTotalLbl);
            this.Controls.Add(this.enterBtn);
            this.Controls.Add(this.runningTotalOutLbl);
            this.Controls.Add(this.runningTotalLbl);
            this.Controls.Add(this.applicationStatusOutLbl);
            this.Controls.Add(this.applicationStatusLbl);
            this.Controls.Add(this.admissionsTSTxtBox);
            this.Controls.Add(this.hsGPATxtBox);
            this.Controls.Add(this.admissionsTSLbl);
            this.Controls.Add(this.hsGPALbl);
            this.Name = "Form1";
            this.Text = "Lab 4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hsGPALbl;
        private System.Windows.Forms.Label admissionsTSLbl;
        private System.Windows.Forms.TextBox hsGPATxtBox;
        private System.Windows.Forms.TextBox admissionsTSTxtBox;
        private System.Windows.Forms.Label applicationStatusLbl;
        private System.Windows.Forms.Label applicationStatusOutLbl;
        private System.Windows.Forms.Label runningTotalLbl;
        private System.Windows.Forms.Label runningTotalOutLbl;
        private System.Windows.Forms.Button enterBtn;
        private System.Windows.Forms.Label runningRejectTotalLbl;
        private System.Windows.Forms.Label runningRejectedTotalLbl;
    }
}

