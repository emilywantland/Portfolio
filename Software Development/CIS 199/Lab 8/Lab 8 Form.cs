// Grading ID: N2466
// Course Section: CIS 199-75
// Lab Number: 8
// Due Date: 3-31-2019
// Description: Program to calculate how much money you need to invest today
//              to earn a certain amount in the future.

using System;
using static System.Console;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Lab8Form : Form
    {
        public Lab8Form()
        {
            InitializeComponent();
        }

        // Calculate button event handler
        private void calcBtn_Click(object sender, EventArgs e)
        {
            double futureValue; // Future value of the dollar amount variable
            double.TryParse(futureValueInputTxt.Text, out futureValue); // Parsing input

            double interestRate; // Annual interest rate variable
            double.TryParse(annualInterestRateInputTxt.Text, out interestRate); // Parsing input

            int numOfYears; // Number of years sitting variable
            int.TryParse(numberOfYearsInputTxt.Text, out numOfYears); // Parsing input

            presentValueOutputLbl.Text = $"{CalcPresentValue(futureValue, interestRate, numOfYears):C}"; // Calling method and output statement
        }

        //Method to calculate net present value
        private double CalcPresentValue(double futureValue, double interestRate, int numOfYears) // Naming method, returns a double and will accept three parameters
        {
            double presentValue = (futureValue) / (Math.Pow(1 + interestRate, numOfYears)); // The present value variable and calculation

            return presentValue; // Returns the present value calculation
        }

        //Clear button event handler
        private void clearBtn_Click(object sender, EventArgs e)
        {
            futureValueInputTxt.Text = "";          // Clearing the future value box
            annualInterestRateInputTxt.Text = "";   // Clearing the interest rate box
            numberOfYearsInputTxt.Text = "";        // Clearing the number of years box
            presentValueOutputLbl.Text = "";        // Clearing the present value box
        }
    }
}
