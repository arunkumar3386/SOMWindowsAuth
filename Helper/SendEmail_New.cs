using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class SendEmail_New
    {
        public static bool SendHtmlFormattedEmail(string body, string subject, string toMailAddr)
        {
            try
            {
                bool result = false;
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["From"]);
                    mailMessage.Subject = subject;
                    var htmlBody = new StringBuilder();
                    htmlBody.Append("<font face=\"Arial\">" + body + "</font>");
                    htmlBody.Append("");
                    //htmlBody.Append("<br />&nbsp;<br />");
                    //htmlBody.Append("");
                    //htmlBody.Append("<font face=\"Arial\">Username: <strong>" + user.EmployeeEmail + "<br />");
                    //htmlBody.Append("");
                    //htmlBody.Append("</strong>Password: <strong>" + user.Password + "");
                    //htmlBody.Append("");
                    //htmlBody.Append("</strong>");
                    //htmlBody.Append("");
                    //htmlBody.Append("<br />");
                    //htmlBody.Append("");
                    //htmlBody.Append("<br />");
                    //htmlBody.Append("");
                    //htmlBody.Append("You can login with your new information here: " + "www.tvs.in" + "&nbsp;" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "<br />&nbsp;<br />");
                    //htmlBody.Append("");
                    //htmlBody.Append("If you have any questions or trouble logging on please contact site administrator.<br />&nbsp;<br />");
                    //htmlBody.Append("");
                    htmlBody.Append("Thank You!<br />&nbsp;<br />");
                    htmlBody.Append("");
                    htmlBody.Append("TVS Emerald.");
                    htmlBody.Append("");
                    //htmlBody.Append("</font>");
                    mailMessage.Body = htmlBody.ToString();
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(toMailAddr));
                    result = MailBox(mailMessage);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool MailBox(MailMessage mailMessage)
        {
            try
            {
                //smtp.office365.com; 25; customercare @tvsemerald.com; Password1
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["UserName"], //reading from web.config  
                    Password = ConfigurationManager.AppSettings["Password"] //reading from web.config  
                };
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
