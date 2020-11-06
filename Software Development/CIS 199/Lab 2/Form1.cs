using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Lab2 : Form
    {
        public Lab2()
        {
            InitializeComponent();
        }

        private void calculateTipBtn_Click(object sender, EventArgs e)
        {

            const double TWENTY_RATE = 0.20;
            const double EIGHTEEN_RATE = 0.18;
            const double FIFTEEN_RATE = 0.15;

            double priceOfMeal;
            double fifteen;
            double eighteen;
            double twenty;

            priceOfMeal = double.Parse(priceOfMealTxt.Text);

            twenty = priceOfMeal * TWENTY_RATE;
            eighteen = priceOfMeal * EIGHTEEN_RATE;
            fifteen = priceOfMeal * FIFTEEN_RATE;

            fifteenPercentLbl.Text = $"${fifteen:F2}";
            eighteenPercentLbl.Text = $"${eighteen:F2}";
            twentyPercentLbl.Text = $"${twenty:F2}";

        }
    }
}
