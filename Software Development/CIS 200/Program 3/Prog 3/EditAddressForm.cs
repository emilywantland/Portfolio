// Program 3
// CIS 200-01
// Fall 2019
// Due: 11/11/2019
// By: M1791

// File: EditAddressForm.cs
// This class creates the Edit Address dialog box form GUI. It performs validation
// and provides properties properties for each field.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UPVApp;

namespace Prog2
{
    public partial class EditAddressForm : Form
    {
        private UserParcelView upv; // The UserParcelView
        private List<Address> addressList;  // List of addresses used to fill combo boxes
        public const int MIN_ADDRESSES = 2; // Minimum number of addresses needed

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display and address list is initialized
        public EditAddressForm(List<Address> addresses)
        {
            InitializeComponent();

            addressList = addresses;

            upv = new UserParcelView();
        }

        #region Load Form
        // Precondition:  None
        // Postcondition: The list of addresses is used to populate the
        //                address to edit combo box
        private void EditAddressForm_Load(object sender, EventArgs e)
        {
            foreach (Address a in addressList)
            {
                addressToEditCbo.Items.Add(a.Name);
            }
        }
        #endregion

        #region Get and Set
        internal int EditAddressIndex
        {
            // Precondition:  User has selected from addressToEditCbo
            // Postcondition: The index of the selected origin address returned
            get
            {
                return addressToEditCbo.SelectedIndex;
            }

            // Precondition:  -1 <= value < addressList.Count
            // Postcondition: The specified index is selected in addressToEditCbo
            set
            {
                if ((value >= -1) && (value < addressList.Count))
                    addressToEditCbo.SelectedIndex = value;
                else
                    throw new ArgumentOutOfRangeException("EditAddressIndex", value,
                        "Index must be valid");
            }
        }
        #endregion

        #region Address Validating
        // Precondition:  None
        // Postcondition: If no address selected, focus remains and error provider
        //                highlights the field
        private void AddressToEditCbo_Validating(object sender, CancelEventArgs e)
        {
            // Downcast to sender as ComboBox
            ComboBox cbo = sender as ComboBox; // Cast sender as combo box

            if (cbo.SelectedIndex == -1) // -1 means no item selected
            {
                e.Cancel = true;
                errorProvider.SetError(cbo, "Must select an address");
            }
        }
        #endregion

        #region Address Validated
        // Precondition:  Validating of sender not cancelled, so data OK
        //                sender is Control
        // Postcondition: Error provider cleared and focus allowed to change
        private void AddressToEditCbo_Validated(object sender, EventArgs e)
        {
            // Downcast to sender as Control
            Control control = sender as Control; // Cast sender as Control
                                                 // Should always be a Control
            errorProvider.SetError(control, "");
        }
        #endregion

        #region Enter Button
        // Precondition:  User clicked on enterbtn
        // Postcondition: If invalid field on dialog, keep form open and give first invalid
        //                field the focus. Else return OK and close form.
        private void Enterbtn_Click(object sender, EventArgs e)
        {
            // Raise validating event for all enabled controls on form
            // If all pass, ValidateChildren() will be true
            if (ValidateChildren())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region Cancel Button
        // Precondition:  User pressed on cancelBtn
        // Postcondition: Form closes
        private void CancelBtn_Click(object sender, MouseEventArgs e)
        {
            // This handler uses MouseDown instead of Click event because
            // Click won't be allowed if other field's validation fails

            if (e.Button == MouseButtons.Left) // Was it a left-click?
                this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
