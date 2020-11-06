namespace UPVApp
{
    partial class LetterForm
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
            this.originAddressLbl = new System.Windows.Forms.Label();
            this.destAddressLbl = new System.Windows.Forms.Label();
            this.fixedCostLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.originAddressCmbo = new System.Windows.Forms.ComboBox();
            this.destAddressCmbo = new System.Windows.Forms.ComboBox();
            this.fixedPriceTxt = new System.Windows.Forms.TextBox();
            this.originAddressErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.destAddressErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.fixedCostErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.originAddressErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destAddressErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedCostErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // originAddressLbl
            // 
            this.originAddressLbl.AutoSize = true;
            this.originAddressLbl.Location = new System.Drawing.Point(53, 16);
            this.originAddressLbl.Name = "originAddressLbl";
            this.originAddressLbl.Size = new System.Drawing.Size(117, 20);
            this.originAddressLbl.TabIndex = 0;
            this.originAddressLbl.Text = "Origin Address:";
            // 
            // destAddressLbl
            // 
            this.destAddressLbl.AutoSize = true;
            this.destAddressLbl.Location = new System.Drawing.Point(13, 58);
            this.destAddressLbl.Name = "destAddressLbl";
            this.destAddressLbl.Size = new System.Drawing.Size(157, 20);
            this.destAddressLbl.TabIndex = 1;
            this.destAddressLbl.Text = "Destination Address:";
            // 
            // fixedCostLbl
            // 
            this.fixedCostLbl.AutoSize = true;
            this.fixedCostLbl.Location = new System.Drawing.Point(80, 99);
            this.fixedCostLbl.Name = "fixedCostLbl";
            this.fixedCostLbl.Size = new System.Drawing.Size(90, 20);
            this.fixedCostLbl.TabIndex = 2;
            this.fixedCostLbl.Text = "Fixed Price:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(182, 141);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(84, 39);
            this.cancelBtn.TabIndex = 14;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(84, 141);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(84, 39);
            this.okBtn.TabIndex = 13;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // originAddressCmbo
            // 
            this.originAddressCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.originAddressCmbo.FormattingEnabled = true;
            this.originAddressCmbo.Location = new System.Drawing.Point(190, 13);
            this.originAddressCmbo.Name = "originAddressCmbo";
            this.originAddressCmbo.Size = new System.Drawing.Size(158, 28);
            this.originAddressCmbo.TabIndex = 1;
            this.originAddressCmbo.Validating += new System.ComponentModel.CancelEventHandler(this.originAddressCmbo_Validating);
            this.originAddressCmbo.Validated += new System.EventHandler(this.originAddressCmbo_Validated);
            // 
            // destAddressCmbo
            // 
            this.destAddressCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destAddressCmbo.FormattingEnabled = true;
            this.destAddressCmbo.Location = new System.Drawing.Point(190, 55);
            this.destAddressCmbo.Name = "destAddressCmbo";
            this.destAddressCmbo.Size = new System.Drawing.Size(158, 28);
            this.destAddressCmbo.TabIndex = 2;
            this.destAddressCmbo.Validating += new System.ComponentModel.CancelEventHandler(this.destAddressCmbo_Validating);
            this.destAddressCmbo.Validated += new System.EventHandler(this.destAddressCmbo_Validated);
            // 
            // fixedPriceTxt
            // 
            this.fixedPriceTxt.Location = new System.Drawing.Point(190, 96);
            this.fixedPriceTxt.Name = "fixedPriceTxt";
            this.fixedPriceTxt.Size = new System.Drawing.Size(158, 26);
            this.fixedPriceTxt.TabIndex = 3;
            this.fixedPriceTxt.Validating += new System.ComponentModel.CancelEventHandler(this.fixedPriceTxt_Validating);
            this.fixedPriceTxt.Validated += new System.EventHandler(this.fixedPriceTxt_Validated);
            // 
            // originAddressErrorProvider
            // 
            this.originAddressErrorProvider.ContainerControl = this;
            // 
            // destAddressErrorProvider
            // 
            this.destAddressErrorProvider.ContainerControl = this;
            // 
            // fixedCostErrorProvider
            // 
            this.fixedCostErrorProvider.ContainerControl = this;
            // 
            // LetterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 198);
            this.Controls.Add(this.fixedPriceTxt);
            this.Controls.Add(this.destAddressCmbo);
            this.Controls.Add(this.originAddressCmbo);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.fixedCostLbl);
            this.Controls.Add(this.destAddressLbl);
            this.Controls.Add(this.originAddressLbl);
            this.Name = "LetterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Letter Form";
            //this.Load += new System.EventHandler(this.LetterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.originAddressErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destAddressErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedCostErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label originAddressLbl;
        private System.Windows.Forms.Label destAddressLbl;
        private System.Windows.Forms.Label fixedCostLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.ComboBox originAddressCmbo;
        private System.Windows.Forms.ComboBox destAddressCmbo;
        private System.Windows.Forms.TextBox fixedPriceTxt;
        private System.Windows.Forms.ErrorProvider originAddressErrorProvider;
        private System.Windows.Forms.ErrorProvider destAddressErrorProvider;
        private System.Windows.Forms.ErrorProvider fixedCostErrorProvider;
    }
}