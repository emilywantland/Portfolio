namespace Prog2
{
    partial class EditAddressForm
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
            this.editLbl = new System.Windows.Forms.Label();
            this.addressToEditCbo = new System.Windows.Forms.ComboBox();
            this.enterbtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // editLbl
            // 
            this.editLbl.AutoSize = true;
            this.editLbl.Location = new System.Drawing.Point(24, 22);
            this.editLbl.Name = "editLbl";
            this.editLbl.Size = new System.Drawing.Size(185, 20);
            this.editLbl.TabIndex = 0;
            this.editLbl.Text = "Choose Address To Edit:";
            // 
            // addressToEditCbo
            // 
            this.addressToEditCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addressToEditCbo.FormattingEnabled = true;
            this.addressToEditCbo.Location = new System.Drawing.Point(28, 74);
            this.addressToEditCbo.Name = "addressToEditCbo";
            this.addressToEditCbo.Size = new System.Drawing.Size(181, 28);
            this.addressToEditCbo.TabIndex = 1;
            this.addressToEditCbo.Validating += new System.ComponentModel.CancelEventHandler(this.AddressToEditCbo_Validating);
            this.addressToEditCbo.Validated += new System.EventHandler(this.AddressToEditCbo_Validated);
            // 
            // enterbtn
            // 
            this.enterbtn.Location = new System.Drawing.Point(28, 133);
            this.enterbtn.Name = "enterbtn";
            this.enterbtn.Size = new System.Drawing.Size(83, 32);
            this.enterbtn.TabIndex = 2;
            this.enterbtn.Text = "Enter";
            this.enterbtn.UseVisualStyleBackColor = true;
            this.enterbtn.Click += new System.EventHandler(this.Enterbtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(129, 133);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(80, 32);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CancelBtn_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // EditAddressForm
            // 
            this.AcceptButton = this.enterbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 194);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.enterbtn);
            this.Controls.Add(this.addressToEditCbo);
            this.Controls.Add(this.editLbl);
            this.Name = "EditAddressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.EditAddressForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label editLbl;
        private System.Windows.Forms.ComboBox addressToEditCbo;
        private System.Windows.Forms.Button enterbtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}