// Grading ID: N2466
// Section: CIS 199-75
// Lab #: 9
// Due Date: 4-21-2019
// Description: When a valid month, day, or year is entered--the form's displayed date should change. 

using System;
using System.Windows.Forms;

namespace Lab9
{
    // Precondition:  None
    // Postcondition: Date object created and output
    public partial class DateForm : Form
    {
        public DateForm()
        {
            InitializeComponent();
        }

        // Declaring variables
        int myMonth;    // Month variable
        int myDay;      // Day variable
        int myYear;     // Year variable

        int validMonth; // Valid month variable, user input
        int validDay;   // Valid day variable, user input
        int validYear;  // Valid year variable, user input

        // Date object created
        Date myDate = new Date(1, 1, 2000);

        // Update Month Button
        private void updateMonthBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(monthInputTxt.Text, out validMonth)) // Tryparses input
            {
                myMonth = validMonth;
                Date myDate = new Date(myMonth, myDay, myYear);
                dateOutputStatementLbl.Text = $"{myDate}";
                monthInputTxt.Text = "";
            }
            else // Displays an error message
            {
                MessageBox.Show("Enter a number between 1-12.");
                monthInputTxt.Text = "";
            }
        }

        // Update Day Button
        private void updateDayBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dayInputTxt.Text, out validDay)) // Tryparses input
            {
                myDay = validDay;
                Date myDate = new Date(myMonth, myDay, myYear);
                dateOutputStatementLbl.Text = $"{myDate}";
                dayInputTxt.Text = "";
            }
            else // Displays an error message
            {
                MessageBox.Show("Enter a number between 1-31.");
                dayInputTxt.Text = "";
            }
        }

        // Update Year Button
        private void updateYearBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(yearInputTxt.Text, out validYear)) // Tryparses input
            {
                myYear = validYear;
                Date myDate = new Date(myMonth, myDay, myYear);
                dateOutputStatementLbl.Text = $"{myDate}";
                yearInputTxt.Text = "";
            }
            else // Displays an error message
            {
                MessageBox.Show("Enter a positive number.");
                yearInputTxt.Text = "";
            }
        }

        // Load event, assigns and displays an initial date
        public void DateForm_Load(object sender, EventArgs e)
        {
            myMonth = myDate.Month;
            myDay = myDate.Day;
            myYear = myDate.Year;

            dateOutputStatementLbl.Text = $"{myDate}";
        }
    }
}