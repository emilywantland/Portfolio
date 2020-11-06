namespace Program2
{
    partial class Prog2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prog2Form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.headofHouseRB = new System.Windows.Forms.RadioButton();
            this.singleRB = new System.Windows.Forms.RadioButton();
            this.marriedJointRB = new System.Windows.Forms.RadioButton();
            this.marriedSeparateRB = new System.Windows.Forms.RadioButton();
            this.resetBtn = new System.Windows.Forms.Button();
            this.calcBtn = new System.Windows.Forms.Button();
            this.amountDueOutputLbl = new System.Windows.Forms.Label();
            this.marginalOutputLbl = new System.Windows.Forms.Label();
            this.amountDueLbl = new System.Windows.Forms.Label();
            this.marginalLbl = new System.Windows.Forms.Label();
            this.taxIncomeTxt = new System.Windows.Forms.TextBox();
            this.taxIncomeLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.headofHouseRB);
            this.groupBox1.Controls.Add(this.singleRB);
            this.groupBox1.Controls.Add(this.marriedJointRB);
            this.groupBox1.Controls.Add(this.marriedSeparateRB);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(43, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 188);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filing Status";
            // 
            // headofHouseRB
            // 
            this.headofHouseRB.AutoSize = true;
            this.headofHouseRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headofHouseRB.Location = new System.Drawing.Point(19, 107);
            this.headofHouseRB.Name = "headofHouseRB";
            this.headofHouseRB.Size = new System.Drawing.Size(172, 24);
            this.headofHouseRB.TabIndex = 4;
            this.headofHouseRB.TabStop = true;
            this.headofHouseRB.Text = "Head of Household";
            this.headofHouseRB.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.headofHouseRB.UseVisualStyleBackColor = true;
            // 
            // singleRB
            // 
            this.singleRB.AutoSize = true;
            this.singleRB.Checked = true;
            this.singleRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleRB.Location = new System.Drawing.Point(19, 47);
            this.singleRB.Name = "singleRB";
            this.singleRB.Size = new System.Drawing.Size(78, 24);
            this.singleRB.TabIndex = 0;
            this.singleRB.TabStop = true;
            this.singleRB.Text = "Single";
            this.singleRB.UseVisualStyleBackColor = true;
            // 
            // marriedJointRB
            // 
            this.marriedJointRB.AutoSize = true;
            this.marriedJointRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marriedJointRB.Location = new System.Drawing.Point(19, 77);
            this.marriedJointRB.Name = "marriedJointRB";
            this.marriedJointRB.Size = new System.Drawing.Size(176, 24);
            this.marriedJointRB.TabIndex = 1;
            this.marriedJointRB.TabStop = true;
            this.marriedJointRB.Text = "Married Filing Jointly";
            this.marriedJointRB.UseVisualStyleBackColor = true;
            // 
            // marriedSeparateRB
            // 
            this.marriedSeparateRB.AutoSize = true;
            this.marriedSeparateRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marriedSeparateRB.Location = new System.Drawing.Point(19, 137);
            this.marriedSeparateRB.Name = "marriedSeparateRB";
            this.marriedSeparateRB.Size = new System.Drawing.Size(208, 24);
            this.marriedSeparateRB.TabIndex = 3;
            this.marriedSeparateRB.TabStop = true;
            this.marriedSeparateRB.Text = "Married Filing Separately";
            this.marriedSeparateRB.UseVisualStyleBackColor = true;
            // 
            // resetBtn
            // 
            this.resetBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.resetBtn.Location = new System.Drawing.Point(194, 293);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(107, 34);
            this.resetBtn.TabIndex = 31;
            this.resetBtn.Text = "Reset";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // calcBtn
            // 
            this.calcBtn.Location = new System.Drawing.Point(64, 293);
            this.calcBtn.Name = "calcBtn";
            this.calcBtn.Size = new System.Drawing.Size(107, 34);
            this.calcBtn.TabIndex = 30;
            this.calcBtn.Text = "Calculate";
            this.calcBtn.UseVisualStyleBackColor = true;
            this.calcBtn.Click += new System.EventHandler(this.calcBtn_Click);
            // 
            // amountDueOutputLbl
            // 
            this.amountDueOutputLbl.BackColor = System.Drawing.SystemColors.Window;
            this.amountDueOutputLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.amountDueOutputLbl.Location = new System.Drawing.Point(202, 409);
            this.amountDueOutputLbl.Name = "amountDueOutputLbl";
            this.amountDueOutputLbl.Size = new System.Drawing.Size(131, 26);
            this.amountDueOutputLbl.TabIndex = 29;
            // 
            // marginalOutputLbl
            // 
            this.marginalOutputLbl.BackColor = System.Drawing.SystemColors.Window;
            this.marginalOutputLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.marginalOutputLbl.Location = new System.Drawing.Point(202, 364);
            this.marginalOutputLbl.Name = "marginalOutputLbl";
            this.marginalOutputLbl.Size = new System.Drawing.Size(131, 26);
            this.marginalOutputLbl.TabIndex = 28;
            // 
            // amountDueLbl
            // 
            this.amountDueLbl.AutoSize = true;
            this.amountDueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountDueLbl.Location = new System.Drawing.Point(65, 410);
            this.amountDueLbl.Name = "amountDueLbl";
            this.amountDueLbl.Size = new System.Drawing.Size(114, 20);
            this.amountDueLbl.TabIndex = 27;
            this.amountDueLbl.Text = "Amount Due:";
            // 
            // marginalLbl
            // 
            this.marginalLbl.AutoSize = true;
            this.marginalLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marginalLbl.Location = new System.Drawing.Point(20, 365);
            this.marginalLbl.Name = "marginalLbl";
            this.marginalLbl.Size = new System.Drawing.Size(159, 20);
            this.marginalLbl.TabIndex = 26;
            this.marginalLbl.Text = "Marginal Tax Rate:";
            // 
            // taxIncomeTxt
            // 
            this.taxIncomeTxt.Location = new System.Drawing.Point(202, 28);
            this.taxIncomeTxt.Name = "taxIncomeTxt";
            this.taxIncomeTxt.Size = new System.Drawing.Size(121, 26);
            this.taxIncomeTxt.TabIndex = 25;
            // 
            // taxIncomeLbl
            // 
            this.taxIncomeLbl.AutoSize = true;
            this.taxIncomeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxIncomeLbl.Location = new System.Drawing.Point(39, 31);
            this.taxIncomeLbl.Name = "taxIncomeLbl";
            this.taxIncomeLbl.Size = new System.Drawing.Size(140, 20);
            this.taxIncomeLbl.TabIndex = 24;
            this.taxIncomeLbl.Text = "Taxable Income:";
            // 
            // Prog2Form
            // 
            this.AcceptButton = this.calcBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.resetBtn;
            this.ClientSize = new System.Drawing.Size(364, 458);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.calcBtn);
            this.Controls.Add(this.amountDueOutputLbl);
            this.Controls.Add(this.marginalOutputLbl);
            this.Controls.Add(this.amountDueLbl);
            this.Controls.Add(this.marginalLbl);
            this.Controls.Add(this.taxIncomeTxt);
            this.Controls.Add(this.taxIncomeLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Prog2Form";
            this.Text = "Program 2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton singleRB;
        private System.Windows.Forms.RadioButton marriedJointRB;
        private System.Windows.Forms.RadioButton marriedSeparateRB;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Button calcBtn;
        private System.Windows.Forms.Label amountDueOutputLbl;
        private System.Windows.Forms.Label marginalOutputLbl;
        private System.Windows.Forms.Label amountDueLbl;
        private System.Windows.Forms.Label marginalLbl;
        private System.Windows.Forms.TextBox taxIncomeTxt;
        private System.Windows.Forms.Label taxIncomeLbl;
        private System.Windows.Forms.RadioButton headofHouseRB;
    }
}

