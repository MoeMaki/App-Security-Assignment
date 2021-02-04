using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace App_Security_Assignment
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        byte[] saltByte;

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btn_click_submit(object sender, EventArgs e)
        {
            CultureInfo culture;
            DateTimeStyles styles;
            DateTime dateResult;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            styles = DateTimeStyles.None;
            //implement codes for the button event
            //Extract data from textbox
            int scores = checkPassword(HttpUtility.HtmlEncode(tb_password.Text));
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
            lb_password_error.Text = "Status " + status;
            if (scores < 4)
            {
                lb_password_error.ForeColor = Color.Red;
                return;
            }
            lb_password_error.ForeColor = Color.Green;

            string pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim()); ;
            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte;
            saltByte = new byte[8];
            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);
            SHA512Managed hashing = new SHA512Managed();
            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
            finalHash = Convert.ToBase64String(hashWithSalt);
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
            if (tb_fname.Text == "" || tb_lname.Text == "" || tb_credit_no.Text == "" || tb_dob.Text == "" || tb_password.Text == "" || tb_email.Text == "")
            {
                lb_password_error.Text = "Missing Inputs";
                lb_password_error.ForeColor = Color.Red;
            }
            else if (IsValidEmail(HttpUtility.HtmlEncode(tb_email.Text.Trim()))==false)
            {
                lb_password_error.Text = "Invaild Email Address";
                lb_password_error.ForeColor = Color.Red;
            }
            else if (DateTime.TryParse(tb_dob.Text, culture, styles, out dateResult) == false)
            {
                lb_password_error.Text = "Invaild Date of Birth";
                lb_password_error.ForeColor = Color.Red;
            }
            else if (tb_credit_no.Text.Length != 16)
            {
                lb_password_error.Text = "Invaild Credit Card";
                lb_password_error.ForeColor = Color.Red;
            }
            else
            {
                createAccount();
            }

        }

        public void createAccount()
        {
            var email_check = "";
            var flag = "no";
            try
            {
                SqlConnection con = new SqlConnection(MYDBConnectionString);
                con.Open();
                var str = "Select email From [user]";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    // Assuming your desired value is the name as the 3rd field
                    email_check = myReader.GetValue(0).ToString();
                    if (email_check == HttpUtility.HtmlEncode(tb_email.Text.Trim()))
                    {
                        flag = "yes";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            if (flag == "no")
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO [user] VALUES(@fname,@lname, @credit," +
                            " @email,@password,@password_old,@salt_key,@dob,@status,@minTime,@maxTime,@LockTime)"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@fname", HttpUtility.HtmlEncode(tb_fname.Text.Trim()));
                                cmd.Parameters.AddWithValue("@lname", HttpUtility.HtmlEncode(tb_lname.Text.Trim()));
                                cmd.Parameters.AddWithValue("@credit", encryptData(HttpUtility.HtmlEncode(tb_credit_no.Text.Trim())));
                                cmd.Parameters.AddWithValue("@email", HttpUtility.HtmlEncode(tb_email.Text.Trim()));
                                cmd.Parameters.AddWithValue("@password", finalHash);
                                cmd.Parameters.AddWithValue("@password_old", "");
                                cmd.Parameters.AddWithValue("@salt_key", salt);
                                cmd.Parameters.AddWithValue("@dob", HttpUtility.HtmlEncode(tb_dob.Text.Trim()));
                                cmd.Parameters.AddWithValue("@status", "open");
                                cmd.Parameters.AddWithValue("@minTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@maxTime", DateTime.Now.AddMinutes(15));
                                cmd.Parameters.AddWithValue("@LockTime", DateTime.Now);


                                //cmd.Parameters.AddWithValue("@Nric", encryptData(tb_nric.Text.Trim()));
                                //cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                                //cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                //cmd.Parameters.AddWithValue("@DateTimeRegistered", DateTime.Now);
                                //cmd.Parameters.AddWithValue("@MobileVerified", DBNull.Value);
                                //cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                lb_password_error.Text = "The email already exist";
                lb_password_error.ForeColor = Color.Red;
            }
            
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);


                //Encrypt
                //cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
                //cipherString = Convert.ToBase64String(cipherText);
                //Console.WriteLine("Encrypted Text: " + cipherString);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { }
            return cipherText;
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
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var id = new IdnMapping();
                    string dName = id.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + dName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }
}