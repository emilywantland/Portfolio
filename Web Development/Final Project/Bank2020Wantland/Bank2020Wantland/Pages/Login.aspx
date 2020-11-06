<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/mpOutside.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Bank2020Wantland.Pages.WebForm3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">

        <br />
        <br />
        <br />

        <table style="margin-left:auto; margin-right: auto; font-family: Arial; color: darkslategray;">
            
            <tr>
                <%--Username--%>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Username" Style="font-family: Arial; color: darkslategray;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Style="font-family: Arial; color: darkslategray;">emilywantland</asp:TextBox>
                    <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ErrorMessage="Enter a username!" ControlToValidate="txtUserName" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <%--Password--%>
                <td style="height: 26px">
                    <asp:Label ID="Label2" runat="server" Text="Password" Style="font-family: Arial; color: darkslategray;"></asp:Label>
                </td>
                <td style="height: 26px">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Style="font-family: Arial; color: darkslategray;"></asp:TextBox>
                    <font face="Arial">(password)</font>
                    <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ErrorMessage="Enter a password!" ControlToValidate="txtPassword" ForeColor="#000066" Style="font-family: Arial; color: darkslategray;"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <%--Login Button--%>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Style="font-family: Arial; color: darkslategray;" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <%--Incorrect User or Pass--%>
                    <asp:Label ID="incorrectUserOrPass" runat="server" Text="" Style="font-family: Arial; color: darkslategray;"></asp:Label>
                </td>
            </tr>
           
        </table>

        <br />
        <br />
        <br />
        <br />
        <br />

    </asp:Panel>
</asp:Content>
