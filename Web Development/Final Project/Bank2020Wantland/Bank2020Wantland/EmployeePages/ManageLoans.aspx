<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLoans.aspx.cs" MasterPageFile="~/MasterPages/mpSecure.Master" Inherits="Bank2020Wantland.Pages.WebForm5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <style>
        input[type=text], select, textarea {
            width: 50%;
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
            font-family: Arial;
            color: darkslategray;
        }

        p {
            text-align: left;
            font-family: Arial;
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

        label {
            font-family: Arial;
            color: darkslategray;
        }

        text {
            font-family: Arial;
            color: darkslategray;
        }
    </style>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BankData %>" SelectCommand="SELECT [id], [firstname] FROM [dsCustomerList]"></asp:SqlDataSource>

    <asp:DropDownList Style="width: 23%;" ID="customerNameDrop" runat="server" DataSourceID="SqlDataSource1" DataTextField="firstname" DataValueField="id"></asp:DropDownList>

    <table class="container">
        <tr>
            <td>
                <asp:Label ID="principalLbl" runat="server" Text="Principal"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="principalTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="principalValidator" runat="server" ErrorMessage="Enter the principal!" ControlToValidate="principalTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="interestLbl" runat="server" Text="Interest Rate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="interestTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="interestValidator" runat="server" ErrorMessage="Enter the interest rate!" ControlToValidate="interestTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="monthsLbl" runat="server" Text="Number of Months"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="monthsTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="monthsValidator" runat="server" ErrorMessage="Enter the number of months!" ControlToValidate="monthsTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="addBtn" runat="server" Text="Add" OnClick="addBtn_Click" />
            </td>
            <td>
                <asp:Button PostBackUrl="~/EmployeePages/MainPage.aspx" ID="cancelBtn" runat="server" Text="Cancel" CausesValidation="False" />
            </td>
            <td>
                <asp:Label ID="successMessageLbl" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>

    <br />
    <hr />
    <br />

    <asp:DropDownList Style="width: 33%;" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="firstname" DataValueField="id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BankData %>" SelectCommand="SELECT [ID],[FirstName] FROM [dsCustomerList]"></asp:SqlDataSource>

    <asp:GridView Style="font-family: Arial; color: darkslategray;" ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource3" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
        <Columns>
            <asp:BoundField DataField="principal" HeaderText="Principal" SortExpression="name" />
            <asp:BoundField DataField="interest" HeaderText="Interest" SortExpression="name" />
            <asp:BoundField DataField="month" HeaderText="Month" SortExpression="name" />
            <asp:BoundField DataField="fkcustomerid" HeaderText="FK Customer ID" SortExpression="id" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>

    <asp:GridView Style="font-family: Arial; color: darkslategray;" ID="GridViewAmortization" runat="server" AllowSorting="True" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Starting Balance" HeaderText="Starting Balance" SortExpression="name" />
            <asp:BoundField DataField="Interest" HeaderText="Interest" SortExpression="name" />
            <asp:BoundField DataField="Principal" HeaderText="Principal" SortExpression="name" />
            <asp:BoundField DataField="Ending Balance" HeaderText="Ending Balance" SortExpression="name" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:BankData %>" DeleteCommand="DELETE FROM [dsLoanList] WHERE [id] = @id" InsertCommand="INSERT INTO [dsLoanList] ([fkcustomerid], [principal], [interest],[month]) VALUES (@fkcustomerid, @principal,@interest, @month)" SelectCommand="SELECT * FROM [dsLoanList]" UpdateCommand="UPDATE [dsLoanList] SET [fkcustomerid] = @fkcustomerid, [principal] = @principal,[interest] = @interest, [month] = @month WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="fkcustomerid" Type="String" />
            <asp:Parameter Name="principal" Type="String" />
            <asp:Parameter Name="interest" Type="String" />
            <asp:Parameter Name="month" Type="String" />

        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="fkcustomerid" Type="String" />
            <asp:Parameter Name="principal" Type="String" />
            <asp:Parameter Name="interest" Type="String" />
            <asp:Parameter Name="month" Type="String" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
