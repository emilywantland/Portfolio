namespace Lab3
{
    partial class lab3Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lab3Form));
            this.radiusOfSphereLbl = new System.Windows.Forms.Label();
            this.diameterLbl = new System.Windows.Forms.Label();
            this.surfaceAreaLbl = new System.Windows.Forms.Label();
            this.volumeLbl = new System.Windows.Forms.Label();
            this.diameterOutLbl = new System.Windows.Forms.Label();
            this.surfaceAreaOutLbl = new System.Windows.Forms.Label();
            this.volumeOutLbl = new System.Windows.Forms.Label();
            this.radiusOfSphereTxt = new System.Windows.Forms.TextBox();
            this.calcBtn = new System.Windows.Forms.Button();
            this.sphereOnePic = new System.Windows.Forms.PictureBox();
            this.sphereTwoPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.sphereOnePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sphereTwoPic)).BeginInit();
            this.SuspendLayout();
            // 
            // radiusOfSphereLbl
            // 
            this.radiusOfSphereLbl.AutoSize = true;
            this.radiusOfSphereLbl.Location = new System.Drawing.Point(223, 69);
            this.radiusOfSphereLbl.Name = "radiusOfSphereLbl";
            this.radiusOfSphereLbl.Size = new System.Drawing.Size(137, 20);
            this.radiusOfSphereLbl.TabIndex = 0;
            this.radiusOfSphereLbl.Text = "Radius of Sphere:";
            // 
            // diameterLbl
            // 
            this.diameterLbl.AutoSize = true;
            this.diameterLbl.Location = new System.Drawing.Point(35, 205);
            this.diameterLbl.Name = "diameterLbl";
            this.diameterLbl.Size = new System.Drawing.Size(74, 20);
            this.diameterLbl.TabIndex = 3;
            this.diameterLbl.Text = "Diameter";
            // 
            // surfaceAreaLbl
            // 
            this.surfaceAreaLbl.AutoSize = true;
            this.surfaceAreaLbl.Location = new System.Drawing.Point(35, 251);
            this.surfaceAreaLbl.Name = "surfaceAreaLbl";
            this.surfaceAreaLbl.Size = new System.Drawing.Size(103, 20);
            this.surfaceAreaLbl.TabIndex = 4;
            this.surfaceAreaLbl.Text = "Surface Area";
            // 
            // volumeLbl
            // 
            this.volumeLbl.AutoSize = true;
            this.volumeLbl.Location = new System.Drawing.Point(35, 295);
            this.volumeLbl.Name = "volumeLbl";
            this.volumeLbl.Size = new System.Drawing.Size(63, 20);
            this.volumeLbl.TabIndex = 5;
            this.volumeLbl.Text = "Volume";
            // 
            // diameterOutLbl
            // 
            this.diameterOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.diameterOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.diameterOutLbl.Location = new System.Drawing.Point(158, 204);
            this.diameterOutLbl.Name = "diameterOutLbl";
            this.diameterOutLbl.Size = new System.Drawing.Size(148, 26);
            this.diameterOutLbl.TabIndex = 6;
            // 
            // surfaceAreaOutLbl
            // 
            this.surfaceAreaOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.surfaceAreaOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.surfaceAreaOutLbl.Location = new System.Drawing.Point(158, 250);
            this.surfaceAreaOutLbl.Name = "surfaceAreaOutLbl";
            this.surfaceAreaOutLbl.Size = new System.Drawing.Size(148, 26);
            this.surfaceAreaOutLbl.TabIndex = 7;
            // 
            // volumeOutLbl
            // 
            this.volumeOutLbl.BackColor = System.Drawing.SystemColors.Window;
            this.volumeOutLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.volumeOutLbl.Location = new System.Drawing.Point(158, 294);
            this.volumeOutLbl.Name = "volumeOutLbl";
            this.volumeOutLbl.Size = new System.Drawing.Size(148, 26);
            this.volumeOutLbl.TabIndex = 8;
            // 
            // radiusOfSphereTxt
            // 
            this.radiusOfSphereTxt.Location = new System.Drawing.Point(380, 66);
            this.radiusOfSphereTxt.Name = "radiusOfSphereTxt";
            this.radiusOfSphereTxt.Size = new System.Drawing.Size(100, 26);
            this.radiusOfSphereTxt.TabIndex = 10;
            // 
            // calcBtn
            // 
            this.calcBtn.Location = new System.Drawing.Point(335, 111);
            this.calcBtn.Name = "calcBtn";
            this.calcBtn.Size = new System.Drawing.Size(94, 32);
            this.calcBtn.TabIndex = 11;
            this.calcBtn.Text = "Calculate";
            this.calcBtn.UseVisualStyleBackColor = true;
            this.calcBtn.Click += new System.EventHandler(this.calcBtn_Click);
            // 
            // sphereOnePic
            // 
            this.sphereOnePic.Image = ((System.Drawing.Image)(resources.GetObject("sphereOnePic.Image")));
            this.sphereOnePic.Location = new System.Drawing.Point(24, 12);
            this.sphereOnePic.Name = "sphereOnePic";
            this.sphereOnePic.Size = new System.Drawing.Size(150, 150);
            this.sphereOnePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sphereOnePic.TabIndex = 12;
            this.sphereOnePic.TabStop = false;
            // 
            // sphereTwoPic
            // 
            this.sphereTwoPic.Image = ((System.Drawing.Image)(resources.GetObject("sphereTwoPic.Image")));
            this.sphereTwoPic.Location = new System.Drawing.Point(335, 182);
            this.sphereTwoPic.Name = "sphereTwoPic";
            this.sphereTwoPic.Size = new System.Drawing.Size(150, 150);
            this.sphereTwoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sphereTwoPic.TabIndex = 13;
            this.sphereTwoPic.TabStop = false;
            // 
            // lab3Form
            // 
            this.AcceptButton = this.calcBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 361);
            this.Controls.Add(this.sphereTwoPic);
            this.Controls.Add(this.sphereOnePic);
            this.Controls.Add(this.calcBtn);
            this.Controls.Add(this.radiusOfSphereTxt);
            this.Controls.Add(this.volumeOutLbl);
            this.Controls.Add(this.surfaceAreaOutLbl);
            this.Controls.Add(this.diameterOutLbl);
            this.Controls.Add(this.volumeLbl);
            this.Controls.Add(this.surfaceAreaLbl);
            this.Controls.Add(this.diameterLbl);
            this.Controls.Add(this.radiusOfSphereLbl);
            this.Name = "lab3Form";
            this.Text = "Lab 3";
            ((System.ComponentModel.ISupportInitialize)(this.sphereOnePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sphereTwoPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label radiusOfSphereLbl;
        private System.Windows.Forms.Label diameterLbl;
        private System.Windows.Forms.Label surfaceAreaLbl;
        private System.Windows.Forms.Label volumeLbl;
        private System.Windows.Forms.Label diameterOutLbl;
        private System.Windows.Forms.Label surfaceAreaOutLbl;
        private System.Windows.Forms.Label volumeOutLbl;
        private System.Windows.Forms.TextBox radiusOfSphereTxt;
        private System.Windows.Forms.Button calcBtn;
        private System.Windows.Forms.PictureBox sphereOnePic;
        private System.Windows.Forms.PictureBox sphereTwoPic;
    }
}

