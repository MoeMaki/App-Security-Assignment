using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Security_Assignment
{
    public partial class AccountRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_reset_click(object sender, EventArgs e)
        {
            var status = "closed";

            if (tb_email.Text == "")
            {
                lb_msg.Text = "Missing Inputs";
                lb_msg.ForeColor = Color.Red;
            }
            else
            {
                var email = HttpUtility.HtmlEncode(tb_email.Text);
                string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;



                try
                {
                    status = "open";
                    SqlConnection con = new SqlConnection(MYDBConnectionString);
                    con.Open();
                    var str = "Update [user] set status = @status, LockTime = @LockTime where email = @email";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@LockTime", DateTime.Now.AddMinutes(30));
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lb_msg.Text = "Account Resetted";
                    lb_msg.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
}