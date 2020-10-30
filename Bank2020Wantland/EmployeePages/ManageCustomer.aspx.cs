using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bank2020Wantland.Pages
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BankData"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("insert into dsCustomerList(firstname, lastname, address, age) values('" + firstNameTxt.Text + "', '" + lastNameTxt.Text + "', '" + addressTxt.Text + "', '" + Convert.ToInt32(ageTxt.Text) + "')", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            successMessageLbl.Text = "Customer was sucessfully added";
        }
    }
}