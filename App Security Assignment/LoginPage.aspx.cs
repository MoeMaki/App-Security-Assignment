using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Security_Assignment
{
    public class MyObject
    {
        public string success { get; set; }
        public List<string> ErrorMessage { get; set; }

    }
    public partial class LoginPage : System.Web.UI.Page
    {
        static int counter = 0;
        static string salt;
        static string finalHash;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_click_submit(object sender, EventArgs e)
        {
            var email_check = "";
            var password_check = "";
            var status_check = "";
            var status = "closed";
            var minTime = DateTime.Now;
            var maxTime = DateTime.Now;
            var LockTime = DateTime.Now;
            
            var pass = HttpUtility.HtmlEncode(tb_password.Text);
            var email = HttpUtility.HtmlEncode(tb_email.Text);
            
            string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
            if (ValidateCaptcha())
            {
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
                        status_check = myReader.GetValue(9).ToString();
                        password_check = myReader.GetValue(5).ToString();
                        email_check = myReader.GetValue(4).ToString();
                        salt = myReader.GetValue(7).ToString();
                        minTime = Convert.ToDateTime(myReader.GetValue(10).ToString());
                        maxTime = Convert.ToDateTime(myReader.GetValue(11).ToString());
                        LockTime = Convert.ToDateTime(myReader.GetValue(12).ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

                if (LockTime < DateTime.Now)
                {
                    status = "open";
                    SqlConnection con = new SqlConnection(MYDBConnectionString);
                    con.Open();
                    var str = "Update [user] set status = @status, LockTime = @LockTime where email = @email";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@LockTime", DateTime.Now);
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
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
                        status_check = myReader.GetValue(9).ToString();
                        password_check = myReader.GetValue(5).ToString();
                        email_check = myReader.GetValue(4).ToString();
                        salt = myReader.GetValue(7).ToString();
                        minTime = Convert.ToDateTime(myReader.GetValue(10).ToString());
                        maxTime = Convert.ToDateTime(myReader.GetValue(11).ToString());
                        LockTime = Convert.ToDateTime(myReader.GetValue(12).ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                if (status_check == "open" && counter < 3)
                {
                    string pwd = pass.Trim(); ;
                   
                    //Fills array of bytes with a cryptographically strong sequence of random values.
                   
                    
                    SHA512Managed hashing = new SHA512Managed();
                    string pwdWithSalt = pwd + salt;
                    byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    finalHash = Convert.ToBase64String(hashWithSalt);
                    
                    if (DateTime.Now > maxTime)
                    {
                        lb_result.Text = "Password has expired";
                        lb_result.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (email_check == email && password_check == finalHash)
                        {
                            Session["LoggedIn"] = email;
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            Response.Redirect("ProfilePage.aspx");
                        }
                        else
                        {
                            counter = counter + 1;
                            lb_result.Text = "Wrong or Invaild Email and/or Password";
                            lb_result.ForeColor = Color.Red;
                        }
                    }
                    
                }
                
                if (counter >= 3)
                {
                    try
                    {
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
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                }
                if (status_check == "closed")
                {
                    lb_result.Text = "Account has been locked out. Please contact adminstator";
                }


            }
        }

        public bool ValidateCaptcha()
        {
            bool result = true;

            // When user submits the recaptcha form, the user gets a response POST parameter.
            // captchaResponse consist of the user click pattern. Behaviour analytics! AI :)
            string captchaResponse = Request.Form["g-recaptcha-response"];

            // To send a GET request to Google along with the response and Secret Key.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=6LfWGEQaAAAAAGkbdL5TdiVUG_9fnfc83v1GjM3q &response=" + captchaResponse);

            try
            {
                // Codes to receive the Response in JSON format from Google Server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        // The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        // To show the JSON response string for learning purpose
                        //lb_result.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        // Create jsonObject to handle the response e.g success or ERROR
                        // Deserialize Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        // Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);
                    }

                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected void btn_register_click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }

        protected void btn_change_click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePasswordPage.aspx");
        }

        protected void btn_account_click(object sender, EventArgs e)
        {
            Response.Redirect("AccountRecovery.aspx");
        }

    }
}