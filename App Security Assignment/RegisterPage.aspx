<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="App_Security_Assignment.RegisterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
    
    <table class="nav-justified">
    <tr>
        <td style="width: 201px; height: 27px;">
            &nbsp;</td>
        <td style="width: 483px; height: 27px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px; height: 27px;">
            &nbsp;</td>
        <td style="width: 483px; height: 27px;">
            Register Page</td>
    </tr>
    <tr>
        <td style="width: 201px; height: 27px;">
            <asp:Label ID="lb_fname" runat="server" Text="First Name: "></asp:Label>
        </td>
        <td style="width: 483px; height: 27px;">
            <asp:TextBox ID="tb_fname" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 201px">
            <asp:Label ID="lb_lname" runat="server" Text="Last Name:"></asp:Label>
        </td>
        <td style="width: 483px">
            <asp:TextBox ID="tb_lname" runat="server"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px">
            <asp:Label ID="lb_credit_card" runat="server" Text="Credit Card:"></asp:Label>
        </td>
        <td style="width: 483px">
            <asp:TextBox ID="tb_credit_no" runat="server"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px; height: 22px;">
            <asp:Label ID="lb_email" runat="server" Text="Email:"></asp:Label>
        </td>
        <td style="width: 483px; height: 22px;">
            <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
        </td>
        <td style="height: 22px"></td>
    </tr>
    <tr>
        <td style="width: 201px">
            <asp:Label ID="lb_password" runat="server" Text="Password:"></asp:Label>
        </td>
        <td style="width: 483px">
            <asp:TextBox ID="tb_password" runat="server" onkeyup="javascript:validatePass()" TextMode="Password" ></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px">
            <asp:Label ID="lb_dob" runat="server" Text="Date of Birth:"></asp:Label>
        </td>
        <td style="width: 483px">
            <asp:TextBox ID="tb_dob" runat="server"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px">
            &nbsp;</td>
        <td style="width: 483px">
            &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px">
            &nbsp;</td>
        <td style="width: 483px">
            <asp:Label ID="lb_password_error" runat="server"></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 201px">
            &nbsp;</td>
        <td style="width: 483px">
            <asp:Button ID="btn_submit" runat="server" OnClick="btn_click_submit" Text="Submit" />
        </td>
        <td>&nbsp;</td>
    </tr>
</table>
   <script type="text/javascript">
       function validatePass() {
           var str = document.getElementById('<%=tb_password.ClientID %>').value;
           if (str.length < 8) {
               document.getElementById("MainContent_lb_password_error").innerHTML = "The password is less than 8 characters.";
               document.getElementById("MainContent_lb_password_error").style.color = "Red";
               return ("too short");
           }
           else if (str.search(/[0-9]/) == -1) {
               document.getElementById("MainContent_lb_password_error").innerHTML = "Password require at least 1 number";
               document.getElementById("MainContent_lb_password_error").style.color = "Red";
               return ("no_number");
           }

           else if (str.search(/[^0-9a-zA-Z]/) == -1) {
               document.getElementById("MainContent_lb_password_error").innerHTML = "Password require at least 1 special character";
               document.getElementById("MainContent_lb_password_error").style.color = "Red";
               return ("no_special");
           }
           else if (str.search(/[A-Z]/) == -1) {
               document.getElementById("MainContent_lb_password_error").innerHTML = "Password require at least 1 uppercase character";
               document.getElementById("MainContent_lb_password_error").style.color = "Red";
               return ("no_uppercase");
           }
           else if (str.search(/[a-z]/) == -1) {
               document.getElementById("MainContent_lb_password_error").innerHTML = "Password require at least 1 lowercase character";
               document.getElementById("MainContent_lb_password_error").style.color = "Red";
               return ("no_lowercase");
           }
           document.getElementById("MainContent_lb_password_error").innerHTML = "";
           document.getElementById("MainContent_lb_password_error").style.color = "Blue";
       }
   </script>
</asp:Content>
