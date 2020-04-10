using Helper;
using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarOfTheMonth.Repo
{
    public interface IConfigurationRepo : IDisposable
    {
        RepositoryResponse LoadConfigurationGridData();
        RepositoryResponse LoadConfigurationDetailsByID(long ID);
        RepositoryResponse AddOrEditConfigurationDetails(ConfigurationModel model, string _loggedInUserID);
        ConfigurationModel GetEmailDetails();

        void SendEmailUsingSOM_Nominee_Submit_HOD(string nominationID, string loggedInUserID);

        void SendEmailUsingSOM_HOD_ReAssign_Nominee(string nominationID, string loggedInUserID);

        void SendEmailUsingSOM_HOD_Assign_TQC(string nominationID, string loggedInUserID);

        void SendEmailUsingSOM_TQC_Assign_Evaluator(string nominationID, string loggedInUserID);

        void SendEmailUsingSOM_Evaluator_Assign_TQC(string nominationID, string loggedInUserID);

        void SendEmailUsingSOM_TQC_Declare_SOM(string nominationID, string loggedInUserID);

        void SendEmailUsingSOM_HOD_Reject_Nominee(string nominationID, string loggedInUserID);
    }
    public class ConfigurationRepo : IConfigurationRepo
    {
        private SOMEntities objSOMEntities;
        private IntranetPortalEntities objIPEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;

        public RepositoryResponse LoadConfigurationGridData()
        {
            baseModel = new RepositoryResponse();
            try
            {
                string subMonth = string.Empty;
                string subYear = string.Empty;
              
                List<ConfigurationModel> lstConfig = new List<ConfigurationModel>();
                using (objSOMEntities = new SOMEntities())
                {
                    var _lstlstConfig = objSOMEntities.Configurations.Where(r => r.IsActive == true && r.IsDisplayUI == true).ToList();

                    int i = 1;
                    foreach (var item in _lstlstConfig)
                    {
                        ConfigurationModel model = new ConfigurationModel();
                        model.SlNo = i;
                        model.ID = item.ID;
                        model.Description = item.Description;
                        model.IsActive = (bool)item.IsActive;
                        model.Module = item.Module;
                        model.Type = item.Type;
                        model.Value = item.Value.Replace(",",", ");
                        lstConfig.Add(model);
                        i++;
                    }
                    baseModel = new RepositoryResponse { success = true, message = "Successfully Configuration retrieved for Grid", Data = lstConfig };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public RepositoryResponse LoadConfigurationDetailsByID(long ID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    ConfigurationModel model = new ConfigurationModel();              
                    var _data = objSOMEntities.Configurations.Where(r => r.ID == ID && r.IsActive == true).FirstOrDefault();
                    if (_data != null)
                    {
                        model.ID = _data.ID;
                        model.Description = _data.Description;
                        model.IsActive = (bool)_data.IsActive;
                        model.Module = _data.Module;
                        model.Type = _data.Type;
                        model.Value = _data.Value;
                        if (_data.Module == "SMTP" && _data.Type == "SMTPDetails")
                        {
                            string[] split = _data.Value.Split(';');
                            model.SMTP = split[0];
                            model.PortNo = split[1];
                            model.FromUserID = split[2];
                            model.Password = split[3];
                        }
                    }
                    baseModel = new RepositoryResponse { success = true, message = "Successfully retrieved Configuration Details for ID : " + ID.ToString(), Data = model };
                }
            }
            catch (Exception ex)
            {

                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public ConfigurationModel GetEmailDetails()
        {
            ConfigurationModel model = new ConfigurationModel();
            using (objSOMEntities = new SOMEntities())
            {
                var _data = objSOMEntities.Configurations.Where(r => r.Module == "SMTP" && r.Type == "SMTPDetails" && r.IsActive == true).FirstOrDefault();
                
                if (!string.IsNullOrEmpty(_data.Value))
                {
                    string[] split = _data.Value.Split(';');
                    model.SMTP = split[0];
                    model.PortNo = split[1];
                    model.FromUserID = split[2];
                    model.Password = split[3];
                }
            }
            return model;
        }

        public RepositoryResponse AddOrEditConfigurationDetails(ConfigurationModel model, string _loggedInUserID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var db = objSOMEntities.Configurations.Where(r => r.ID == model.ID).First();
                    if (db == null)
                    {
                        db = new Configuration();
                        db.Description = model.Description;                       
                        db.Module = model.Module;
                        if (db.Module == "SMTP" && db.Type == "SMTPDetails")
                        {
                            db.Value = model.SMTP + "; " + model.PortNo + "; " + model.FromUserID + "; " + model.Password;
                        }
                        else
                        {
                            db.Value = model.Value;
                        }   
                        db.Type = model.Type;
                        db.CreatedBy = _loggedInUserID;
                        objSOMEntities.Configurations.Add(db);
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Successfully Added Configuration Details" };
                    }
                    else
                    {
                        //db.Description = model.Description;
                        if (db.Module == "SMTP" && db.Type == "SMTPDetails")
                        {
                            db.Value = model.SMTP + "; " + model.PortNo + "; " + model.FromUserID + "; " + model.Password;
                        }
                        else
                        {
                            db.Value = model.Value;
                        }
                        db.ModifiedBy = _loggedInUserID;
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Successfully updated Configuration Details" };
                    }
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    objSOMEntities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #region sendEmail 

        public void SendEmailUsingSOM_Nominee_Submit_HOD(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month Nominee Form Submit to HoD";
            string message = "Dear ##UserName##, <br><br>";
            message += "<pre>  The below Nominee user has submitted Nomination from for your review. Please find the below details. " + "</pre><br><br>";
            message += "<pre> Nominee User name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";
            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##NomineeUserName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var config = objSOMEntities.Configurations.Where(r => r.Module == "MAIL" && r.Type == "Nomination" && r.IsActive == true).FirstOrDefault();
                message = config.Value;
                subject = config.Description;
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();

                message.Replace("##UserName##", rptPerson.EmployeeName);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = rptPerson.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        public void SendEmailUsingSOM_HOD_ReAssign_Nominee(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month HoD ReAssign to Nominee";
            string message = "Dear ##UserName##, <br><br>";
            message += "<pre>  The HoD user has need more information to approve Nomination from please refer HoD comments section. Please find the below details. " + "</pre><br><br>";
            message += "<pre> HoD Name              :   ##HODUserName## </pre>";
            message += "<pre> HoD Employee No       :   ##HODUserNo## </pre>";
            message += "<pre> HoD Comments          :   ##HoDComments## </pre>";
            message += "<pre> Nominee User Name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";
            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##NomineeUserName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var config = objSOMEntities.Configurations.Where(r => r.Module == "MAIL" && r.Type == "NominationNeedClarification" && r.IsActive == true).FirstOrDefault();
                message = config.Value;
                subject = config.Description;

                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();

                message.Replace("##HODUserName##", rptPerson.EmployeeName);
                message.Replace("##HODUserNo##", rptPerson.EmployeeName);
                message.Replace("##HoDComments##", nom.DHComments);

                message.Replace("##UserName##", rptPerson.EmployeeName);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = emp.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        public void SendEmailUsingSOM_HOD_Assign_TQC(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month HoD Assign to TQC";
            string message = "Dear ##TQCUserName##, <br><br>";
            message += "<pre>  The HoD user has approved nominee information and submitted to TQC. Please find the below details. " + "</pre><br><br>";
            message += "<pre> HoD Name              :   ##HODUserName## </pre>";
            message += "<pre> HoD Employee No       :   ##HODUserNo## </pre>";
            message += "<pre> HoD Comments          :   ##HoDComments## </pre>";
            message += "<pre> Nominee User Name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";
            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##HODUserName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var config = objSOMEntities.Configurations.Where(r => r.Module == "MAIL" && r.Type == "NominationApproved" && r.IsActive == true).FirstOrDefault();
                message = config.Value;
                subject = config.Description;

                var tqc = objSOMEntities.TQCHeads.Where(r => r.IsActive == true).FirstOrDefault();
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();
                var tqcPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == tqc.EmployeeNumber.ToString()).FirstOrDefault();

                message.Replace("##HODUserName##", rptPerson.EmployeeName);
                message.Replace("##HODUserNo##", rptPerson.EmployeeName);
                message.Replace("##HoDComments##", nom.DHComments);

                message.Replace("##UserName##", tqc.Name);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                message.Replace("##TQCName##", tqc.Name);
                message.Replace("##TQCNo##", tqc.EmployeeNumber);



                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = tqcPerson.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        public void SendEmailUsingSOM_TQC_Assign_Evaluator(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month TQC Add to Evaluator for Nomination";
            string message = "Dear ##EvalName##, <br><br>";
            message += "<pre>  The TQC user has add evaluation user to evaluate nomination form. Please find the below details. " + "</pre><br><br>";

            message += "<pre> HoD Name              :   ##HODUserName## </pre>";
            message += "<pre> HoD Employee No       :   ##HODUserNo## </pre>";
            message += "<pre> HoD Comments          :   ##HoDComments## </pre>";

            message += "<pre> Nominee User Name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";

            message += "<pre> Evaluator Name        :   ##EvalName## </pre>";
            message += "<pre> Evaluator No          :   ##EvalNo## </pre>";

            message += "<pre> TQC Name              :   ##TQCName## </pre>";
            message += "<pre> TQC No                :   ##TQCNo## </pre>";

            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##TQCName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var config = objSOMEntities.Configurations.Where(r => r.Module == "MAIL" && r.Type == "NominationAssignedForEvaluation" && r.IsActive == true).FirstOrDefault();
                message = config.Value;
                subject = config.Description;

                var tqc = objSOMEntities.TQCHeads.Where(r => r.IsActive == true).FirstOrDefault();
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var eval = objSOMEntities.Evaluations.Where(r => r.NominationID == nominationID).FirstOrDefault();
                var evalEmp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == eval.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();
                var tqcPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == tqc.EmployeeNumber.ToString()).FirstOrDefault();

                message.Replace("##HODUserName##", rptPerson.EmployeeName);
                message.Replace("##HODUserNo##", rptPerson.EmployeeName);
                message.Replace("##HoDComments##", nom.DHComments);

                message.Replace("##UserName##", tqc.Name);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                message.Replace("##TQCName##", tqc.Name);
                message.Replace("##TQCNo##", tqc.EmployeeNumber);
                message.Replace("##EvalName##", evalEmp.EmployeeName);
                message.Replace("##EvalNo##", evalEmp.EmployeeNumber);

                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = evalEmp.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        public void SendEmailUsingSOM_Evaluator_Assign_TQC(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month - Evaluator Submit Score Details to TQC";
            string message = "Dear ##TQCName##, <br><br>";
            message += "<pre>  The ##EvalName## user has evaluated the nomination form and submitted to TQC Head. Please find the below details. " + "</pre><br><br>";

            message += "<pre> HoD Name              :   ##HODUserName## </pre>";
            message += "<pre> HoD Employee No       :   ##HODUserNo## </pre>";
            message += "<pre> HoD Comments          :   ##HoDComments## </pre>";

            message += "<pre> Nominee User Name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";

            message += "<pre> Evaluator Name        :   ##EvalName## </pre>";
            message += "<pre> Evaluator No          :   ##EvalNo## </pre>";

            message += "<pre> TQC Name              :   ##TQCName## </pre>";
            message += "<pre> TQC No                :   ##TQCNo## </pre>";

            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##EvalName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var config = objSOMEntities.Configurations.Where(r => r.Module == "MAIL" && r.Type == "NominationEvaluationComplete" && r.IsActive == true).FirstOrDefault();
                message = config.Value;
                subject = config.Description;

                var tqc = objSOMEntities.TQCHeads.Where(r => r.IsActive == true).FirstOrDefault();
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var eval = objSOMEntities.Evaluations.Where(r => r.NominationID == nominationID).FirstOrDefault();
                var evalEmp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == eval.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();
                var tqcPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == tqc.EmployeeNumber.ToString()).FirstOrDefault();

                message.Replace("##HODUserName##", rptPerson.EmployeeName);
                message.Replace("##HODUserNo##", rptPerson.EmployeeName);
                message.Replace("##HoDComments##", nom.DHComments);

                message.Replace("##UserName##", tqc.Name);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                message.Replace("##TQCName##", tqc.Name);
                message.Replace("##TQCNo##", tqc.EmployeeNumber);
                message.Replace("##EvalName##", evalEmp.EmployeeName);
                message.Replace("##EvalNo##", evalEmp.EmployeeNumber);

                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = tqcPerson.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        public void SendEmailUsingSOM_TQC_Declare_SOM(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month TQC Declare SOM";
            string message = "Dear ##NomineeUserName##, <br><br>";
            message += "<pre>  The ##NomineeUserName## user has declared the Star of the Month for ##SubMonth## - ##SubYear##. Please find the below details. " + "</pre><br><br>";

            message += "<pre> HoD Name              :   ##HODUserName## </pre>";
            message += "<pre> HoD Employee No       :   ##HODUserNo## </pre>";
            message += "<pre> HoD Comments          :   ##HoDComments## </pre>";

            message += "<pre> Nominee User Name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";

            message += "<pre> Evaluator Name        :   ##EvalName## </pre>";
            message += "<pre> Evaluator No          :   ##EvalNo## </pre>";

            message += "<pre> TQC Name              :   ##TQCName## </pre>";
            message += "<pre> TQC No                :   ##TQCNo## </pre>";

            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##TQCName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var tqc = objSOMEntities.TQCHeads.Where(r => r.IsActive == true).FirstOrDefault();
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var eval = objSOMEntities.Evaluations.Where(r => r.NominationID == nominationID).FirstOrDefault();
                var evalEmp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == eval.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();

                message.Replace("##HODUserName##", rptPerson.EmployeeName);
                message.Replace("##HODUserNo##", rptPerson.EmployeeName);
                message.Replace("##HoDComments##", nom.DHComments);

                message.Replace("##UserName##", tqc.Name);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                message.Replace("##TQCName##", tqc.Name);
                message.Replace("##TQCNo##", tqc.EmployeeNumber);
                message.Replace("##EvalName##", evalEmp.EmployeeName);
                message.Replace("##EvalNo##", evalEmp.EmployeeNumber);

                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = emp.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                //sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        public void SendEmailUsingSOM_HOD_Reject_Nominee(string nominationID, string loggedInUserID)
        {
            string subject = "Star of the Month HoD Reject to Nominee";
            string message = "Dear ##UserName##, <br><br>";
            message += "<pre>  The HoD user has rejected Nomination from please refer HoD comments section. Please find the below details. " + "</pre><br><br>";
            message += "<pre> HoD Name              :   ##HODUserName## </pre>";
            message += "<pre> HoD Employee No       :   ##HODUserNo## </pre>";
            message += "<pre> HoD Comments          :   ##HoDComments## </pre>";
            message += "<pre> Nominee User Name     :   ##NomineeUserName## </pre>";
            message += "<pre> Nominee Employee No   :   ##NomineeUserNo## </pre>";
            message += "<pre> Nomination ID         :   ##NominationID## </pre>";
            message += "<pre> Project Title         :   ##ProjectTitle## </pre>";
            message += "<pre> Submitted Month       :   ##SubMonth## </pre>";
            message += "<pre> Submitted Year        :   ##SubYear## </pre>";
            message += "<br><br>";
            message += "Thanks,<br>";
            message += "##NomineeUserName##";

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == nom.EmployeeNumber).FirstOrDefault();
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == emp.ReportingPersonId.ToString()).FirstOrDefault();

                message.Replace("##HODUserName##", rptPerson.EmployeeName);
                message.Replace("##HODUserNo##", rptPerson.EmployeeName);
                message.Replace("##HoDComments##", nom.DHComments);

                message.Replace("##UserName##", rptPerson.EmployeeName);
                message.Replace("##NomineeUserName##", emp.EmployeeName);
                message.Replace("##NomineeUserNo##", nom.EmployeeNumber);
                message.Replace("##NominationID##", nom.NominationId);
                message.Replace("##ProjectTitle##", nom.ProjectTitle);
                message.Replace("##SubMonth##", nom.SubmittedMonth);
                message.Replace("##SubYear##", nom.SubmittedYear);

                ConfigurationModel emailDet = GetEmailDetails();

                EmailParam objEmail = new EmailParam();
                objEmail.PrimaryEnableSsl = true;
                objEmail.PrimaryFrom = emailDet.FromUserID;
                objEmail.PrimaryPassword = emailDet.Password;
                objEmail.PrimaryPortNo = int.Parse(emailDet.PortNo);
                objEmail.PrimarySMTP = emailDet.SMTP;
                objEmail.ToEmailAddress = emp.EmployeeEmail;
                objEmail.MailContent = message;
                objEmail.MailSubject = subject;

                SendEmail sendEmail = new SendEmail();
                //sendEmail.SendEmailUsingPrimary(objEmail);
            }
        }

        #endregion
    }
}
