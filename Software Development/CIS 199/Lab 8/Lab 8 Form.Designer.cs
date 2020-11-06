namespace Lab8
{
    partial class Lab8Form
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
            this.futureValueLbl = new System.Windows.Forms.Label();
            this.annualInterestRateLbl = new System.Windows.Forms.Label();
            this.numberOfYearsLbl = new System.Windows.Forms.Label();
            this.presentValueLbl = new System.Windows.Forms.Label();
            this.futureValueInputTxt = new System.Windows.Forms.TextBox();
            this.annualInterestRateInputTxt = new System.Windows.Forms.TextBox();
            this.numberOfYearsInputTxt = new System.Windows.Forms.TextBox();
            this.presentValueOutputLbl = new System.Windows.Forms.Label();
            this.calcBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // futureValueLbl
            // 
            this.futureValueLbl.AutoSize = true;
            this.futureValueLbl.Location = new System.Drawing.Point(46, 44);
            this.futureValueLbl.Name = "futureValueLbl";
            this.futureValueLbl.Size = new System.Drawing.Size(109, 20);
            this.futureValueLbl.TabIndex = 0;
            this.futureValueLbl.Text = "Future Value: ";
            // 
            // annualInterestRateLbl
            // 
            this.annualInterestRateLbl.AutoSize = true;
            this.annualInterestRateLbl.Location = new System.Drawing.Point(46, 93);
            this.annualInterestRateLbl.Name = "annualInterestRateLbl";
            this.annualInterestRateLbl.Size = new System.Drawing.Size(161, 20);
            this.annualInterestRateLbl.TabIndex = 1;
            this.annualInterestRateLbl.Text = "Annual Interest Rate:";
            // 
            // numberOfYearsLbl
            // 
            this.numberOfYearsLbl.AutoSize = true;
            this.numberOfYearsLbl.Location = new System.Drawing.Point(46, 141);
            this.numberOfYearsLbl.Name = "numberOfYearsLbl";
            this.numberOfYearsLbl.Size = new System.Drawing.Size(133, 20);
            this.numberOfYearsLbl.TabIndex = 2;
            this.numberOfYearsLbl.Text = "Number of Years:";
            // 
            // presentValueLbl
            // 
            this.presentValueLbl.AutoSize = true;
            this.presentValueLbl.Location = new System.Drawing.Point(46, 247);
            this.presentValueLbl.Name = "presentValueLbl";
            this.presentValueLbl.Size = new System.Drawing.Size(113, 20);
            this.presentValueLbl.TabIndex = 3;
            this.presentValueLbl.Text = "Present Value:";
            // 
            // futureValueInputTxt
            // 
            this.futureValueInputTxt.Location = new System.Drawing.Point(308, 41);
            this.futureValueInputTxt.Name = "futureValueInputTxt";
            this.futureValueInputTxt.Size = new System.Drawing.Size(133, 26);
            this.futureValueInputTxt.TabIndex = 4;
            this.futureValueInputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // annualInterestRateInputTxt
            // 
            this.annualInterestRateInputTxt.Location = new System.Drawing.Point(308, 90);
            this.annualInterestRateInputTxt.Name = "annualInterestRateInputTxt";
            this.annualInterestRateInputTxt.Size = new System.Drawing.Size(133, 26);
            this.annualInterestRateInputTxt.TabIndex = 5;
            this.annualInterestRateInputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numberOfYearsInputTxt
            // 
            this.numberOfYearsInputTxt.Location = new System.Drawing.Point(308, 138);
            this.numberOfYearsInputTxt.Name = "numberOfYearsInputTxt";
            this.numberOfYearsInputTxt.Size = new System.Drawing.Size(133, 26);
            this.numberOfYearsInputTxt.TabIndex = 6;
            this.numberOfYearsInputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // presentValueOutputLbl
            // 
            this.presentValueOutputLbl.BackColor = System.Drawing.SystemColors.Window;
            this.presentValueOutputLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.presentValueOutputLbl.Location = new System.Drawing.Point(308, 246);
            this.presentValueOutputLbl.Name = "presentValueOutputLbl";
            this.presentValueOutputLbl.Size = new System.Drawing.Size(133, 26);
            this.presentValueOutputLbl.TabIndex = 7;
            this.presentValueOutputLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calcBtn
            // 
            this.calcBtn.Location = new System.Drawing.Point(126, 186);
            this.calcBtn.Name = "calcBtn";
            this.calcBtn.Size = new System.Drawing.Size(102, 43);
            this.calcBtn.TabIndex = 8;
            this.calcBtn.Text = "Calculate";
            this.calcBtn.UseVisualStyleBackColor = true;
            this.calcBtn.Click += new System.EventHandler(this.calcBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.clearBtn.Location = new System.Drawing.Point(245, 186);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(102, 43);
            this.clearBtn.TabIndex = 9;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // Lab8Form
            // 
            this.AcceptButton = this.calcBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.clearBtn;
            this.ClientSize = new System.Drawing.Size(500, 302);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.calcBtn);
            this.Controls.Add(this.presentValueOutputLbl);
            this.Controls.Add(this.numberOfYearsInputTxt);
            this.Controls.Add(this.annualInterestRateInputTxt);
            this.Controls.Add(this.futureValueInputTxt);
            this.Controls.Add(this.presentValueLbl);
            this.Controls.Add(this.numberOfYearsLbl);
            this.Controls.Add(this.annualInterestRateLbl);
            this.Controls.Add(this.futureValueLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Lab8Form";
            this.Text = "Lab8";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label futureValueLbl;
        private System.Windows.Forms.Label annualInterestRateLbl;
        private System.Windows.Forms.Label numberOfYearsLbl;
        private System.Windows.Forms.Label presentValueLbl;
        private System.Windows.Forms.TextBox futureValueInputTxt;
        private System.Windows.Forms.TextBox annualInterestRateInputTxt;
        private System.Windows.Forms.TextBox numberOfYearsInputTxt;
        private System.Windows.Forms.Label presentValueOutputLbl;
        private System.Windows.Forms.Button calcBtn;
        private System.Windows.Forms.Button clearBtn;
    }
}

