//Grading ID: N2466
//Program #: 2
//Section: CIS 199-75
//Due Date: 3/5/2019
//Description: Calculating marginal tax rate and the amount due.

using System;
using static System.Console;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program2
{
    public partial class Prog2Form : Form
    {
        public Prog2Form()
        {
            InitializeComponent();
        }

        private void calcBtn_Click(object sender, EventArgs e)
        {
            //Declaring variables
            double income; //Entered income value
            double taxDue = 0; //Output payment
            double taxRate = 0; //Output rate

            //Marginal tax rates
            const double rate1 = 0.10; //Tax rate 1
            const double rate2 = 0.12; //Tax rate 2
            const double rate3 = 0.22; //Tax rate 3
            const double rate4 = 0.24; //Tax rate 4
            const double rate5 = 0.32; //Tax rate 5
            const double rate6 = 0.35; //Tax rate 6
            const double rate7 = 0.37; //Tax rate 7

            //Single thresholds
            const int singleThreshold1 = 9700; //Single threshold 1
            const int singleThreshold2 = 39475; //Single threshold 2
            const int singleThreshold3 = 84200; //Single threshold 3
            const int singleThreshold4 = 160725; //Single threshold 4
            const int singleThreshold5 = 204100; //Single threshold 5
            const int singleThreshold6 = 510300; //Single threshold 6

            //Married filing jointly thresholds
            const int marriedjointThreshold1 = 19400; //Married joint threshold 1
            const int marriedjointThreshold2 = 78950; //Married joint threshold 2
            const int marriedjointThreshold3 = 168400; //Married joint threshold 3
            const int marriedjointThreshold4 = 321450; //Married joint threshold 4
            const int marriedjointThreshold5 = 408200; //Married joint threshold 5
            const int marriedjointThreshold6 = 612350; //Married joint threshold 6

            //Head of household thresholds
            const int headofhouseThreshold1 = 13850; //Head of household threshold 1
            const int headofhouseThreshold2 = 52850; //Head of household threshold 2
            const int headofhouseThreshold3 = 84200; //Head of household threshold 3
            const int headofhouseThreshold4 = 160700; //Head of household threshold 4
            const int headofhouseThreshold5 = 204100; //Head of household threshold 5
            const int headofhouseThreshold6 = 510300; //Head of household threshold 6

            //Married filing separately thresholds
            const int marriedseparateThreshold1 = 9700; //Married separate threshold 1
            const int marriedseparateThreshold2 = 39475; //Married separate threshold 2
            const int marriedseparateThreshold3 = 84200; //Married separate threshold 3
            const int marriedseparateThreshold4 = 160725; //Married separate threshold 4
            const int marriedseparateThreshold5 = 204100; //Married separate threshold 5
            const int marriedseparateThreshold6 = 306175; //Married separate threshold 6

            //Error message for invalid input
            if (!double.TryParse(taxIncomeTxt.Text, out income) || income == 0)
            {
                MessageBox.Show("Enter a valid income.");
            }

            //Single if statements, determines the tax rate and tax due through calculations
            if (singleRB.Checked)
            {
                if (double.TryParse(taxIncomeTxt.Text, out income))
                {

                    if (income > singleThreshold6)
                    {
                        taxRate = rate7;

                        taxDue = (singleThreshold1 * rate1);
                        taxDue = taxDue + ((singleThreshold2 - singleThreshold1) * rate2);
                        taxDue = taxDue + ((singleThreshold3 - singleThreshold2) * rate3);
                        taxDue = taxDue + ((singleThreshold4 - singleThreshold3) * rate4);
                        taxDue = taxDue + ((singleThreshold5 - singleThreshold4) * rate5);
                        taxDue = taxDue + ((singleThreshold6 - singleThreshold5) * rate6);
                        taxDue = taxDue + ((income - singleThreshold6) * rate7);
                    }

                    else if (income >= singleThreshold5)
                    {
                        taxRate = rate6;

                        taxDue = (singleThreshold1 * rate1);
                        taxDue = taxDue + ((singleThreshold2 - singleThreshold1) * rate2);
                        taxDue = taxDue + ((singleThreshold3 - singleThreshold2) * rate3);
                        taxDue = taxDue + ((singleThreshold4 - singleThreshold3) * rate4);
                        taxDue = taxDue + ((singleThreshold5 - singleThreshold4) * rate5);
                        taxDue = taxDue + ((income - singleThreshold5) * rate6);
                    }

                    else if (income >= singleThreshold4)
                    {
                        taxRate = rate5;

                        taxDue = (singleThreshold1 * rate1);
                        taxDue = taxDue + ((singleThreshold2 - singleThreshold1) * rate2);
                        taxDue = taxDue + ((singleThreshold3 - singleThreshold2) * rate3);
                        taxDue = taxDue + ((singleThreshold4 - singleThreshold3) * rate4);
                        taxDue = taxDue + ((income - singleThreshold4) * rate5);
                    }

                    else if (income >= singleThreshold3)
                    {
                        taxRate = rate4;

                        taxDue = (singleThreshold1 * rate1);
                        taxDue = taxDue + ((singleThreshold2 - singleThreshold1) * rate2);
                        taxDue = taxDue + ((singleThreshold3 - singleThreshold2) * rate3);
                        taxDue = taxDue + ((income - singleThreshold3) * rate4);
                    }

                    else if (income >= singleThreshold2)
                    {
                        taxRate = rate3;

                        taxDue = (singleThreshold1 * rate1);
                        taxDue = taxDue + ((singleThreshold2 - singleThreshold1) * rate2);
                        taxDue = taxDue + ((income - singleThreshold2) * rate3);
                    }

                    else if (income >= singleThreshold1)
                    {
                        taxRate = rate2;

                        taxDue = (singleThreshold1 * rate1);
                        taxDue = taxDue + ((income - singleThreshold1) * rate2);
                    }

                    else 
                    {
                        taxRate = rate1;

                        taxDue = (income * rate1);
                    }
                }
            }

            //Married filing jointly if statements, determines the tax rate and tax due through calculations
            if (marriedJointRB.Checked)
            {
                if (double.TryParse(taxIncomeTxt.Text, out income))
                {
                    if (income > marriedjointThreshold6)
                    {
                        taxRate = rate7;

                        taxDue = (marriedjointThreshold1 * rate1);
                        taxDue = taxDue + ((marriedjointThreshold2 - marriedjointThreshold1) * rate2);
                        taxDue = taxDue + ((marriedjointThreshold3 - marriedjointThreshold2) * rate3);
                        taxDue = taxDue + ((marriedjointThreshold4 - marriedjointThreshold3) * rate4);
                        taxDue = taxDue + ((marriedjointThreshold5 - marriedjointThreshold4) * rate5);
                        taxDue = taxDue + ((marriedjointThreshold6 - marriedjointThreshold5) * rate6);
                        taxDue = taxDue + ((income - marriedjointThreshold6) * rate7);
                    }

                    else if (income >= marriedjointThreshold5)
                    {
                        taxRate = rate6;

                        taxDue = (marriedjointThreshold1 * rate1);
                        taxDue = taxDue + ((marriedjointThreshold2 - marriedjointThreshold1) * rate2);
                        taxDue = taxDue + ((marriedjointThreshold3 - marriedjointThreshold2) * rate3);
                        taxDue = taxDue + ((marriedjointThreshold4 - marriedjointThreshold3) * rate4);
                        taxDue = taxDue + ((marriedjointThreshold5 - marriedjointThreshold4) * rate5);
                        taxDue = taxDue + ((income - marriedjointThreshold5) * rate6);
                    }

                    else if (income >= marriedjointThreshold4)
                    {
                        taxRate = rate5;

                        taxDue = (marriedjointThreshold1 * rate1);
                        taxDue = taxDue + ((marriedjointThreshold2 - marriedjointThreshold1) * rate2);
                        taxDue = taxDue + ((marriedjointThreshold3 - marriedjointThreshold2) * rate3);
                        taxDue = taxDue + ((marriedjointThreshold4 - marriedjointThreshold3) * rate4);
                        taxDue = taxDue + ((income - marriedjointThreshold4) * rate5);
                    }

                    else if (income >= marriedjointThreshold3)
                    {
                        taxRate = rate4;

                        taxDue = (marriedjointThreshold1 * rate1);
                        taxDue = taxDue + ((marriedjointThreshold2 - marriedjointThreshold1) * rate2);
                        taxDue = taxDue + ((marriedjointThreshold3 - marriedjointThreshold2) * rate3);
                        taxDue = taxDue + ((income - marriedjointThreshold3) * rate4);
                    }

                    else if (income >= marriedjointThreshold2)
                    {
                        taxRate = rate3;

                        taxDue = (marriedjointThreshold1 * rate1);
                        taxDue = taxDue + ((marriedjointThreshold2 - marriedjointThreshold1) * rate2);
                        taxDue = taxDue + ((income - marriedjointThreshold2) * rate3);
                    }

                    else if (income >= marriedjointThreshold1)
                    {
                        taxRate = rate2;

                        taxDue = (marriedjointThreshold1 * rate1);
                        taxDue = taxDue + ((income - marriedjointThreshold1) * rate2);
                    }

                    else
                    {
                        taxRate = rate1;

                        taxDue = (income * rate1);
                    }
                }
            }

            //Head of household if statements, determines the tax rate and tax due through calculations
            if (headofHouseRB.Checked)
            {
                if (double.TryParse(taxIncomeTxt.Text, out income))
                {
                    if (income > headofhouseThreshold6)
                    {
                        taxRate = rate7;

                        taxDue = (headofhouseThreshold1 * rate1);
                        taxDue = taxDue + ((headofhouseThreshold2 - headofhouseThreshold1) * rate2);
                        taxDue = taxDue + ((headofhouseThreshold3 - headofhouseThreshold2) * rate3);
                        taxDue = taxDue + ((headofhouseThreshold4 - headofhouseThreshold3) * rate4);
                        taxDue = taxDue + ((headofhouseThreshold5 - headofhouseThreshold4) * rate5);
                        taxDue = taxDue + ((headofhouseThreshold6 - headofhouseThreshold5) * rate6);
                        taxDue = taxDue + ((income - headofhouseThreshold6) * rate7);
                    }

                    else if (income >= headofhouseThreshold5)
                    {
                        taxRate = rate6;

                        taxDue = (headofhouseThreshold1 * rate1);
                        taxDue = taxDue + ((headofhouseThreshold2 - headofhouseThreshold1) * rate2);
                        taxDue = taxDue + ((headofhouseThreshold3 - headofhouseThreshold2) * rate3);
                        taxDue = taxDue + ((headofhouseThreshold4 - headofhouseThreshold3) * rate4);
                        taxDue = taxDue + ((headofhouseThreshold5 - headofhouseThreshold4) * rate5);
                        taxDue = taxDue + ((income - headofhouseThreshold5) * rate6);
                    }

                    else if (income >= headofhouseThreshold4)
                    {
                        taxRate = rate5;

                        taxDue = (headofhouseThreshold1 * rate1);
                        taxDue = taxDue + ((headofhouseThreshold2 - headofhouseThreshold1) * rate2);
                        taxDue = taxDue + ((headofhouseThreshold3 - headofhouseThreshold2) * rate3);
                        taxDue = taxDue + ((headofhouseThreshold4 - headofhouseThreshold3) * rate4);
                        taxDue = taxDue + ((income - headofhouseThreshold4) * rate5);
                    }

                    else if (income >= headofhouseThreshold3)
                    {
                        taxRate = rate4;

                        taxDue = (headofhouseThreshold1 * rate1);
                        taxDue = taxDue + ((headofhouseThreshold2 - headofhouseThreshold1) * rate2);
                        taxDue = taxDue + ((headofhouseThreshold3 - headofhouseThreshold2) * rate3);
                        taxDue = taxDue + ((income - headofhouseThreshold3) * rate4);
                    }

                    else if (income >= headofhouseThreshold2)
                    {
                        taxRate = rate3;

                        taxDue = (headofhouseThreshold1 * rate1);
                        taxDue = taxDue + ((headofhouseThreshold2 - headofhouseThreshold1) * rate2);
                        taxDue = taxDue + ((income - headofhouseThreshold2) * rate3);
                    }

                    else if (income >= headofhouseThreshold1)
                    {
                        taxRate = rate2;

                        taxDue = (headofhouseThreshold1 * rate1);
                        taxDue = taxDue + ((income - headofhouseThreshold1) * rate2);
                    }

                    else
                    {
                        taxRate = rate1;

                        taxDue = (income * rate1);
                    }

                }
            }

            //Married filing separately if statements, determines the tax rate and tax due through calculations
            if (marriedSeparateRB.Checked)
            {
                if (double.TryParse(taxIncomeTxt.Text, out income))
                {
                    if (income > marriedseparateThreshold6)
                    {
                        taxRate = rate7;

                        taxDue = (marriedseparateThreshold1 * rate1);
                        taxDue = taxDue + ((marriedseparateThreshold2 - marriedseparateThreshold1) * rate2);
                        taxDue = taxDue + ((marriedseparateThreshold3 - marriedseparateThreshold2) * rate3);
                        taxDue = taxDue + ((marriedseparateThreshold4 - marriedseparateThreshold3) * rate4);
                        taxDue = taxDue + ((marriedseparateThreshold5 - marriedseparateThreshold4) * rate5);
                        taxDue = taxDue + ((marriedseparateThreshold6 - marriedseparateThreshold5) * rate6);
                        taxDue = taxDue + ((income - marriedseparateThreshold6) * rate7);
                    }

                    else if (income >= marriedseparateThreshold5)
                    {
                        taxRate = rate6;

                        taxDue = (marriedseparateThreshold1 * rate1);
                        taxDue = taxDue + ((marriedseparateThreshold2 - marriedseparateThreshold1) * rate2);
                        taxDue = taxDue + ((marriedseparateThreshold3 - marriedseparateThreshold2) * rate3);
                        taxDue = taxDue + ((marriedseparateThreshold4 - marriedseparateThreshold3) * rate4);
                        taxDue = taxDue + ((marriedseparateThreshold5 - marriedseparateThreshold4) * rate5);
                        taxDue = taxDue + ((income - marriedseparateThreshold5) * rate6);
                    }

                    else if (income >= marriedseparateThreshold4)
                    {
                        taxRate = rate5;

                        taxDue = (marriedseparateThreshold1 * rate1);
                        taxDue = taxDue + ((marriedseparateThreshold2 - marriedseparateThreshold1) * rate2);
                        taxDue = taxDue + ((marriedseparateThreshold3 - marriedseparateThreshold2) * rate3);
                        taxDue = taxDue + ((marriedseparateThreshold4 - marriedseparateThreshold3) * rate4);
                        taxDue = taxDue + ((income - marriedseparateThreshold4) * rate5);
                    }

                    else if (income >= marriedseparateThreshold3)
                    {
                        taxRate = rate4;

                        taxDue = (marriedseparateThreshold1 * rate1);
                        taxDue = taxDue + ((marriedseparateThreshold2 - marriedseparateThreshold1) * rate2);
                        taxDue = taxDue + ((marriedseparateThreshold3 - marriedseparateThreshold2) * rate3);
                        taxDue = taxDue + ((income - marriedseparateThreshold3) * rate4);
                    }

                    else if (income >= marriedseparateThreshold2)
                    {
                        taxRate = rate3;

                        taxDue = (marriedseparateThreshold1 * rate1);
                        taxDue = taxDue + ((marriedseparateThreshold2 - marriedseparateThreshold1) * rate2);
                        taxDue = taxDue + ((income - marriedseparateThreshold2) * rate3);
                    }

                    else if (income >= marriedseparateThreshold1)
                    {
                        taxRate = rate2;

                        taxDue = (marriedseparateThreshold1 * rate1);
                        taxDue = taxDue + ((income - marriedseparateThreshold1) * rate2);
                    }

                    else
                    {
                        taxRate = rate1;

                        taxDue = (income * rate1);
                    }
                }
            }

            //Output statements
            if(double.TryParse(taxIncomeTxt.Text, out income) && income != 0)
            {
            marginalOutputLbl.Text = $"{taxRate:P0}";
            amountDueOutputLbl.Text = $"{taxDue:C}";
            }
            
        }

        //Resets the form
        private void resetBtn_Click(object sender, EventArgs e)
        {
            taxIncomeTxt.Text = String.Empty;
            marginalOutputLbl.Text = String.Empty;
            amountDueOutputLbl.Text = String.Empty;
            singleRB.Checked = true;
        }
    }
}