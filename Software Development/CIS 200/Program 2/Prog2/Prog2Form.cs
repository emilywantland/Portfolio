// Program 2
// CIS 200-01
// Fall 2019
// Due: 10/21/2019
// By: M1791

// File: Prog2Form.cs
// Initializes address and parcel items, adds items to lists, and generates reports.

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
    public partial class Prog2Form : Form
    {
        #region Initialization
        // Constructed UPV Object
        private UserParcelView upv = new UserParcelView();

        // Precondition:  None
        // Postcondition: The Prog2Form GUI and address and parcel items is initialized
        public Prog2Form()
        {
            InitializeComponent();

            upv = new UserParcelView();

            // UPV Address Data
            upv.AddAddress("John Smith", "1234 Happy Lane", "Louisville", "KY", 40208);
            upv.AddAddress("Jane Doe", "5678 Oak Road", "Jeffersonville", "IN", 47129);
            upv.AddAddress("Rob Perry", "987 Hillcrest Avenue", "Cincinatti", "OH", 45202);
            upv.AddAddress("Sarah Murphy", "6543 Stone Street", "Montgomery", "WV", 25136);
            upv.AddGroundPackage(upv.AddressAt(3), upv.AddressAt(0), 4.5, 6.00, 12.5, 35);
            upv.AddLetter(upv.AddressAt(1), upv.AddressAt(2), (decimal)5.25);
            upv.AddLetter(upv.AddressAt(3), upv.AddressAt(2), (decimal)3.5);
            upv.AddNextDayAirPackage(upv.AddressAt(3), upv.AddressAt(1), 10, 6, 8.25, 17, (decimal)7.25);
            upv.AddTwoDayAirPackage(upv.AddressAt(0), upv.AddressAt(2), 18.25, 11.5, 7, 13, TwoDayAirPackage.Delivery.Saver);
        }
        #endregion

        #region Exit Button
        // Precondition:  User has initiated exit button
        // Postcondition: Prog2Form is dismissed with Cancel result
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region About Button
        // Precondition:  User has initiated about button
        // Postcondition: About dialogue box pops up
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID: M1791\nProgram 2\nCIS 200-01\nDue: 10/21/2019", "About Program 2",
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Address Button
        // Precondition:  User has initiated the insert address button
        // Postcondition: A modal AddressForm displays and when properly filled out,
        //                an item is added to the list of addresses
        private void AddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create New Address Child
            var AddressForm = new AddressForm(); // Address Form object
            DialogResult result;
            result = AddressForm.ShowDialog(); // Display child 

            if (result == DialogResult.OK)
            {
                string name = AddressForm.AddressName;
                string address1 = AddressForm.AddressLine1;
                string address2 = AddressForm.AddressLine2;
                string city = AddressForm.AddressCity;
                string state = AddressForm.AddressState;
                int zip = int.Parse(AddressForm.AddressZipCode);

                upv.AddAddress(name, address1, address2, city, state, zip);
            }
        }
        #endregion

        #region Letter Button
        // Precondition:  User has initiated the insert address button
        // Postcondition: A modal LetterForm displays and when properly filled out,
        //                an item is added to the list of parcels
        private void LetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create New Letter Child
            var LetterForm = new LetterForm(upv.AddressList); // Letter Form object
            DialogResult result;
            result = LetterForm.ShowDialog(); // Display child 

            if (result == DialogResult.OK)
            {
                int originAddress = LetterForm.OriginAddress;
                int destinationAddress = LetterForm.DestinationAddress;
                decimal fixedPrice = decimal.Parse(LetterForm.FixedPrice);

                upv.AddLetter(upv.AddressAt(originAddress), upv.AddressAt(destinationAddress), fixedPrice);
            }
        }
        #endregion

        #region Report Addresses
        // Precondition:  User has initiated the address report button
        // Postcondition: A report of addresses is displayed comprising all items from the address list
        private void ListAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string addressResult = null;
            List<Address> addressList = upv.AddressList;

            foreach (Address address in addressList)
            {
                addressResult += address + System.Environment.NewLine + "--------------------------------" + System.Environment.NewLine;
            }

            reportTxt.Text = $"List of Addresses ({upv.AddressCount})" + System.Environment.NewLine + "--------------------------------" + System.Environment.NewLine + System.Environment.NewLine + addressResult;
        }
        #endregion

        #region Report Parcels
        // Precondition:  User has initiated the address report button
        // Postcondition: A report of parcel is displayed comprising all items from the parcel list
        private void ListParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string parcelResult = null;
            List<Parcel> parcelList = upv.ParcelList;

            foreach (Parcel parcel in parcelList)
            {
                parcelResult += parcel + System.Environment.NewLine + "--------------------------------" + System.Environment.NewLine;
            }

            reportTxt.Text = $"List of Parcels ({upv.ParcelCount})" + System.Environment.NewLine + "--------------------------------" + System.Environment.NewLine + System.Environment.NewLine + parcelResult;
        }
        #endregion
    }
}                                                                             