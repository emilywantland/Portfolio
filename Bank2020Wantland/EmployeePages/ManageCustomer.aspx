<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCustomer.aspx.cs" MasterPageFile="~/MasterPages/mpSecure.Master" Inherits="Bank2020Wantland.Pages.WebForm4" %>

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
            resize: vertical;
            float: left;
            display: inline-block;
            flex-direction: row;
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
            padding: 20px;
            font-family: Arial;
            color: darkslategray;
        }

        p {
            text-align: left;
            font-family: Arial;
        }
    </style>


    <table class="container">
        <tr>
            <td>
                <asp:Label ID="firstNameLbl" runat="server" Text="First Name"></asp:Label></td>
            <td>
                <asp:TextBox ID="firstNameTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="firstNameValidator" runat="server" ErrorMessage="Enter a first name!" ControlToValidate="firstNameTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lastNameLbl" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="lastNameTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="lastNameValidator" runat="server" ErrorMessage="Enter a last name!" ControlToValidate="lastNameTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>

            <td>
                <asp:Label ID="addressLbl" runat="server" Text="Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="addressTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="addressValidator" runat="server" ErrorMessage="Enter an address!" ControlToValidate="addressTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ageLbl" runat="server" Text="Age"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ageTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="ageValidator" runat="server" ErrorMessage="Enter an age!" ControlToValidate="ageTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>
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


    &nbsp;
    <br />
    &nbsp;
    &nbsp;
    &nbsp;
    &nbsp;
    &nbsp;

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BankData %>" DeleteCommand="DELETE FROM [dsCustomerList] WHERE [ID] = @ID" InsertCommand="INSERT INTO [dsCustomerList] ([firstname],[lastname],[address],[age]) VALUES (@firstName,@lastName,@address,@age)" SelectCommand="SELECT * FROM [dsCustomerList]" UpdateCommand="UPDATE [dsCustomerList] SET [firstname] = @firstName, [lastname] = @lastName, [address] = @address, [age] = @age WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="firstName" />
            <asp:Parameter Name="lastName" />
            <asp:Parameter Name="address" />
            <asp:Parameter Name="age" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="firstName" />
            <asp:Parameter Name="lastName" />
            <asp:Parameter Name="address" />
            <asp:Parameter Name="age" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:GridView Style="font-family: Arial; color: darkslategray;" ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="firstname" HeaderText="First Name" SortExpression="firstname" />
            <asp:BoundField DataField="lastname" HeaderText="Last Name" SortExpression="lastname" />
            <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
            <asp:BoundField DataField="age" HeaderText="Age" SortExpression="age" />
        </Columns>
    </asp:GridView>

</asp:Content>
