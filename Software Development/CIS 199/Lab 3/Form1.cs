//Grading ID: N2466
//Course Section: CIS 199-75
//Lab Number: 3
//Due Date: 2-10-19
//Description: Program to calculate diameter, surface area, and volume from the radius of a sphere.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class lab3Form : Form
    {
        public lab3Form()
        {
            InitializeComponent();
        }

        //Button to calculate diameter, surface area, and volume. 
        private void calcBtn_Click(object sender, EventArgs e)
        {

            //Naming the variables
            double diameter; //Diameter output variable
            double surfaceArea; //Surface Area output variable
            double volume; //Volume output variable
            double radiusOfSphere; //Radius of Sphere input variable

            //Parsing the text
            radiusOfSphere = double.Parse(radiusOfSphereTxt.Text);

            //Naming the calculations
            diameter = 2 * radiusOfSphere;
            surfaceArea = 4 * Math.PI * Math.Pow(radiusOfSphere, 2);
            volume = (4 * Math.PI * Math.Pow(radiusOfSphere, 3)) / (3);

            //Output statements
            diameterOutLbl.Text = $"{diameter:F2}";
            surfaceAreaOutLbl.Text = $"{surfaceArea:F2}";
            volumeOutLbl.Text = $"{volume:F2}";
        }
    }
}
