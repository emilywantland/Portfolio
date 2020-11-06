// Program 2
// CIS 200-01
// Fall 2019
// Due: 10/21/2019
// By: M1791

// File: LetterForm.cs
// Initiaizes combo boxes, gets and sets fields, and validates them using error providers and focus. 

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
    public partial class LetterForm : Form
    {
        #region Initialization
        // Precondition:  None
        // Postcondition: The LetterForm GUI and combo boxes are initialized and initial focus is set
        public LetterForm(List<Address> addressList)
        {
            InitializeComponent();

            this.ActiveControl = originAddressCmbo;

            foreach (Address address in addressList)
            {
                originAddressCmbo.Items.Add(address.Name);
                destAddressCmbo.Items.Add(address.Name);
            }
        }
        #endregion

        #region Get and Set
        internal string FixedPrice // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in fixedPriceTxt is returned
            get { return fixedPriceTxt.Text; }

            // Precondition:  None
            // Postcondition: Text in fixedPriceTxt is set to specified value
            set { fixedPriceTxt.Text = value; }
        }

        internal int OriginAddress // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in originAddressCmbo is returned
            get { return originAddressCmbo.SelectedIndex; }

            // Precondition:  None
            // Postcondition: Text in originAddressCmbo is set to specified value
            set { originAddressCmbo.SelectedIndex = value; }
        }

        internal int DestinationAddress // Can be accessed by other classes in same namespace
        {
            // Precondition:  None
            // Postcondition: Text in destAddressCmbo is returned
            get { return destAddressCmbo.SelectedIndex; }

            // Precondition:  None
            // Postcondition: Text in destAddressCmbo is set to specified value
            set { destAddressCmbo.SelectedIndex = value; }
        }
        #endregion

        #region Origin Address Validation
        // Precondition:  Attempting to change focus from originAddressCmbo
        // Postcondition: If entered value is valid string, focus will change,
        //                else focus will remain and error provider message set
        private void originAddressCmbo_Validating(object sender, CancelEventArgs e)
        {
            // Null checks
            // If fails, it returns false
            // If succeeds, null check returns true
            if (originAddressCmbo.SelectedIndex < 0)
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                originAddressCmbo.Focus();

                originAddressErrorProvider.SetError(originAddressCmbo, "Select an option!"); // Set error message
            }
        }

        // Precondition:  originAddressCmbo_Validating succeeded
        // Postcondition: Any error message set for originAddressCmbo is cleared
        //                Focus is allowed to change
        private void originAddressCmbo_Validated(object sender, EventArgs e)
        {
            originAddressErrorProvider.SetError(originAddressCmbo, ""); // Clears error message
        }
        #endregion

        #region Destination Address Validation
        // Precondition:  Attempting to change focus from destAddressCmbo
        // Postcondition: If entered value is valid string, focus will change,
        //                else focus will remain and error provider message set
        private void destAddressCmbo_Validating(object sender, CancelEventArgs e)
        {
            // Null checks
            // If fails, it returns false
            // If succeeds, null check returns true
            if (destAddressCmbo.SelectedIndex < 0)
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                destAddressCmbo.Focus();

                destAddressErrorProvider.SetError(destAddressCmbo, "Select an option!"); // Set error message
            }
            else if (destAddressCmbo.SelectedIndex == originAddressCmbo.SelectedIndex)
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                destAddressCmbo.Focus();

                destAddressErrorProvider.SetError(destAddressCmbo, "The origin and the destination addresses must be different!"); // Set error message
            }
        }

        // Precondition:  destAddressCmbo_Validating succeeded
        // Postcondition: Any error message set for destAddressCmbo is cleared
        //                Focus is allowed to change
        private void destAddressCmbo_Validated(object sender, EventArgs e)
        {
            destAddressErrorProvider.SetError(destAddressCmbo, ""); // Clears error message
        }
        #endregion

        #region Fixed Cost Validation
        // Precondition:  Attempting to change focus from fixedPriceTxt
        // Postcondition: If entered value is valid int, focus will change,
        //                else focus will remain and error provider message set
        private void fixedPriceTxt_Validating(object sender, CancelEventArgs e)
        {
            decimal fixedCost; // Value entered into fixedPriceTxt

            // Will try to parse text as int
            // If fails, TryParse returns false
            // If succeeds, TryParse returns true and number stores parsed value
            if (!decimal.TryParse(fixedPriceTxt.Text, out fixedCost))
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                fixedPriceTxt.Focus();

                fixedCostErrorProvider.SetError(fixedPriceTxt, "Enter a price!"); // Set error message

                fixedPriceTxt.SelectAll(); // Select all text in fixedPriceTxt to ease correction
            }
            else if (fixedCost < 0)
            {
                e.Cancel = true; // Stops focus changing process
                // Will NOT proceed to Validated event

                fixedCostErrorProvider.SetError(fixedPriceTxt, "Enter a positive price!"); // Set error message

                fixedPriceTxt.SelectAll(); // Select all text in fixedPriceTxt to ease correction
            }
        }
        // Precondition:  fixedPriceTxt_Validating succeeded
        // Postcondition: Any error message set for fixedPriceTxt is cleared
        //                Focus is allowed to change
        private void fixedPriceTxt_Validated(object sender, EventArgs e)
        {
            fixedCostErrorProvider.SetError(fixedPriceTxt, ""); // Clears error message
        }
        #endregion

        #region Cancel Button
        // Precondition:  User has initiated click on cancelBtn
        // Postcondition: If left-click, LetterForm is dismissed with Cancel result
        private void CancelBtn_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Was it a left-click?
                this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region OK Button
        // Precondition:  User has initiated click on OkBtn
        // Postcondition: If all controls on form validate, LetterForm is dismissed with OK result
        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                this.DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
