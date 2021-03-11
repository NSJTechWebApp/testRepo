using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SendEmail
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void InsertLogOutTime() {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}