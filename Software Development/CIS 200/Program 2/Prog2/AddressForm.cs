// Program 2
// CIS 200-01
// Fall 2019
// Due: 10/21/2019
// By: M1791

// File: AddressForm.cs
// Gets and sets fields and validates them using error providers and focus. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class AddressForm : Form
    {
        #region Initialization
        // Precondition:  None
        // Postcondition: The AddressForm GUI is initialized and initial focus is set
        public AddressForm()
        {
            InitializeComponent();
            this.ActiveControl = nameTxt;
        }
        #endregion

        #region Gets and Sets
        internal string AddressName // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in nameTxt is returned
            get { return nameTxt.Text; }

            // Precondition:  None
            // Postcondition: Text in nameTxt is set to specified value
            set { nameTxt.Text = value; }
        }

        internal string AddressLine1 // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in address1Txt is returned
            get { return address1Txt.Text; }

            // Precondition:  None
            // Postcondition: Text in address1Txt is set to specified value
            set { address1Txt.Text = value; }
        }

        internal string AddressLine2 // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in address2Txt is returned
            get { return address2Txt.Text; }

            // Precondition:  None
            // Postcondition: Text in address2Txt is set to specified value
            set { address2Txt.Text = value; }
        }

        internal string AddressCity // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in cityTxt is returned
            get { return cityTxt.Text; }

            // Precondition:  None
            // Postcondition: Text in cityTxt is set to specified value
            set { cityTxt.Text = value; }
        }

        internal string AddressState // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in stateCmbo is returned
            get { return stateCmbo.GetItemText(stateCmbo.SelectedItem); }

            // Precondition:  None
            // Postcondition: Text in stateCmbo is set to specified value
            set { stateCmbo.Text = value; }
        }

        internal string AddressZipCode // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in zipTxt is returned
            get { return zipTxt.Text; }

            // Precondition:  None
            // Postcondition: Text in zipTxt is set to specified value
            set { zipTxt.Text = value; }
        }
        #endregion

        #region Name Validation
        // Precondition:  Attempting to change focus from nameTxt
        // Postcondition: If entered value is valid string, focus will change,
        //                else focus will remain and error provider message set
        private void nameTxt_Validating(object sender, CancelEventArgs e)
        {
            // Null checks
            // If fails, it returns false
            // If succeeds, null check returns true
            if (string.IsNullOrWhiteSpace(nameTxt.Text))
            {
                e.Cancel = true; // Stops focus changing process. Will NOT proceed to Validated event
                nameTxt.Focus();
                nameErrorProvider.SetError(nameTxt, "Enter a string!"); // Set error message
                nameTxt.SelectAll(); // Select all text in nameTxt to ease correction
            }
        }

        // Precondition:  nametTxt_Validating succeeded
        // Postcondition: Any error message set for nameTxt is cleared
        //                Focus is allowed to change
        private void nameTxt_Validated(object sender, EventArgs e)
        {
            nameErrorProvider.SetError(nameTxt, ""); // Clears error message
        }
        #endregion

        #region Address Validation
        // Precondition:  Attempting to change focus from addressTxt
        // Postcondition: If entered value is valid string, focus will change,
        //                else focus will remain and error provider message set
        private void addressTxt_Validating(object sender, CancelEventArgs e)
        {
            // Null checks
            // If fails, it returns false
            // If succeeds, null check returns true
            if (string.IsNullOrWhiteSpace(address1Txt.Text))
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                address1Txt.Focus();

                addressErrorProvider.SetError(address1Txt, "Enter a string!"); // Set error message

                address1Txt.SelectAll(); // Select all text in addressTxt to ease correction
            }
        }

        // Precondition:  addressTxt_Validating succeeded
        // Postcondition: Any error message set for addressTxt is cleared
        //                Focus is allowed to change
        private void addressTxt_Validated(object sender, EventArgs e)
        {
            addressErrorProvider.SetError(address1Txt, ""); // Clears error message
        }
        #endregion

        #region City Validation
        // Precondition:  Attempting to change focus from cityTxt
        // Postcondition: If entered value is valid string, focus will change,
        //                else focus will remain and error provider message set
        private void cityTxt_Validating(object sender, CancelEventArgs e)
        {
            // Null checks
            // If fails, it returns false
            // If succeeds, null check returns true
            if (string.IsNullOrWhiteSpace(cityTxt.Text))
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                cityTxt.Focus();

                cityErrorProvider.SetError(cityTxt, "Enter a string!"); // Set error message

                cityTxt.SelectAll(); // Select all text in cityTxt to ease correction
            }
        }

        // Precondition:  cityTxt_Validating succeeded
        // Postcondition: Any error message set for cityTxt is cleared
        //                Focus is allowed to change
        private void cityTxt_Validated(object sender, EventArgs e)
        {
            cityErrorProvider.SetError(cityTxt, ""); // Clears error message
        }
        #endregion

        #region State Validation
        // Precondition:  Attempting to change focus from stateCmbo
        // Postcondition: If entered value is valid string, focus will change,
        //                else focus will remain and error provider message set
        private void stateCmbo_Validating(object sender, CancelEventArgs e)
        {
            // Null checks
            // If fails, it returns false
            // If succeeds, null check returns true
            if (stateCmbo.SelectedIndex < 0)
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                stateCmbo.Focus();

                stateErrorProvider.SetError(stateCmbo, "Select an option!"); // Set error message
            }
        }

        // Precondition:  stateCmbo_Validating succeeded
        // Postcondition: Any error message set for stateCmbo is cleared
        //                Focus is allowed to change
        private void stateCmbo_Validated(object sender, EventArgs e)
        {
            stateErrorProvider.SetError(stateCmbo, ""); // Clears error message
        }
        #endregion

        #region Zip Validation
        // Precondition:  Attempting to change focus from zipTxt
        // Postcondition: If entered value is valid int, focus will change,
        //                else focus will remain and error provider message set
        private void zipTxt_Validating(object sender, CancelEventArgs e)
        {
            int zip; // Value entered into zipTxt

            // Will try to parse text as int
            // If fails, TryParse returns false
            // If succeeds, TryParse returns true and number stores parsed value
            if (!int.TryParse(zipTxt.Text, out zip))
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                zipTxt.Focus();

                zipErrorProvider.SetError(zipTxt, "Enter an integer!"); // Set error message

                zipTxt.SelectAll(); // Select all text in zipTxt to ease correction
            }
            else if (zip < 0)
            {
                e.Cancel = true; // Stops focus changing process
                // Will NOT proceed to Validated event

                zipErrorProvider.SetError(zipTxt, "Enter a non-negative integer between 0 and 99999!"); // Set error message

                zipTxt.SelectAll(); // Select all text in zipTxt to ease correction
            }
            else if (zip > 99999)
            {
                e.Cancel = true; // Stops focus changing process
                // Will NOT proceed to Validated event

                zipErrorProvider.SetError(zipTxt, "Enter an integer between 0 and 99999!"); // Set error message

                zipTxt.SelectAll(); // Select all text in zipTxt to ease correction
            }
        }
        // Precondition:  zipTxt_Validating succeeded
        // Postcondition: Any error message set for zipTxt is cleared
        //                Focus is allowed to change
        private void zipTxt_Validated(object sender, EventArgs e)
        {
            zipErrorProvider.SetError(zipTxt, ""); // Clears error message
        }
        #endregion

        #region Cancel Button
        // Precondition:  User has initiated click on cancelBtn
        // Postcondition: If left-click, AddressForm is dismissed with Cancel result
        private void CancelBtn_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Was it a left-click?
                this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region OK Button
        // Precondition:  User has initiated click on OkBtn
        // Postcondition: If all controls on form validate, AddressForm is dismissed with OK result
        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                this.DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
