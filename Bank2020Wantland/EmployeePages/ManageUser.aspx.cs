using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Bank2020Wantland.Pages
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BankData"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("insert into dsUserInformation(firstname, lastname, username, password) values('" + firstNameTxt.Text + "', '" + lastNameTxt.Text + "', '" + usernameTxt.Text + "', '" + passwordTxt.Text + "')", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            successMessageLbl.Text = "User was sucessfully added";
        }
    }
}