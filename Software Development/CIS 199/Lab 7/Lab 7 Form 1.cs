// Grading ID: N2466
// Section Number: CIS 199-75
// Due Date: 3-24-2019
// Program Number: 7
// Description: Assigns grades based on words typed per minute.

using System;
using static System.Console;
using System.Windows.Forms;

namespace Lab_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
        }

        // Calculate button
        private void calcBtn_Click(object sender, EventArgs e)
        {
            // Arrays
            int[] wordsPerMinute = { 0, 16, 31, 51, 76 };       // Typed words per minute
            string[] letterGrade = { "F", "D", "C", "B", "A" }; // Letter grades to be assigned

            // Naming varibles
            bool found = false;  // Found bool variable
            int validWordsInput; // User's input

            // Error message for invalid input
            if (!int.TryParse(wordsTypedInputTxt.Text, out validWordsInput))
            {
                MessageBox.Show("Enter a valid number of words.");
            }

            // Assignment variable
            string studentGrade = ""; // Assume lowest grade for bad input

            // Try parsing the input
            int.TryParse(wordsTypedInputTxt.Text, out validWordsInput);

            // Starting at the correct position
            int index = wordsPerMinute.Length - 1;

            // While loop to search through the arrays
            while (index >= 0 && !found)
            {
                if (validWordsInput >= wordsPerMinute[index])
                    found = true;
                else
                    --index;
            }

            // Output statements
            if (found && int.TryParse(wordsTypedInputTxt.Text, out validWordsInput)) // Won't output defaut for incorrect input
            {
                studentGrade = letterGrade[index];
                gradeEarnedOutLbl.Text = ($"{studentGrade}");
            }
        }

        // Clear button
        private void clearBtn_Click(object sender, EventArgs e)
        {
            wordsTypedInputTxt.Text = "";
            gradeEarnedOutLbl.Text = "";
        }
    }
}
