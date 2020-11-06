namespace Lab2
{
    partial class Lab2
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
            this.priceOfMealTxt = new System.Windows.Forms.MaskedTextBox();
            this.priceOfMealLbl = new System.Windows.Forms.Label();
            this.fifteenLbl = new System.Windows.Forms.Label();
            this.fifteenPercentLbl = new System.Windows.Forms.Label();
            this.eighteenPercentLbl = new System.Windows.Forms.Label();
            this.eighteenLbl = new System.Windows.Forms.Label();
            this.twentyPercentLbl = new System.Windows.Forms.Label();
            this.twentyLbl = new System.Windows.Forms.Label();
            this.calculateTipBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // priceOfMealTxt
            // 
            this.priceOfMealTxt.Location = new System.Drawing.Point(193, 9);
            this.priceOfMealTxt.Name = "priceOfMealTxt";
            this.priceOfMealTxt.Size = new System.Drawing.Size(100, 26);
            this.priceOfMealTxt.TabIndex = 0;
            // 
            // priceOfMealLbl
            // 
            this.priceOfMealLbl.AutoSize = true;
            this.priceOfMealLbl.Location = new System.Drawing.Point(12, 9);
            this.priceOfMealLbl.Name = "priceOfMealLbl";
            this.priceOfMealLbl.Size = new System.Drawing.Size(146, 20);
            this.priceOfMealLbl.TabIndex = 1;
            this.priceOfMealLbl.Text = "Enter price of meal:";
            // 
            // fifteenLbl
            // 
            this.fifteenLbl.AutoSize = true;
            this.fifteenLbl.Location = new System.Drawing.Point(117, 70);
            this.fifteenLbl.Name = "fifteenLbl";
            this.fifteenLbl.Size = new System.Drawing.Size(41, 20);
            this.fifteenLbl.TabIndex = 2;
            this.fifteenLbl.Text = "15%";
            // 
            // fifteenPercentLbl
            // 
            this.fifteenPercentLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fifteenPercentLbl.Location = new System.Drawing.Point(193, 70);
            this.fifteenPercentLbl.Name = "fifteenPercentLbl";
            this.fifteenPercentLbl.Size = new System.Drawing.Size(100, 23);
            this.fifteenPercentLbl.TabIndex = 3;
            // 
            // eighteenPercentLbl
            // 
            this.eighteenPercentLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eighteenPercentLbl.Location = new System.Drawing.Point(193, 124);
            this.eighteenPercentLbl.Name = "eighteenPercentLbl";
            this.eighteenPercentLbl.Size = new System.Drawing.Size(100, 23);
            this.eighteenPercentLbl.TabIndex = 4;
            // 
            // eighteenLbl
            // 
            this.eighteenLbl.AutoSize = true;
            this.eighteenLbl.Location = new System.Drawing.Point(117, 127);
            this.eighteenLbl.Name = "eighteenLbl";
            this.eighteenLbl.Size = new System.Drawing.Size(41, 20);
            this.eighteenLbl.TabIndex = 5;
            this.eighteenLbl.Text = "18%";
            // 
            // twentyPercentLbl
            // 
            this.twentyPercentLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.twentyPercentLbl.Location = new System.Drawing.Point(193, 181);
            this.twentyPercentLbl.Name = "twentyPercentLbl";
            this.twentyPercentLbl.Size = new System.Drawing.Size(100, 23);
            this.twentyPercentLbl.TabIndex = 6;
            // 
            // twentyLbl
            // 
            this.twentyLbl.AutoSize = true;
            this.twentyLbl.Location = new System.Drawing.Point(117, 184);
            this.twentyLbl.Name = "twentyLbl";
            this.twentyLbl.Size = new System.Drawing.Size(41, 20);
            this.twentyLbl.TabIndex = 7;
            this.twentyLbl.Text = "20%";
            // 
            // calculateTipBtn
            // 
            this.calculateTipBtn.Location = new System.Drawing.Point(121, 246);
            this.calculateTipBtn.Name = "calculateTipBtn";
            this.calculateTipBtn.Size = new System.Drawing.Size(127, 37);
            this.calculateTipBtn.TabIndex = 8;
            this.calculateTipBtn.Text = "Calculate Tip";
            this.calculateTipBtn.UseVisualStyleBackColor = true;
            this.calculateTipBtn.Click += new System.EventHandler(this.calculateTipBtn_Click);
            // 
            // Lab2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 319);
            this.Controls.Add(this.calculateTipBtn);
            this.Controls.Add(this.twentyLbl);
            this.Controls.Add(this.twentyPercentLbl);
            this.Controls.Add(this.eighteenLbl);
            this.Controls.Add(this.eighteenPercentLbl);
            this.Controls.Add(this.fifteenPercentLbl);
            this.Controls.Add(this.fifteenLbl);
            this.Controls.Add(this.priceOfMealLbl);
            this.Controls.Add(this.priceOfMealTxt);
            this.Name = "Lab2";
            this.Text = "Lab 2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox priceOfMealTxt;
        private System.Windows.Forms.Label priceOfMealLbl;
        private System.Windows.Forms.Label fifteenLbl;
        private System.Windows.Forms.Label fifteenPercentLbl;
        private System.Windows.Forms.Label eighteenPercentLbl;
        private System.Windows.Forms.Label eighteenLbl;
        private System.Windows.Forms.Label twentyPercentLbl;
        private System.Windows.Forms.Label twentyLbl;
        private System.Windows.Forms.Button calculateTipBtn;
    }
}

