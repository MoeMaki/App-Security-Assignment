<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="App_Security_Assignment.ProfilePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td style="width: 200px; height: 20px;"></td>
            <td style="height: 20px"></td>
        </tr>
        <tr>
            <td style="width: 200px">
                <asp:Label ID="lb_logged_in" runat="server" Text="Logged in"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 200px; height: 20px;">
                <asp:Button ID="btn_logout" runat="server" Text="Log Out" OnClick="btn_logout_click" />
            </td>
            <td style="height: 20px"></td>
        </tr>
    </table>
</asp:Content>
