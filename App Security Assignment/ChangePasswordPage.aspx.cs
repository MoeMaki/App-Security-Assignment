using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Security_Assignment
{
    public partial class ChangePasswordPage : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submit_click(object sender, EventArgs e)
        {
            int scores = checkPassword(HttpUtility.HtmlEncode(tb_pass.Text));
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Very Strong";
                    break;
                default:
                    break;
            }
            lb_msg.Text = "Status " + status;
            if (scores < 4)
            {
                lb_msg.ForeColor = Color.Red;
                return;
            }
            lb_msg.ForeColor = Color.Green;
            if (tb_pass.Text == "" || tb_email.Text == "")
            {
                lb_msg.Text = "Missing Inputs";
                lb_msg.ForeColor = Color.Red;
            }
            else
            {
                var email_check = "";
                var password_old1 = "";
                var password_old2 = "";
                var status_check = "";
                var minTime = DateTime.Now;
                var maxTime = DateTime.Now;


                var pass = HttpUtility.HtmlEncode(tb_pass.Text);
                var email = HttpUtility.HtmlEncode(tb_email.Text);
                string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;

                

                try
                {
                    SqlConnection con = new SqlConnection(MYDBConnectionString);
                    con.Open();
                    var str = "Select * From [user] where email = @email";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader myReader = cmd.ExecuteReader();

                    while (myReader.Read())
                    {
                        // Assuming your desired value is the name as the 3rd field
                        status_check = myReader.GetValue(8).ToString();
                        password_old1 = myReader.GetValue(5).ToString();
                        password_old2 = myReader.GetValue(6).ToString();
                        email_check = myReader.GetValue(4).ToString();
                        salt = myReader.GetValue(7).ToString();
                        minTime = Convert.ToDateTime(myReader.GetValue(10).ToString());
                        maxTime = Convert.ToDateTime(myReader.GetValue(11).ToString());
                    }
                    con.Close();
                    con.Open();

                    str = "Update [user] set password = @pass, password_old = @pass2, salt_key = @salt_key, minTime = @minTime, maxTime = @maxTime where email = @email";
                    cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@email", email);

                    string pwd = HttpUtility.HtmlEncode(pass.Trim());

                    SHA512Managed hashing = new SHA512Managed();
                    string pwdWithSalt = pwd + salt;
                    byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    finalHash = Convert.ToBase64String(hashWithSalt);

                    if (DateTime.Now < minTime.AddMinutes(5) )
                    {
                        lb_msg.Text = "Cannot change password so soon";
                        lb_msg.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (finalHash != password_old1 && finalHash != password_old2)
                        {
                            cmd.Parameters.AddWithValue("@pass", finalHash);
                            cmd.Parameters.AddWithValue("@pass2", password_old1);
                            cmd.Parameters.AddWithValue("@salt_key", salt);
                            cmd.Parameters.AddWithValue("@minTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@maxTime", DateTime.Now.AddMinutes(15));
                            cmd.CommandText = str;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            lb_msg.Text = "Cannot reuse old password";
                            lb_msg.ForeColor = Color.Red;
                        }
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            
        }

        private int checkPassword(string password)
        {
            int score = 0;

            //include your implentation here

            //Score 1 very weak!
            //if length of password is less than 8 chars

            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            //Score 2 weak!
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            //Score 3 medium!
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            //Score 4 strong!
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            //Score 5 very strong!
            if (Regex.IsMatch(password, "[^0-9a-zA-Z]"))
            {
                score++;
            }

            return score;
        }
    }
}