namespace UPVApp
{
    partial class AddressForm
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
            this.components = new System.ComponentModel.Container();
            this.nameLbl = new System.Windows.Forms.Label();
            this.addressLbl = new System.Windows.Forms.Label();
            this.address1Txt = new System.Windows.Forms.TextBox();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.address2Txt = new System.Windows.Forms.TextBox();
            this.cityTxt = new System.Windows.Forms.TextBox();
            this.stateCmbo = new System.Windows.Forms.ComboBox();
            this.zipTxt = new System.Windows.Forms.TextBox();
            this.cityLbl = new System.Windows.Forms.Label();
            this.stateLbl = new System.Windows.Forms.Label();
            this.zipLbl = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.nameErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.addressErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cityErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.stateErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.zipErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nameErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zipErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(26, 16);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(59, 20);
            this.nameLbl.TabIndex = 0;
            this.nameLbl.Text = "Name: ";
            // 
            // addressLbl
            // 
            this.addressLbl.AutoSize = true;
            this.addressLbl.Location = new System.Drawing.Point(13, 60);
            this.addressLbl.Name = "addressLbl";
            this.addressLbl.Size = new System.Drawing.Size(72, 20);
            this.addressLbl.TabIndex = 1;
            this.addressLbl.Text = "Address:";
            // 
            // address1Txt
            // 
            this.address1Txt.Location = new System.Drawing.Point(111, 57);
            this.address1Txt.Name = "address1Txt";
            this.address1Txt.Size = new System.Drawing.Size(158, 26);
            this.address1Txt.TabIndex = 2;
            this.address1Txt.Validating += new System.ComponentModel.CancelEventHandler(this.addressTxt_Validating);
            this.address1Txt.Validated += new System.EventHandler(this.addressTxt_Validated);
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(111, 13);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(158, 26);
            this.nameTxt.TabIndex = 1;
            this.nameTxt.Validating += new System.ComponentModel.CancelEventHandler(this.nameTxt_Validating);
            this.nameTxt.Validated += new System.EventHandler(this.nameTxt_Validated);
            // 
            // address2Txt
            // 
            this.address2Txt.Location = new System.Drawing.Point(111, 102);
            this.address2Txt.Name = "address2Txt";
            this.address2Txt.Size = new System.Drawing.Size(158, 26);
            this.address2Txt.TabIndex = 3;
            // 
            // cityTxt
            // 
            this.cityTxt.Location = new System.Drawing.Point(111, 145);
            this.cityTxt.Name = "cityTxt";
            this.cityTxt.Size = new System.Drawing.Size(158, 26);
            this.cityTxt.TabIndex = 4;
            this.cityTxt.Validating += new System.ComponentModel.CancelEventHandler(this.cityTxt_Validating);
            this.cityTxt.Validated += new System.EventHandler(this.cityTxt_Validated);
            // 
            // stateCmbo
            // 
            this.stateCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateCmbo.FormattingEnabled = true;
            this.stateCmbo.Items.AddRange(new object[] {
            "KY",
            "IN",
            "OH",
            "WV"});
            this.stateCmbo.Location = new System.Drawing.Point(111, 189);
            this.stateCmbo.Name = "stateCmbo";
            this.stateCmbo.Size = new System.Drawing.Size(158, 28);
            this.stateCmbo.TabIndex = 5;
            this.stateCmbo.Validating += new System.ComponentModel.CancelEventHandler(this.stateCmbo_Validating);
            this.stateCmbo.Validated += new System.EventHandler(this.stateCmbo_Validated);
            // 
            // zipTxt
            // 
            this.zipTxt.Location = new System.Drawing.Point(111, 235);
            this.zipTxt.Name = "zipTxt";
            this.zipTxt.Size = new System.Drawing.Size(158, 26);
            this.zipTxt.TabIndex = 6;
            this.zipTxt.Validating += new System.ComponentModel.CancelEventHandler(this.zipTxt_Validating);
            this.zipTxt.Validated += new System.EventHandler(this.zipTxt_Validated);
            // 
            // cityLbl
            // 
            this.cityLbl.AutoSize = true;
            this.cityLbl.Location = new System.Drawing.Point(46, 148);
            this.cityLbl.Name = "cityLbl";
            this.cityLbl.Size = new System.Drawing.Size(39, 20);
            this.cityLbl.TabIndex = 8;
            this.cityLbl.Text = "City:";
            // 
            // stateLbl
            // 
            this.stateLbl.AutoSize = true;
            this.stateLbl.Location = new System.Drawing.Point(33, 192);
            this.stateLbl.Name = "stateLbl";
            this.stateLbl.Size = new System.Drawing.Size(52, 20);
            this.stateLbl.TabIndex = 9;
            this.stateLbl.Text = "State:";
            // 
            // zipLbl
            // 
            this.zipLbl.AutoSize = true;
            this.zipLbl.Location = new System.Drawing.Point(50, 238);
            this.zipLbl.Name = "zipLbl";
            this.zipLbl.Size = new System.Drawing.Size(35, 20);
            this.zipLbl.TabIndex = 10;
            this.zipLbl.Text = "Zip:";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(50, 279);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(84, 39);
            this.okBtn.TabIndex = 11;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(148, 279);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(84, 39);
            this.cancelBtn.TabIndex = 12;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CancelBtn_Click);
            // 
            // nameErrorProvider
            // 
            this.nameErrorProvider.ContainerControl = this;
            // 
            // addressErrorProvider
            // 
            this.addressErrorProvider.ContainerControl = this;
            // 
            // cityErrorProvider
            // 
            this.cityErrorProvider.ContainerControl = this;
            // 
            // stateErrorProvider
            // 
            this.stateErrorProvider.ContainerControl = this;
            // 
            // zipErrorProvider
            // 
            this.zipErrorProvider.ContainerControl = this;
            // 
            // AddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 330);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.zipLbl);
            this.Controls.Add(this.stateLbl);
            this.Controls.Add(this.cityLbl);
            this.Controls.Add(this.zipTxt);
            this.Controls.Add(this.stateCmbo);
            this.Controls.Add(this.cityTxt);
            this.Controls.Add(this.address2Txt);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.address1Txt);
            this.Controls.Add(this.addressLbl);
            this.Controls.Add(this.nameLbl);
            this.Name = "AddressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Address Form";
            ((System.ComponentModel.ISupportInitialize)(this.nameErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zipErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label addressLbl;
        private System.Windows.Forms.TextBox address1Txt;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox address2Txt;
        private System.Windows.Forms.TextBox cityTxt;
        private System.Windows.Forms.ComboBox stateCmbo;
        private System.Windows.Forms.TextBox zipTxt;
        private System.Windows.Forms.Label cityLbl;
        private System.Windows.Forms.Label stateLbl;
        private System.Windows.Forms.Label zipLbl;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ErrorProvider nameErrorProvider;
        private System.Windows.Forms.ErrorProvider addressErrorProvider;
        private System.Windows.Forms.ErrorProvider cityErrorProvider;
        private System.Windows.Forms.ErrorProvider stateErrorProvider;
        private System.Windows.Forms.ErrorProvider zipErrorProvider;
    }
}