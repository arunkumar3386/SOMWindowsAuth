using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Helper
{
    public class EmailParam
    {
        public string PrimarySMTP { get; set; }
        public string PrimaryUserName { get; set; }
        public string PrimaryFrom { get; set; }
        public string PrimaryPassword { get; set; }
        public int PrimaryPortNo { get; set; }
        public bool PrimaryUseDefaultCredentials { get; set; }
        public bool PrimaryEnableSsl { get; set; }

        public string SecondarySMTP { get; set; }
        public string SecondaryUserName { get; set; }
        public string SecondaryFrom { get; set; }
        public string SecondaryPassword { get; set; }
        public int SecondaryPortNo { get; set; }
        public bool SecondaryUseDefaultCredentials { get; set; }
        public bool SecondaryEnableSsl { get; set; }

        public string ToEmailAddress { get; set; }
        public string CCEmailAddress { get; set; }
        public string BCCEmailAddress { get; set; }

        public string MailContent { get; set; } //with design template
        public string MailSubject { get; set; }
        public string AloneMailContent { get; set; } //with out design template

        public string ApplicationName { get; set; }

    }

    public class SendEmail
    {
        /* This function is used to avoid the single quotes */
        private string replacesplchr(string txt)
        {
            if ((txt != null) && (txt != string.Empty) && (txt != ""))
            {
                txt = txt.Replace("'", "").Replace(",", "");
                txt = txt.Trim();
            }
            else
            {
                txt = string.Empty;
            }

            return txt;
        }

        private MailAddressCollection GetMailAddressList(string EmailId)
        {
            MailAddressCollection objMAC = new MailAddressCollection();
            string MailAddresses = string.Empty;


            MailAddresses = NullToSpace(EmailId);

            string[] mailIdCollections = MailAddresses.Split(';');

            if (mailIdCollections.Count() > 0)
            {
                foreach (string mailId in mailIdCollections)
                {
                    if (mailId != string.Empty)
                    {
                        objMAC.Add(new MailAddress(mailId));
                    }
                }
            }
            else
            {
                if (MailAddresses != string.Empty)
                {
                    objMAC.Add(new MailAddress(MailAddresses));
                }
            }

            return objMAC;
        }

        private string NullToSpace(object txt)
        {
            string strRetValue = string.Empty;

            if (txt == DBNull.Value)
            {
                strRetValue = "";
            }
            else if (txt == null)
            {
                strRetValue = "";
            }
            else if ((txt.ToString() == "") || (txt.ToString() == " "))
            {
                strRetValue = "";
            }
            else
            {
                strRetValue = Convert.ToString(txt).Trim();
            }

            return strRetValue;
        }

        public bool SendEmailUsingPrimary(EmailParam objEmail)
        {

            byte[] bArray = { };

            string P_SMTP = objEmail.PrimarySMTP;
            string P_MsgFrom = objEmail.PrimaryFrom;
            string P_MailFrmUID = objEmail.PrimaryUserName;
            string P_MailFrmPW = objEmail.PrimaryPassword;
            int P_MailFrmPort = objEmail.PrimaryPortNo;
            bool P_PrimaryUseDefaultCredentials = objEmail.PrimaryUseDefaultCredentials;
            bool P_PrimaryEnableSsl = objEmail.PrimaryEnableSsl;

            string S_SMTP = objEmail.SecondarySMTP;
            string S_MsgFrom = objEmail.SecondaryFrom;
            string S_MailFrmUID = objEmail.SecondaryUserName;
            string S_MailFrmPW = objEmail.SecondaryPassword;
            int S_MailFrmPort = objEmail.SecondaryPortNo;
            bool S_PrimaryUseDefaultCredentials = objEmail.SecondaryUseDefaultCredentials;
            bool S_PrimaryEnableSsl = objEmail.SecondaryEnableSsl;

            string MsgTo = string.Empty;
            string ccEmail = string.Empty;
            string bccEmail = string.Empty;
            string MsgSubject = string.Empty;
            string MsgBody = string.Empty;


            MailMessage msg = new MailMessage();
            MailAddressCollection lst_ToMAC;
            MailAddressCollection lst_CCMAC;
            MailAddressCollection lst_BCCMAC;

            bool bMailSent = false;
            try
            {
                MsgSubject = objEmail.MailSubject;

                MsgBody = objEmail.MailContent;

                msg = new MailMessage(objEmail.ApplicationName + " <" + objEmail.PrimaryFrom + ">", objEmail.ToEmailAddress, MsgSubject, MsgBody);
                msg.IsBodyHtml = true;

                lst_ToMAC = GetMailAddressList(objEmail.ToEmailAddress);

                foreach (var item in lst_ToMAC)
                {
                    msg.To.Add(item);
                }

                if (!string.IsNullOrEmpty(objEmail.CCEmailAddress))
                {
                    lst_CCMAC = GetMailAddressList(objEmail.CCEmailAddress);

                    foreach (var item in lst_CCMAC)
                    {
                        msg.CC.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(objEmail.BCCEmailAddress))
                {
                    lst_BCCMAC = GetMailAddressList(objEmail.BCCEmailAddress);
                    foreach (var item in lst_BCCMAC)
                    {
                        msg.Bcc.Add(item);
                    }
                }

                SmtpClient mailClient = new SmtpClient(P_SMTP, P_MailFrmPort);
                NetworkCredential NetCrd = new NetworkCredential(P_MsgFrom, P_MailFrmPW);
                mailClient.UseDefaultCredentials = P_PrimaryUseDefaultCredentials;
                mailClient.Credentials = NetCrd;
                mailClient.EnableSsl = P_PrimaryEnableSsl;

                //mailClient.Send(msg);
                bMailSent = true;
                //SaveEmailMessage(objEmail, true, true);
            }
            catch (Exception ex)
            {
                bMailSent = false;
                //SaveEmailMessage(objEmail, false, true, ex);
            }
            finally
            {
                if (msg != null)
                {
                    msg.Dispose();
                    msg = null;
                }
            }
            return bMailSent;
        }

        public bool SendEmailUsingSecondary(EmailParam objEmail)
        {

            byte[] bArray = { };

            string P_SMTP = objEmail.PrimarySMTP;
            string P_MsgFrom = objEmail.PrimaryFrom;
            string P_MailFrmUID = objEmail.PrimaryUserName;
            string P_MailFrmPW = objEmail.PrimaryPassword;
            int P_MailFrmPort = objEmail.PrimaryPortNo;
            bool P_PrimaryUseDefaultCredentials = objEmail.PrimaryUseDefaultCredentials;
            bool P_PrimaryEnableSsl = objEmail.PrimaryEnableSsl;

            string S_SMTP = objEmail.SecondarySMTP;
            string S_MsgFrom = objEmail.SecondaryFrom;
            string S_MailFrmUID = objEmail.SecondaryUserName;
            string S_MailFrmPW = objEmail.SecondaryPassword;
            int S_MailFrmPort = objEmail.SecondaryPortNo;
            bool S_PrimaryUseDefaultCredentials = objEmail.SecondaryUseDefaultCredentials;
            bool S_PrimaryEnableSsl = objEmail.SecondaryEnableSsl;

            string MsgTo = string.Empty;
            string ccEmail = string.Empty;
            string bccEmail = string.Empty;
            string MsgSubject = string.Empty;
            string MsgBody = string.Empty;


            MailMessage msg = new MailMessage();
            MailAddressCollection lst_ToMAC;
            MailAddressCollection lst_CCMAC;
            MailAddressCollection lst_BCCMAC;

            bool bMailSent = false;
            try
            {
                MsgSubject = objEmail.MailSubject;

                MsgBody = objEmail.MailContent;

                msg = new MailMessage(objEmail.ApplicationName + " <" + objEmail.SecondaryFrom + ">", objEmail.ToEmailAddress, MsgSubject, MsgBody);
                msg.IsBodyHtml = true;

                lst_ToMAC = GetMailAddressList(objEmail.ToEmailAddress);

                foreach (var item in lst_ToMAC)
                {
                    msg.To.Add(item);
                }

                if (!string.IsNullOrEmpty(objEmail.CCEmailAddress))
                {
                    lst_CCMAC = GetMailAddressList(objEmail.CCEmailAddress);

                    foreach (var item in lst_CCMAC)
                    {
                        msg.CC.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(objEmail.BCCEmailAddress))
                {
                    lst_BCCMAC = GetMailAddressList(objEmail.BCCEmailAddress);
                    foreach (var item in lst_BCCMAC)
                    {
                        msg.Bcc.Add(item);
                    }
                }

                SmtpClient mailClient = new SmtpClient(S_SMTP, S_MailFrmPort);
                NetworkCredential NetCrd = new NetworkCredential(S_MsgFrom, S_MailFrmPW);
                mailClient.UseDefaultCredentials = S_PrimaryUseDefaultCredentials;
                mailClient.Credentials = NetCrd;
                mailClient.EnableSsl = S_PrimaryEnableSsl;

                mailClient.Send(msg);
                bMailSent = true;
                SaveEmailMessage(objEmail, true, false);
            }
            catch (Exception ex)
            {
                bMailSent = false;
                SaveEmailMessage(objEmail, false, false, ex);
            }
            return bMailSent;
        }

        private void SaveEmailMessage(EmailParam objEmail, bool eMailStatus, bool isPrimary, Exception ex = null)
        {
            //TO DO
        }

        public string GetEmailTemplateByBankname(string sbankName, string path)
        {
            if (sbankName == "UBI")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\UBIdesign.html");
            }
            else if (sbankName == "UBL")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\UBLdesign.html");
            }
            else if (sbankName == "ICICI")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\ICICIdesign.html");
            }
            else if (sbankName == "RBL")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\RBLdesign.html");
            }
            else if (sbankName == "ANB")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\ANBdesign.html");
            }
            else if (sbankName == "BOI")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\BOIdesign.html");
            }
            else if (sbankName == "CANARA")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\CANARAdesign.html");
            }
            else if (sbankName == "PNB")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\PNBdesign.html");
            }
            else if (sbankName == "SEPAH")
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\SEPAHdesign.html");
            }
            else
            {
                path = System.IO.Path.GetFullPath(path + "NewEmailTemplate\\design.html");
            }
            return path;
        }
    }
}
