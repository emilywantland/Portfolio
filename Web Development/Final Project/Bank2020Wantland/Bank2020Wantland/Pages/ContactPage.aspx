<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/mpOutside.Master" CodeBehind="ContactPage.aspx.cs" Inherits="Bank2020Wantland.Pages.WebForm2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <style>
        input[type=text], select, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
            resize: vertical
        }

        input[type=submit] {
            background-color: lightblue;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

            input[type=submit]:hover {
                background-color: steelblue;
            }

        .container {
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px;
            font-family: Arial;
            color: darkslategray;
        }

        p{
            text-align: left;
            font-family: Arial;
        }

    </style>

    <%--Contact Us Form--%>
    <div class="container">
        <form>
            <p>First Name</p>
            <input type="text" id="fname" name="firstname" placeholder="First Name...">

            <p>Last Name</p>
            <input type="text" id="lname" name="lastname" placeholder="Last Name....">

            <p>State</p>
            <select id="state" name="state">
                <option value="kentucky">KY</option>
                <option value="indiana">IN</option>
                <option value="ohio">OH</option>
            </select>

            <p>Subject</p>
            <textarea id="subject" name="subject" placeholder="Message..." style="height: 200px"></textarea>

            <input type="submit" value="Submit">
        </form>
    </div>
    </asp:Content>
