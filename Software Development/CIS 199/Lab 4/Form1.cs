//Grading ID: N2466
//Course Section: CIS 199-75
//Lab Number: 4
//Due Date: 2-17-2019
//Description: Program to evaluate admission status & keep a running total of accepted students.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Status variables
        string status = ""; //Admission status variable
        int totalAccepted = 0; //Running accepted total variable
        int totalRejected = 0; //Running rejected total variable

        //Enter button that calculates application status
        private void enterBtn_Click(object sender, EventArgs e)
        {
            //Naming variables
            int admissionScore; //Admission score variable
            double hsGPA; //High school GPA variable

            //Naming Constants
            const double neededGPA = 3.0; //Necessary GPA constant variable
            const int lowTestScore = 60; //Necessary low test score constant variable
            const int highTestScore = 80; //Necessary high test score constant variable

            //If statements
            if (double.TryParse(hsGPATxtBox.Text, out hsGPA))
            {
                if (int.TryParse(admissionsTSTxtBox.Text, out admissionScore))
                {
                    if (hsGPA >= neededGPA && admissionScore >= lowTestScore)
                        status = "Accepted";

                    else if (hsGPA <= neededGPA && admissionScore >= highTestScore)
                        status = "Accepted";
                    else
                        status = "Rejected";
                }
            }

            //Output if statements
            applicationStatusOutLbl.Text = $"{status}";

            if (status == "Accepted")
            {
                totalAccepted = (totalAccepted + 1);
            }
            runningTotalOutLbl.Text = $"{totalAccepted}";

            if (status == "Rejected")
            {
                totalRejected = (totalRejected + 1);
            }
            runningRejectedTotalLbl.Text = $"{totalRejected}";
        }
    }
}
