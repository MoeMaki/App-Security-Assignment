<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="App_Security_Assignment.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    &nbsp;&nbsp;&nbsp;    <table class="nav-justified">
        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
        <script src="https://www.google.com/recaptcha/api.js?render=6LfWGEQaAAAAAIDg9vojoZeYhFMeHJ4FprjvdP_2"></script>
        <script>
            grecaptcha.ready(function () {
                grecaptcha.execute('6LfWGEQaAAAAAIDg9vojoZeYhFMeHJ4FprjvdP_2', { action: 'Login' }).then(function (token) {
                    document.getElementById("g-recaptcha-response").value = token;
                });
            });
        </script>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">Login Page</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px; height: 20px"></td>
            <td style="width: 422px; height: 20px">
                <asp:Label ID="lb_email" runat="server" Text="Email"></asp:Label>
            </td>
            <td style="height: 20px"></td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">
                <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">
                <asp:Label ID="lb_password" runat="server" Text="Password" ></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">
                <asp:TextBox ID="tb_password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">
                <asp:Button ID="btn_submit" runat="server" OnClick="btn_click_submit" Text="Submit" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_register" runat="server" OnClick="btn_register_click" Text="Register" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px; margin-left: 40px;">
                <asp:Button ID="btn_change_pass" runat="server" OnClick="btn_change_click" Text="Change Password" Width="122px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_account" runat="server" OnClick="btn_account_click" Text="Account Recovery" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px; margin-left: 40px;">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px; height: 21px;"></td>
            <td style="width: 422px; height: 21px;">
                <asp:Label ID="lb_result" runat="server"></asp:Label>
            </td>
            <td style="height: 21px"></td>
        </tr>
        <tr>
            <td style="width: 203px">&nbsp;</td>
            <td style="width: 422px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
