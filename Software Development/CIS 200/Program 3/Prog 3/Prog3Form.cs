// Program 3
// CIS 200-01
// Fall 2019
// Due: 11/11/2019
// By: M1791

// File: Prog3Form.cs
// This class creates the main GUI for Program 3. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, a Report menu with List Addresses and List Parcels
// items, Opens and Saves items, and edits Address objects. 

using Prog2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class Prog3Form : Form
    {
        private UserParcelView upv; // The UserParcelView
        private FileStream output; // Stream for writing to a file
        private FileStream input; // Stream for opening a file
        private BinaryFormatter reader = new BinaryFormatter(); // Deserializes a file
        private BinaryFormatter writer = new BinaryFormatter(); // Serializes a file

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display.
        public Prog3Form()
        {
            InitializeComponent();

            upv = new UserParcelView();

            // Commented Out
            #region Test Address Data
            //// Test Data - Magic Numbers OK
            //upv.AddAddress("  John Smith  ", "   123 Any St.   ", "  Apt. 45 ",
            //    "  Louisville   ", "  KY   ", 40202); // Test Address 1
            //upv.AddAddress("Jane Doe", "987 Main St.",
            //    "Beverly Hills", "CA", 90210); // Test Address 2
            //upv.AddAddress("James Kirk", "654 Roddenberry Way", "Suite 321",
            //    "El Paso", "TX", 79901); // Test Address 3
            //upv.AddAddress("John Crichton", "678 Pau Place", "Apt. 7",
            //    "Portland", "ME", 04101); // Test Address 4
            //upv.AddAddress("John Doe", "111 Market St.", "",
            //    "Jeffersonville", "IN", 47130); // Test Address 5
            //upv.AddAddress("Jane Smith", "55 Hollywood Blvd.", "Apt. 9",
            //    "Los Angeles", "CA", 90212); // Test Address 6
            //upv.AddAddress("Captain Robert Crunch", "21 Cereal Rd.", "Room 987",
            //    "Bethesda", "MD", 20810); // Test Address 7
            //upv.AddAddress("Vlad Dracula", "6543 Vampire Way", "Apt. 1",
            //    "Bloodsucker City", "TN", 37210); // Test Address 8
            #endregion

            // Commented Out
            #region Test Parcel Data
            //upv.AddLetter(upv.AddressAt(0), upv.AddressAt(1), 3.95M);                     // Letter test object
            //upv.AddLetter(upv.AddressAt(2), upv.AddressAt(3), 4.25M);                     // Letter test object
            //upv.AddGroundPackage(upv.AddressAt(4), upv.AddressAt(5), 14, 10, 5, 12.5);    // Ground test object
            //upv.AddGroundPackage(upv.AddressAt(6), upv.AddressAt(7), 8.5, 9.5, 6.5, 2.5); // Ground test object
            //upv.AddNextDayAirPackage(upv.AddressAt(0), upv.AddressAt(2), 25, 15, 15,      // Next Day test object
            //    85, 7.50M);
            //upv.AddNextDayAirPackage(upv.AddressAt(2), upv.AddressAt(4), 9.5, 6.0, 5.5,   // Next Day test object
            //    5.25, 5.25M);
            //upv.AddNextDayAirPackage(upv.AddressAt(1), upv.AddressAt(6), 10.5, 6.5, 9.5,  // Next Day test object
            //    15.5, 5.00M);
            //upv.AddTwoDayAirPackage(upv.AddressAt(4), upv.AddressAt(6), 46.5, 39.5, 28.0, // Two Day test object
            //    80.5, TwoDayAirPackage.Delivery.Saver);
            //upv.AddTwoDayAirPackage(upv.AddressAt(7), upv.AddressAt(0), 15.0, 9.5, 6.5,   // Two Day test object
            //    75.5, TwoDayAirPackage.Delivery.Early);
            //upv.AddTwoDayAirPackage(upv.AddressAt(5), upv.AddressAt(3), 12.0, 12.0, 6.0,  // Two Day test object
            //    5.5, TwoDayAirPackage.Delivery.Saver);
            #endregion
        }

        #region About Button
        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 2{NL}By: Andrew L. Wright{NL}CIS 200{NL}Fall 2019",
                "About Program 2");
        }
        #endregion

        #region Exit Button
        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Address Forms
        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result
            int zip; // Address zip code

            if (result == DialogResult.OK) // Only add if OK
            {
                if (int.TryParse(addressForm.ZipText, out zip))
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        zip); // Use form's properties to create address
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
                                   // Alternatively, use with using clause as in Ch. 17
        }
        #endregion

        #region List Addresses
        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // -- OR --
            // Not using StringBuilder, just use TextBox directly

            //reportTxt.Clear();
            //reportTxt.AppendText("Addresses:");
            //reportTxt.AppendText(NL); // Remember, \n doesn't always work in GUIs
            //reportTxt.AppendText(NL);

            //foreach (Address a in upv.AddressList)
            //{
            //    reportTxt.AppendText(a.ToString());
            //    reportTxt.AppendText(NL);
            //    reportTxt.AppendText("------------------------------");
            //    reportTxt.AppendText(NL);
            //}

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }
        #endregion

        #region Letter Form
        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog
            decimal fixedCost;     // The letter's cost

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return; // Exit now since can't create valid letter
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                if (decimal.TryParse(letterForm.FixedCostText, out fixedCost))
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        fixedCost); // Letter to be inserted
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
                                  // Alternatively, use with using clause as in Ch. 17
        }
        #endregion

        #region List Parcels
        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }
        #endregion

        #region Open File
        // Precondition:  File, Open menu item activated
        // Postcondition: The application opens a dialog box and opens a file containing address objects
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create and show dialog box enabling user to open file         
            DialogResult result; // Result of OpenFileDialog
            string fileName; // Name of file containing data

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // Get Specified Name
            }

            // Ensure that user clicked "OK"
            if (result == DialogResult.OK)
            {
                // Show error if user specified invalid file
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Invalid File Name", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        // Create FileStream to obtain read access to file
                        FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        upv = (UserParcelView)reader.Deserialize(input);

                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error reading from file",
                           "File Error", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (input != null)
                        {
                            input.Close();
                        }
                    }
                }
            }
        }
        #endregion

        #region Save As File
        // Precondition:  File, Save As menu item activated
        // Postcondition: The application opens a dialog box and savea address objects to a file
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create and show dialog box enabling user to save file
            DialogResult result;
            string fileName; // Name of file to save data

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // Let user create file

                // Retrieve the result of the dialog box
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // Get specified file name
            }

            // Ensure that user clicked "OK"
            if (result == DialogResult.OK)
            {
                // Show error if user specified invalid file
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Invalid File Name", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Save file via FileStream if user specified valid file
                    try
                    {
                        // Open file with write access
                        output = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                        writer.Serialize(output, upv);
                    }
                    catch (IOException)
                    {
                        // Notify user if file could not be opened
                        MessageBox.Show("Error opening file", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (output != null)
                        {
                            output.Close();
                        }
                    }
                }
            }
        }
        #endregion

        #region Edit Address
        // Precondition:  File, Edit menu item activated
        // Postcondition: The application opens a dialog box and...
        private void AddressEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditAddressForm editAddress; // The edit address form
            DialogResult result; // The result of the dialog box
            editAddress = new EditAddressForm(upv.AddressList);
            result = editAddress.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                int index; // Selected combo box index number
                index = editAddress.EditAddressIndex;

                if (index >= 0)
                {
                    Address add = upv.AddressAt(index);

                    AddressForm addressForm = new AddressForm(); // The address dialog box form

                    addressForm.AddressName = add.Name;
                    addressForm.Address1 = add.Address1;
                    addressForm.Address2 = add.Address2;
                    addressForm.City = add.City;
                    addressForm.State = add.State;
                    addressForm.ZipText = add.Zip.ToString();

                    DialogResult result2 = addressForm.ShowDialog(); // Show form as dialog and store result

                    if (result2 == DialogResult.OK)
                    {
                        add.Name = addressForm.AddressName;
                        add.Address1 = addressForm.Address1;
                        add.Address2 = addressForm.Address2;
                        add.City = addressForm.City;
                        add.State = addressForm.State;
                        add.Zip = int.Parse(addressForm.ZipText);
                    }
                    addressForm.Dispose();
                }
            }
            editAddress.Dispose();
        }
        #endregion
    }
}