using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Security_Assignment
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_logout.Visible = false;
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value)){
                    lb_logged_in.Text = "Logged In Successfully";
                    lb_logged_in.ForeColor = System.Drawing.Color.Green;
                    btn_logout.Visible = true;
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }

            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

        }

        protected void btn_logout_click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("LoginPage.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
    }
}