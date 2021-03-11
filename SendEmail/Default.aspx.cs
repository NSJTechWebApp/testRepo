using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Globalization;
using System.Web.Services;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SendEmail
{
    public partial class _Default : Page
    {
        public void Alert(String strMess)
        {
            String strScript = "alert('" + strMess + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MyScript", strScript, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
            //string pass = "8ff35aa328ac6b0d15554b80516ae69c";
            //string pass = "test";
            //string pass = "098f6bcd4621d373cade4e832627b4f6";
            if (!IsPostBack)
            {
                DropDownList1.DataSource = CountryList();
                DropDownList1.DataBind();
                GetSubnetMask();
                lblIPAddress.Text = GetIP();
            }
        }

        public string GetIP()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();

        }

        public static void GetSubnetMask()
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine(unicastIPAddressInformation.IPv4Mask);

                        string test = unicastIPAddressInformation.IPv4Mask.ToString();
                    }
                }
            }
        }

        public static string MD5Encryption(string encryptionText) // tested
        {
            // We have created an instance of the MD5CryptoServiceProvider class.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //We converted the data as a parameter to a byte array.
            byte[] array = Encoding.UTF8.GetBytes(encryptionText);
            //We have calculated the hash of the array.
            array = md5.ComputeHash(array);
            //We created a StringBuilder object to store hashed data.
            StringBuilder sb = new StringBuilder();
            //We have converted each byte from string into string type.

            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //We returned the hexadecimal string.
            return sb.ToString();
        }        

        public static List<string> CountryList()
        {
            List<string> CultureList = new List<string>();

            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);

                if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }
            CultureList.Sort();
            return CultureList;
        }

        public static List<string> StateList()
        {
            List<string> CultureList = new List<string>();

            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);

                if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }
            CultureList.Sort();
            return CultureList;
        }

        public string CreateMD5Hash(string RawData) //tested
        {
            byte[] hash = System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(RawData));
            string str = "";
            byte[] numArray = hash;
            int index = 0;
            while (index < numArray.Length)
            {
                byte num = numArray[index];
                str = str + num.ToString("x2");
                checked { ++index; }
            }
            return str;
        }

        [WebMethod]
        public void AlertMe()
        {
           InsertLogOutTime();
           Alert("Success");
        }

        protected string GetIPAddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            return request.UserHostAddress;
        }

        public static void InsertLogOutTime()
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("John.Discalsote@nsjbi.com.ph", "Workforce 24/7");
                msg.To.Add("njdiscalsote25@gmail.com");
                msg.Subject = "test";
                msg.Body = "logout";
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("John.Discalsote@nsjbi.com.ph", "J0hn0325$");
                    //smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                }
            }    
            catch(Exception ex)
            {
                throw ex;
            }       
        }

        //Data Encryption
        public string Encrypt(string stringToEncrypt)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        //Data Decryption
        public string Decrypt(string EncryptedText)
        {
            EncryptedText = EncryptedText.Replace(" ", "+");
            byte[] inputByteArray = new byte[EncryptedText.Length + 1];
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };
            int mod = EncryptedText.Length % 4;
            if (mod > 0)
            {
                EncryptedText += new string('=', 4 - mod);
            }

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(EncryptedText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("John.Discalsote@nsjbi.com.ph", "Workforce 24/7");
                msg.To.Add(txtEmail.Text);
                msg.Subject = txtSubject.Text;
                msg.Body = txtBody.Text;
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("John.Discalsote@nsjbi.com.ph", MD5Encryption("8ff35aa328ac6b0d15554b80516ae69c"));
                    //smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            lblResult.Text = HttpUtility.UrlEncode(Encrypt(txtEncrypt.Text).ToString());
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            lblResult1.Text = Decrypt(HttpUtility.UrlDecode(txtDecrypt.Text).ToString());
        }

        protected void btnAlert_Click(object sender, EventArgs e)
        {
            AlertMe();
        }
    }
}