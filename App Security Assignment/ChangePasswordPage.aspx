<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePasswordPage.aspx.cs" Inherits="App_Security_Assignment.ChangePasswordPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td style="width: 228px">&nbsp;</td>
            <td style="width: 355px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 228px">&nbsp;</td>
            <td style="width: 355px">Reset Account</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 228px; height: 20px;"></td>
            <td style="width: 355px; height: 20px;"></td>
            <td style="height: 20px"></td>
        </tr>
        <tr>
            <td style="height: 20px; width: 228px">
                <asp:Label ID="lb_email" runat="server" Text="Email:"></asp:Label>
            </td>
            <td style="width: 355px; height: 20px">
                <asp:TextBox ID="tb_email" runat="server" Width="232px"></asp:TextBox>
            </td>
            <td style="height: 20px"></td>
        </tr>
        <tr>
            <td style="width: 228px; height: 22px;">
                <asp:Label ID="lb_pass" runat="server">Password:</asp:Label>
            </td>
            <td style="width: 355px; height: 22px;">
                <asp:TextBox ID="tb_pass" runat="server" onkeyup="javascript:validatePass()" TextMode="Password" Width="232px"></asp:TextBox>
            </td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="width: 228px">&nbsp;</td>
            <td style="width: 355px">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 228px">&nbsp;</td>
            <td style="width: 355px">
                <asp:Label ID="lb_msg" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 228px">&nbsp;</td>
            <td style="width: 355px">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 228px">&nbsp;</td>
            <td style="width: 355px">
                <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_click" Text="Submit" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <script type="text/javascript">
        function validatePass() {
            var str = document.getElementById('<%=tb_pass.ClientID %>').value;
            if (str.length < 8) {
                document.getElementById("MainContent_lb_msg").innerHTML = "The password is less than 8 characters.";
                document.getElementById("MainContent_lb_msg").style.color = "Red";
                return ("too short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("MainContent_lb_msg").innerHTML = "Password require at least 1 number";
                document.getElementById("MainContent_lb_msg").style.color = "Red";
                return ("no_number");
            }

            else if (str.search(/[^0-9a-zA-Z]/) == -1) {
                document.getElementById("MainContent_lb_msg").innerHTML = "Password require at least 1 special character";
                document.getElementById("MainContent_lb_msg").style.color = "Red";
                return ("no_special");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("MainContent_lb_msg").innerHTML = "Password require at least 1 uppercase character";
                document.getElementById("MainContent_lb_msg").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("MainContent_lb_msg").innerHTML = "Password require at least 1 lowercase character";
                document.getElementById("MainContent_lb_msg").style.color = "Red";
                return ("no_lowercase");
            }
            document.getElementById("MainContent_lb_msg").innerHTML = "";
            document.getElementById("MainContent_lb_msg").style.color = "Blue";
        }
    </script>
</asp:Content>
