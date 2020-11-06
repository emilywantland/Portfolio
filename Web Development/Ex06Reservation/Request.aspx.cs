using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XEx06Reservation
{
    public partial class Request : System.Web.UI.Page
    {
        private string currentDate = DateTime.Today.ToShortDateString();
        private string currentYear = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtArrivalDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDepartureDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            lblYear.Text = currentYear;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSpecialRequests.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtArrivalDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDepartureDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            dropDownNumberOfPeople.SelectedIndex = 0;
            dropDownPreferredMethod.SelectedIndex = 0;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Thank you for your request. We will get back to you within 24 hours.";
        }
    }
}