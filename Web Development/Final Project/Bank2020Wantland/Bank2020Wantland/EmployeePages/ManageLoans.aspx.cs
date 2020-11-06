using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bank2020Wantland.Pages
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BankData"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("insert into dsLoanList(principal, interest, month, fkcustomerid) values('" + principalTxt.Text + "', '" + interestTxt.Text + "', '" + monthsTxt.Text + "', '" + customerNameDrop.Text + "')", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            successMessageLbl.Text = "Loan was sucessfully added";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter adapters = new SqlDataAdapter("Select * from dsLoanList where fkcustomerid='" + DropDownList1.SelectedValue + "'", connectionString);
            DataSet datas = new DataSet();
            adapters.Fill(datas);
            GridView1.DataSourceID = null;
            GridView1.DataSource = datas;
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string id = GridView1.SelectedRow.Cells[0].Text;
            string principal = GridView1.SelectedRow.Cells[1].Text;
            string interest = GridView1.SelectedRow.Cells[2].Text;
            string months = GridView1.SelectedRow.Cells[3].Text;
            string fkcustomerid = GridView1.SelectedRow.Cells[4].Text;


            double principalDouble = Convert.ToDouble(principal);
            double interestDouble = Convert.ToDouble(interest);
            int monthsInt = Convert.ToInt32(months);

            MortgageClass mortgageLoan = new MortgageClass();

            mortgageLoan.Principal = principalDouble;
            mortgageLoan.Interest = interestDouble;
            mortgageLoan.Months = monthsInt;

            DataTable dataTableLoan = mortgageLoan.scheduleTable();


            DataTable dt = new DataTable();

            GridViewAmortization.DataSource = dataTableLoan;
            GridViewAmortization.DataBind();
        }


        public class MortgageClass
        {
            // Backing Fields
            private double _principal, _interest;
            private int _months;

            public double Principal
            {
                get
                {
                    return _principal;
                }
                set
                {
                    _principal = value;
                }
            }

            public double Interest
            {
                get
                {
                    return _interest;
                }
                set
                {
                    _interest = value;
                }
            }

            public int Months
            {
                get
                {
                    return _months;
                }
                set
                {
                    _months = value;
                }
            }

            public double Rate
            {
                get
                {
                    const double rateDivision = 1200;

                    double rate = Interest / rateDivision;

                    return rate;
                }
            }

            public double MonthlyMortgagePayment
            {
                get
                {
                    double middleCalc = Rate / ((Math.Pow((1 + Rate), Months) - 1));

                    double monthlyPayment = (Rate + middleCalc) * Principal;

                    return monthlyPayment;
                }
            }
           
            public DataTable scheduleTable()
            {
                var dataGridView = new DataTable();
                // Arrays
                double[] beginningBalanceArray = new double[Months];
                double[] interestPaidArray = new double[Months];
                double[] principalPaidArray = new double[Months];
                double[] endingBalanceArray = new double[Months];

                for (int i = 0; i < beginningBalanceArray.Length; i++)
                {
                    double startBalance = 0;
                    if (i >= 1)
                    {
                        startBalance = endingBalanceArray[i - 1];
                    }
                    else
                    {
                        startBalance = Principal;
                    }

                    beginningBalanceArray[i] += startBalance;
                    double newRate = beginningBalanceArray[i] * Rate;
                    interestPaidArray[i] += newRate;
                    double principalPaid = MonthlyMortgagePayment - interestPaidArray[i];
                    principalPaidArray[i] += principalPaid;
                    double endingBalance = beginningBalanceArray[i] - principalPaidArray[i];
                    endingBalanceArray[i] += endingBalance;
                }


                dataGridView.Columns.Add("Starting Balance");
                dataGridView.Columns.Add("Interest");
                dataGridView.Columns.Add("Principal");
                dataGridView.Columns.Add("Ending Balance");
                dataGridView.Columns.Add("Total Interest");

                // Rows Display Function
                for (int i = 0; i < beginningBalanceArray.Length; i++)
                {
                    dataGridView.Rows.Add(new object[] { beginningBalanceArray[i].ToString("C"), interestPaidArray[i].ToString("C"), principalPaidArray[i].ToString("C"), endingBalanceArray[i].ToString("C") });
                }
                return dataGridView; //returns the table.
            }
        }
    }
    }