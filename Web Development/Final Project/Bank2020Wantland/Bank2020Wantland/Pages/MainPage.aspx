<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" MasterPageFile="~/MasterPages/mpOutside.Master" Inherits="Bank2020Wantland.Pages.WebForm1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <style>
        .container {
            position: relative;
            text-align: center;
            color: darkslategray;
        }

        .top-right {
            position: absolute;
            top: 8px;
            right: 16px;
        }

        th, td {
            padding: 15px;
        }

        hr {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: dashed;
            border-width: 2px;
            border-color: darkslategray
        }
    </style>

    <%--Header Image--%>
    <header>
        <div class="container">
            <img src="https://www.bankonheritage.com/wp-content/uploads/2017/12/savings-header.jpg" style="width: 100%;">
            <div class="top-right">

                <h1 style="font-family: Arial; color: darkslategray;"><b>Easy Banking for Everyone</b></h1>
                <p style="font-family: Arial; color: darkslategray;"><b>Simple, clutter-free, and intuitive banking</b></p>

            </div>
        </div>

        &nbsp;
        <hr />
        &nbsp;

        <%--Home Bullet Point--%>
        <div>
            <img src="../img/Home.png" style="width: 7%;" align="left">
            <ul>
                <h2 style="font-family: Arial; color: darkslategray;">Home Mortgage</h2>
                <p style="font-family: Arial; color: darkslategray;">
                    Navigating buying a home can feel tricky, we're here to help you make informed decisions regarding your mortgage. Prequalify or apply today!
                </p>
            </ul>
        </div>

        &nbsp;

        <%--Scheduling Bulley Point--%>
        <div>
            <img src="../img/Calc.png" style="width: 7%;" align="left">
            <ul>
                <h2 style="font-family: Arial; color: darkslategray;">Flexible Payment Schedules</h2>
                <p style="font-family: Arial; color: darkslategray;">Work out a plan with our trusted loan advisors.</p>
            </ul>
        </div>

        &nbsp;
        <hr />
        &nbsp;

        <%--Motgage Rates--%>
        <h2 style="font-family: Arial; color: darkslategray; text-align: center;">Today's Mortgage Rates</h2>
        <table style="font-family: Arial; color: darkslategray; margin-left: auto; margin-right: auto;">

            <tr>
                <th><u>30-Year Fixed</u></th>
                <th><u>20-Year Fixed</u></th>
                <th><u>10-Year ARM</u></th>
                <th><u>5-Year ARM</u></th>
            </tr>
            <tr>
                <td>3.630%</td>
                <td>3.500%</td>
                <td>4.630%</td>
                <td>4.880%</td>
            </tr>
            <tr>
                <td>3.700% APR</td>
                <td>3.600% APR</td>
                <td>4.120% APR</td>
                <td>3.770% APR</td>
            </tr>
        </table>
        <p style="font-family: Arial; color: darkslategray; text-align: center;">*Rates are current as of 04/14/2020</p>
    </header>

</asp:Content>
