using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Bank2020Wantland.Pages
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string loginConnectionString = ConfigurationManager.ConnectionStrings["BankData"].ConnectionString;
            SqlConnection login = new SqlConnection(loginConnectionString);
            login.Open();

            SqlCommand open = new SqlCommand("select * from dsUserInformation where username=@user and password=@pass", login);
            open.Parameters.AddWithValue("@user", txtUserName.Text);
            open.Parameters.AddWithValue("@pass", txtPassword.Text);
            SqlDataReader dataReaders = open.ExecuteReader();
            bool pass = false;
            while(dataReaders.Read())
            {
                if (dataReaders.HasRows == true)
                {
                    string fName = dataReaders["firstname"].ToString();
                    string lName = dataReaders["lastname"].ToString();
                    
                    Session["firstname"] = fName;
                    Session["lastname"] = lName;
                    Response.Redirect("~/EmployeePages/MainPage.aspx");
                    pass = true;
                }
            }
            if(!pass)
            {
                incorrectUserOrPass.Text = "Incorrect username or password!";
            }
        }
    }
}