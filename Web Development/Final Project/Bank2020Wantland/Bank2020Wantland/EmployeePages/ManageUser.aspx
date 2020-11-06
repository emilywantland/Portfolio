<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/mpSecure.Master" CodeBehind="ManageUser.aspx.cs" Inherits="Bank2020Wantland.Pages.WebForm6" %>

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
                <asp:Label ID="firstNameLbl" runat="server" Text="First Name"></asp:Label>
            </td>
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
                <asp:RequiredFieldValidator ID="lastnameValidator" runat="server" ErrorMessage="Enter a last name!" ControlToValidate="lastNameTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="usernameLbl" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="usernameTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ErrorMessage="Enter your last name!" ControlToValidate="usernameTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="passwordLbl" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="passwordTxt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ErrorMessage="Enter your last name!" ControlToValidate="passwordTxt" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="addBtn" runat="server" Text="Add" OnClick="addBtn_Click" />
            </td>
            <td>
                <asp:Button PostBackURL="~/EmployeePages/MainPage.aspx" ID="cancelBtn" runat="server" Text="Cancel" CausesValidation="False" />
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

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BankData %>" DeleteCommand="DELETE FROM [dsUserInformation] WHERE [ID] = @ID" InsertCommand="INSERT INTO [dsUserInformation] ([firstname],[lastname],[username],[password]) VALUES (@firstName,@lastName,@userName,@passWord)" SelectCommand="SELECT * FROM [dsUserInformation]" UpdateCommand="UPDATE [dsUserInformation] SET [firstname] = @firstName, [lastname] = @lastName, [username] = @userName, [password] = passWord WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="firstName" />
            <asp:Parameter Name="lastName" />
            <asp:Parameter Name="userName" />
            <asp:Parameter Name="passWord" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="firstName" />
            <asp:Parameter Name="lastName" />
            <asp:Parameter Name="userName" />
            <asp:Parameter Name="passWord" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:GridView Style="font-family: Arial; color: darkslategray;" ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="id">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
            <asp:BoundField DataField="password" HeaderText="Password" SortExpression="password" />
            <asp:BoundField DataField="firstname" HeaderText="First Name" SortExpression="firstname" />
            <asp:BoundField DataField="lastname" HeaderText="Last Name" SortExpression="lastname" />
        </Columns>
    </asp:GridView>

</asp:Content>
