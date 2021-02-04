<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountRecovery.aspx.cs" Inherits="App_Security_Assignment.AccountRecovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
    <tr>
        <td style="width: 246px; height: 21px"></td>
        <td style="width: 430px; height: 21px"></td>
        <td style="height: 21px"></td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">Account Recovery</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 246px; height: 20px">
            <asp:Label ID="lb_email" runat="server" Text="Email:"></asp:Label>
        </td>
        <td style="width: 430px; height: 20px">
            <asp:TextBox ID="tb_email" runat="server" Width="256px"></asp:TextBox>
        </td>
        <td style="height: 20px"></td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">
            <asp:Label ID="lb_msg" runat="server"></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">
            <asp:Button ID="btn_reset" runat="server" OnClick="btn_reset_click" Text="Reset" Width="61px" />
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 246px">&nbsp;</td>
        <td style="width: 430px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>
</asp:Content>
