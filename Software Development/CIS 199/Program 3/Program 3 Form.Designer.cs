namespace Program3
{
    partial class Prog3Form
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
            this.taxOutLbl = new System.Windows.Forms.Label();
            this.taxLbl = new System.Windows.Forms.Label();
            this.marginalRateOutLbl = new System.Windows.Forms.Label();
            this.marginalRateLbl = new System.Windows.Forms.Label();
            this.calcTaxBtn = new System.Windows.Forms.Button();
            this.filingBox = new System.Windows.Forms.GroupBox();
            this.headOfHouseRdoBtn = new System.Windows.Forms.RadioButton();
            this.jointlyRdoBtn = new System.Windows.Forms.RadioButton();
            this.separatelyRdoBtn = new System.Windows.Forms.RadioButton();
            this.singleRdoBtn = new System.Windows.Forms.RadioButton();
            this.incomeTxt = new System.Windows.Forms.TextBox();
            this.incomeLbl = new System.Windows.Forms.Label();
            this.titleLbl = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.filingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // taxOutLbl
            // 
            this.taxOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.taxOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.taxOutLbl.Location = new System.Drawing.Point(203, 436);
            this.taxOutLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.taxOutLbl.Name = "taxOutLbl";
            this.taxOutLbl.Size = new System.Drawing.Size(149, 30);
            this.taxOutLbl.TabIndex = 17;
            // 
            // taxLbl
            // 
            this.taxLbl.AutoSize = true;
            this.taxLbl.Location = new System.Drawing.Point(95, 438);
            this.taxLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.taxLbl.Name = "taxLbl";
            this.taxLbl.Size = new System.Drawing.Size(95, 20);
            this.taxLbl.TabIndex = 16;
            this.taxLbl.Text = "Income Tax:";
            // 
            // marginalRateOutLbl
            // 
            this.marginalRateOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.marginalRateOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.marginalRateOutLbl.Location = new System.Drawing.Point(203, 388);
            this.marginalRateOutLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.marginalRateOutLbl.Name = "marginalRateOutLbl";
            this.marginalRateOutLbl.Size = new System.Drawing.Size(149, 30);
            this.marginalRateOutLbl.TabIndex = 15;
            // 
            // marginalRateLbl
            // 
            this.marginalRateLbl.AutoSize = true;
            this.marginalRateLbl.Location = new System.Drawing.Point(49, 390);
            this.marginalRateLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.marginalRateLbl.Name = "marginalRateLbl";
            this.marginalRateLbl.Size = new System.Drawing.Size(141, 20);
            this.marginalRateLbl.TabIndex = 14;
            this.marginalRateLbl.Text = "Marginal Tax Rate:";
            // 
            // calcTaxBtn
            // 
            this.calcTaxBtn.Location = new System.Drawing.Point(68, 334);
            this.calcTaxBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.calcTaxBtn.Name = "calcTaxBtn";
            this.calcTaxBtn.Size = new System.Drawing.Size(124, 35);
            this.calcTaxBtn.TabIndex = 13;
            this.calcTaxBtn.Text = "Calculate Tax";
            this.calcTaxBtn.UseVisualStyleBackColor = true;
            this.calcTaxBtn.Click += new System.EventHandler(this.calcTaxBtn_Click);
            // 
            // filingBox
            // 
            this.filingBox.Controls.Add(this.headOfHouseRdoBtn);
            this.filingBox.Controls.Add(this.jointlyRdoBtn);
            this.filingBox.Controls.Add(this.separatelyRdoBtn);
            this.filingBox.Controls.Add(this.singleRdoBtn);
            this.filingBox.Location = new System.Drawing.Point(43, 117);
            this.filingBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filingBox.Name = "filingBox";
            this.filingBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filingBox.Size = new System.Drawing.Size(326, 191);
            this.filingBox.TabIndex = 12;
            this.filingBox.TabStop = false;
            this.filingBox.Text = "Filing Status";
            // 
            // headOfHouseRdoBtn
            // 
            this.headOfHouseRdoBtn.AutoSize = true;
            this.headOfHouseRdoBtn.Location = new System.Drawing.Point(9, 137);
            this.headOfHouseRdoBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.headOfHouseRdoBtn.Name = "headOfHouseRdoBtn";
            this.headOfHouseRdoBtn.Size = new System.Drawing.Size(172, 24);
            this.headOfHouseRdoBtn.TabIndex = 4;
            this.headOfHouseRdoBtn.Text = "Head of Household";
            this.headOfHouseRdoBtn.UseVisualStyleBackColor = true;
            this.headOfHouseRdoBtn.CheckedChanged += new System.EventHandler(this.headOfHouseRdoBtn_CheckedChanged);
            // 
            // jointlyRdoBtn
            // 
            this.jointlyRdoBtn.AutoSize = true;
            this.jointlyRdoBtn.Location = new System.Drawing.Point(9, 102);
            this.jointlyRdoBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jointlyRdoBtn.Name = "jointlyRdoBtn";
            this.jointlyRdoBtn.Size = new System.Drawing.Size(176, 24);
            this.jointlyRdoBtn.TabIndex = 2;
            this.jointlyRdoBtn.Text = "Married Filing Jointly";
            this.jointlyRdoBtn.UseVisualStyleBackColor = true;
            this.jointlyRdoBtn.CheckedChanged += new System.EventHandler(this.jointlyRdoBtn_CheckedChanged);
            // 
            // separatelyRdoBtn
            // 
            this.separatelyRdoBtn.AutoSize = true;
            this.separatelyRdoBtn.Location = new System.Drawing.Point(10, 66);
            this.separatelyRdoBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.separatelyRdoBtn.Name = "separatelyRdoBtn";
            this.separatelyRdoBtn.Size = new System.Drawing.Size(208, 24);
            this.separatelyRdoBtn.TabIndex = 1;
            this.separatelyRdoBtn.Text = "Married Filing Separately";
            this.separatelyRdoBtn.UseVisualStyleBackColor = true;
            this.separatelyRdoBtn.CheckedChanged += new System.EventHandler(this.separatelyRdoBtn_CheckedChanged);
            // 
            // singleRdoBtn
            // 
            this.singleRdoBtn.AutoSize = true;
            this.singleRdoBtn.Location = new System.Drawing.Point(10, 31);
            this.singleRdoBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.singleRdoBtn.Name = "singleRdoBtn";
            this.singleRdoBtn.Size = new System.Drawing.Size(78, 24);
            this.singleRdoBtn.TabIndex = 0;
            this.singleRdoBtn.Text = "Single";
            this.singleRdoBtn.UseVisualStyleBackColor = true;
            this.singleRdoBtn.CheckedChanged += new System.EventHandler(this.singleRdoBtn_CheckedChanged);
            // 
            // incomeTxt
            // 
            this.incomeTxt.Location = new System.Drawing.Point(218, 68);
            this.incomeTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.incomeTxt.Name = "incomeTxt";
            this.incomeTxt.Size = new System.Drawing.Size(148, 26);
            this.incomeTxt.TabIndex = 11;
            // 
            // incomeLbl
            // 
            this.incomeLbl.AutoSize = true;
            this.incomeLbl.Location = new System.Drawing.Point(38, 72);
            this.incomeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.incomeLbl.Name = "incomeLbl";
            this.incomeLbl.Size = new System.Drawing.Size(168, 20);
            this.incomeLbl.TabIndex = 10;
            this.incomeLbl.Text = "Enter Taxable Income:";
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.Location = new System.Drawing.Point(64, 31);
            this.titleLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(285, 20);
            this.titleLbl.TabIndex = 9;
            this.titleLbl.Text = "2019 Marginal Tax Rate Caclulator";
            // 
            // clearBtn
            // 
            this.clearBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.clearBtn.Location = new System.Drawing.Point(218, 334);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(125, 35);
            this.clearBtn.TabIndex = 18;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // Prog3Form
            // 
            this.AcceptButton = this.calcTaxBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.clearBtn;
            this.ClientSize = new System.Drawing.Size(420, 494);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.taxOutLbl);
            this.Controls.Add(this.taxLbl);
            this.Controls.Add(this.marginalRateOutLbl);
            this.Controls.Add(this.marginalRateLbl);
            this.Controls.Add(this.calcTaxBtn);
            this.Controls.Add(this.filingBox);
            this.Controls.Add(this.incomeTxt);
            this.Controls.Add(this.incomeLbl);
            this.Controls.Add(this.titleLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Prog3Form";
            this.Text = "Program 3";
            this.Load += new System.EventHandler(this.Prog3Form_Load);
            this.filingBox.ResumeLayout(false);
            this.filingBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label taxOutLbl;
        private System.Windows.Forms.Label taxLbl;
        private System.Windows.Forms.Label marginalRateLbl;
        private System.Windows.Forms.Button calcTaxBtn;
        private System.Windows.Forms.GroupBox filingBox;
        private System.Windows.Forms.RadioButton headOfHouseRdoBtn;
        private System.Windows.Forms.RadioButton jointlyRdoBtn;
        private System.Windows.Forms.RadioButton separatelyRdoBtn;
        private System.Windows.Forms.RadioButton singleRdoBtn;
        private System.Windows.Forms.TextBox incomeTxt;
        private System.Windows.Forms.Label incomeLbl;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Label marginalRateOutLbl;
    }
}

