namespace Lab9
{
    partial class DateForm
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
            this.dateOutputLbl = new System.Windows.Forms.Label();
            this.monthInputTxt = new System.Windows.Forms.TextBox();
            this.dateOutputStatementLbl = new System.Windows.Forms.Label();
            this.monthInputLbl = new System.Windows.Forms.Label();
            this.dayInputLbl = new System.Windows.Forms.Label();
            this.yearInputLbl = new System.Windows.Forms.Label();
            this.dayInputTxt = new System.Windows.Forms.TextBox();
            this.yearInputTxt = new System.Windows.Forms.TextBox();
            this.updateMonthBtn = new System.Windows.Forms.Button();
            this.updateDayBtn = new System.Windows.Forms.Button();
            this.updateYearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateOutputLbl
            // 
            this.dateOutputLbl.AutoSize = true;
            this.dateOutputLbl.Location = new System.Drawing.Point(106, 46);
            this.dateOutputLbl.Name = "dateOutputLbl";
            this.dateOutputLbl.Size = new System.Drawing.Size(48, 20);
            this.dateOutputLbl.TabIndex = 0;
            this.dateOutputLbl.Text = "Date:";
            // 
            // monthInputTxt
            // 
            this.monthInputTxt.Location = new System.Drawing.Point(110, 100);
            this.monthInputTxt.Name = "monthInputTxt";
            this.monthInputTxt.Size = new System.Drawing.Size(100, 26);
            this.monthInputTxt.TabIndex = 1;
            this.monthInputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateOutputStatementLbl
            // 
            this.dateOutputStatementLbl.BackColor = System.Drawing.SystemColors.Window;
            this.dateOutputStatementLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateOutputStatementLbl.Location = new System.Drawing.Point(160, 43);
            this.dateOutputStatementLbl.Name = "dateOutputStatementLbl";
            this.dateOutputStatementLbl.Size = new System.Drawing.Size(144, 26);
            this.dateOutputStatementLbl.TabIndex = 2;
            this.dateOutputStatementLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // monthInputLbl
            // 
            this.monthInputLbl.AutoSize = true;
            this.monthInputLbl.Location = new System.Drawing.Point(46, 103);
            this.monthInputLbl.Name = "monthInputLbl";
            this.monthInputLbl.Size = new System.Drawing.Size(58, 20);
            this.monthInputLbl.TabIndex = 3;
            this.monthInputLbl.Text = "Month:";
            // 
            // dayInputLbl
            // 
            this.dayInputLbl.AutoSize = true;
            this.dayInputLbl.Location = new System.Drawing.Point(63, 155);
            this.dayInputLbl.Name = "dayInputLbl";
            this.dayInputLbl.Size = new System.Drawing.Size(41, 20);
            this.dayInputLbl.TabIndex = 4;
            this.dayInputLbl.Text = "Day:";
            // 
            // yearInputLbl
            // 
            this.yearInputLbl.AutoSize = true;
            this.yearInputLbl.Location = new System.Drawing.Point(57, 208);
            this.yearInputLbl.Name = "yearInputLbl";
            this.yearInputLbl.Size = new System.Drawing.Size(47, 20);
            this.yearInputLbl.TabIndex = 5;
            this.yearInputLbl.Text = "Year:";
            // 
            // dayInputTxt
            // 
            this.dayInputTxt.Location = new System.Drawing.Point(110, 152);
            this.dayInputTxt.Name = "dayInputTxt";
            this.dayInputTxt.Size = new System.Drawing.Size(100, 26);
            this.dayInputTxt.TabIndex = 6;
            this.dayInputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yearInputTxt
            // 
            this.yearInputTxt.Location = new System.Drawing.Point(110, 205);
            this.yearInputTxt.Name = "yearInputTxt";
            this.yearInputTxt.Size = new System.Drawing.Size(100, 26);
            this.yearInputTxt.TabIndex = 7;
            this.yearInputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // updateMonthBtn
            // 
            this.updateMonthBtn.Location = new System.Drawing.Point(231, 92);
            this.updateMonthBtn.Name = "updateMonthBtn";
            this.updateMonthBtn.Size = new System.Drawing.Size(151, 42);
            this.updateMonthBtn.TabIndex = 8;
            this.updateMonthBtn.Text = "Update Month";
            this.updateMonthBtn.UseVisualStyleBackColor = true;
            this.updateMonthBtn.Click += new System.EventHandler(this.updateMonthBtn_Click);
            // 
            // updateDayBtn
            // 
            this.updateDayBtn.Location = new System.Drawing.Point(231, 144);
            this.updateDayBtn.Name = "updateDayBtn";
            this.updateDayBtn.Size = new System.Drawing.Size(151, 42);
            this.updateDayBtn.TabIndex = 9;
            this.updateDayBtn.Text = "Update Day";
            this.updateDayBtn.UseVisualStyleBackColor = true;
            this.updateDayBtn.Click += new System.EventHandler(this.updateDayBtn_Click);
            // 
            // updateYearBtn
            // 
            this.updateYearBtn.Location = new System.Drawing.Point(231, 197);
            this.updateYearBtn.Name = "updateYearBtn";
            this.updateYearBtn.Size = new System.Drawing.Size(151, 42);
            this.updateYearBtn.TabIndex = 10;
            this.updateYearBtn.Text = "Update Year";
            this.updateYearBtn.UseVisualStyleBackColor = true;
            this.updateYearBtn.Click += new System.EventHandler(this.updateYearBtn_Click);
            // 
            // DateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 284);
            this.Controls.Add(this.updateYearBtn);
            this.Controls.Add(this.updateDayBtn);
            this.Controls.Add(this.updateMonthBtn);
            this.Controls.Add(this.yearInputTxt);
            this.Controls.Add(this.dayInputTxt);
            this.Controls.Add(this.yearInputLbl);
            this.Controls.Add(this.dayInputLbl);
            this.Controls.Add(this.monthInputLbl);
            this.Controls.Add(this.dateOutputStatementLbl);
            this.Controls.Add(this.monthInputTxt);
            this.Controls.Add(this.dateOutputLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DateForm";
            this.Text = "Prog4";
            this.Load += new System.EventHandler(this.DateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dateOutputLbl;
        private System.Windows.Forms.TextBox monthInputTxt;
        private System.Windows.Forms.Label dateOutputStatementLbl;
        private System.Windows.Forms.Label monthInputLbl;
        private System.Windows.Forms.Label dayInputLbl;
        private System.Windows.Forms.Label yearInputLbl;
        private System.Windows.Forms.TextBox dayInputTxt;
        private System.Windows.Forms.TextBox yearInputTxt;
        private System.Windows.Forms.Button updateMonthBtn;
        private System.Windows.Forms.Button updateDayBtn;
        private System.Windows.Forms.Button updateYearBtn;
    }
}

