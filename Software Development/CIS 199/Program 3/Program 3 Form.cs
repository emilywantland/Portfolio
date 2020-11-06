//Grading ID: N2466
//Section Number: CIS 199-75
//Due Date: 3-28-2019
//Program Number: 3
//Description: Calculates tax due and assigns a tax rate.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program3
{
    public partial class Prog3Form : Form
    {
        public Prog3Form()
        {
            InitializeComponent();
        }

        // Single Filers
        private const int SINGLE1 = 9700;   // 1st single threshold (LOWEST)
        private const int SINGLE2 = 39475;  // 2nd single threshold
        private const int SINGLE3 = 84200;  // 3rd single threshold
        private const int SINGLE4 = 160725; // 4th single threshold
        private const int SINGLE5 = 204100; // 5th single threshold
        private const int SINGLE6 = 510300; // 6th single threshold (HIGHEST)

        //Married Filing Separately
        private const int SEPARATELY1 = 9700;   // 1st married-separately threshold (LOWEST)
        private const int SEPARATELY2 = 39475;  // 2nd married-separately threshold
        private const int SEPARATELY3 = 84200;  // 3rd married-separately threshold
        private const int SEPARATELY4 = 160725; // 4th married-separately threshold
        private const int SEPARATELY5 = 204100; // 5th married-separately threshold
        private const int SEPARATELY6 = 306175; // 6th married-separately threshold (HIGHEST)

        // Married Filing Jointly
        private const int JOINTLY1 = 19400;  // 1st married-jointly threshold (LOWEST)
        private const int JOINTLY2 = 78950;  // 2nd married-jointly threshold
        private const int JOINTLY3 = 168400; // 3rd married-jointly threshold
        private const int JOINTLY4 = 321450; // 4th married-jointly threshold
        private const int JOINTLY5 = 408200; // 5th married-jointly threshold
        private const int JOINTLY6 = 612350; // 6th married-jointly threshold (HIGHEST)

        // Head of Household
        private const int HOH1 = 13850;  // 1st head of household threshold (LOWEST)
        private const int HOH2 = 52850;  // 2nd head of household threshold
        private const int HOH3 = 84200;  // 3rd head of household threshold
        private const int HOH4 = 160700; // 4th head of household threshold
        private const int HOH5 = 204100; // 5th head of household threshold
        private const int HOH6 = 510300; // 6th head of household threshold (HIGHEST)

        // Income threshold values that apply to this filer
        private int threshold1; // 1st income threshold
        private int threshold2; // 2nd income threshold
        private int threshold3; // 3rd income threshold
        private int threshold4; // 4th income threshold
        private int threshold5; // 5th income threshold
        private int threshold6; // 6th income threshold

        //Calculate button event handler
        private void calcTaxBtn_Click(object sender, EventArgs e)
        {
            // Tax rates
            const decimal RATE1 = 0.10m; // 1st tax rate (LOWEST)
            const decimal RATE2 = 0.12m; // 2nd tax rate
            const decimal RATE3 = 0.22m; // 3rd tax rate
            const decimal RATE4 = 0.24m; // 4th tax rate
            const decimal RATE5 = 0.32m; // 5th tax rate
            const decimal RATE6 = 0.35m; // 6th tax rate
            const decimal RATE7 = 0.37m; // 7th tax rate (HIGHEST)

            // Naming arrays
            decimal[] rates = { RATE1, RATE2, RATE3, RATE4, RATE5, RATE6, RATE7 }; // Tax rates array
            int[] thresholds = new int[rates.Length]; // Creating new array with rate length

            // Assigning values to thresholds array
            thresholds[0] = 1;              // Assigning threshold position 0
            thresholds[1] = threshold1 + 1; // Assigning threshold position 1
            thresholds[2] = threshold2 + 1; // Assigning threshold position 2
            thresholds[3] = threshold3 + 1; // Assigning threshold position 3
            thresholds[4] = threshold4 + 1; // Assigning threshold position 4
            thresholds[5] = threshold5 + 1; // Assigning threshold position 5
            thresholds[6] = threshold6 + 1; // Assigning threshold position 6

            // Naming variables
            bool found = false; // Found bool variable
            int validIncomeInput; // User's input
            decimal assignedTax = 0m; // Assume lowest rate for bad input

            // Starting at the correct position
            int index = thresholds.Length - 1;

            // Declaring variables
            decimal tax = 0; // Filer's calculated income tax due
            decimal currentTax = 0; // Filer's tax for current tier

            //If statement for valid input
            if (int.TryParse(incomeTxt.Text, out validIncomeInput) && validIncomeInput >= 0)
            {
                // While loop to search through the arrays
                while (index >= 0 && !found)
                {
                    if (validIncomeInput >= thresholds[index])
                    {
                        found = true;
                        assignedTax = rates[index];
                    }
                    else
                    {
                        --index;
                    }
                }

                if (index == thresholds.Length - 1)
                {
                    currentTax = (validIncomeInput - threshold6) * RATE7;
                    --index;
                    tax += currentTax;
                }

                // Calculation loop
                if (int.TryParse(incomeTxt.Text, out validIncomeInput)) // Won't start without proper value
                {
                    for (int counter = 0; counter <= index; ++counter)
                    {
                        currentTax = Math.Min((validIncomeInput - (thresholds[counter] - 1)), (thresholds[counter + 1] - thresholds[counter])) * rates[counter];
                        tax += currentTax;
                    }
                }

                // Output statements
                if (found && int.TryParse(incomeTxt.Text, out validIncomeInput)) //Won't output defaut for incorrect input
                {
                    marginalRateOutLbl.Text = ($"{assignedTax:P0}");
                    taxOutLbl.Text = ($"{tax:C}");
                }
            }

            // Error message for invalid input
            else
            {
                    MessageBox.Show("Enter a valid income.");
            }
               
        }

        // Sets default filing status as Single
        private void Prog3Form_Load(object sender, EventArgs e)
        {
            {
             singleRdoBtn.Checked = true; // Single by default, will raise CheckedChanged event
            }
        }

        // User has checked/unchecked Single radio button
        // Updates income thresholds
        private void singleRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (singleRdoBtn.Checked)
            {
                threshold1 = SINGLE1;
                threshold2 = SINGLE2;
                threshold3 = SINGLE3;
                threshold4 = SINGLE4;
                threshold5 = SINGLE5;
                threshold6 = SINGLE6;
            }
        }

        // User has checked/unchecked Married Filing Separately radio button
        // Updates income thresholds
        private void separatelyRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            {
                threshold1 = SEPARATELY1;
                threshold2 = SEPARATELY2;
                threshold3 = SEPARATELY3;
                threshold4 = SEPARATELY4;
                threshold5 = SEPARATELY5;
                threshold6 = SEPARATELY6;
            }
        }

        // User has checked/unchecked Married Filing Jointly radio button
        // Updates income thresholds
        private void jointlyRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            {
                threshold1 = JOINTLY1;
                threshold2 = JOINTLY2;
                threshold3 = JOINTLY3;
                threshold4 = JOINTLY4;
                threshold5 = JOINTLY5;
                threshold6 = JOINTLY6;
            }
        }

        // User has checked/unchecked Head of Household radio button
        // Updates income thresholds
        private void headOfHouseRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            {
                threshold1 = HOH1;
                threshold2 = HOH2;
                threshold3 = HOH3;
                threshold4 = HOH4;
                threshold5 = HOH5;
                threshold6 = HOH6;
            }
        }

        //Clear form button
        private void clearBtn_Click(object sender, EventArgs e)
        {
            incomeTxt.Text = "";
            marginalRateOutLbl.Text = "";
            taxOutLbl.Text = "";
        }
    }
}
