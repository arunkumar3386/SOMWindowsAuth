using Helper;
using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarOfTheMonth.Repo
{
    public interface INominationRepo : IDisposable
    {
        RepositoryResponse AddOrEditNominationDetails(string loggedInUserID, NominationModel model, bool IsSubmit = false);

        RepositoryResponse LoadNomination(string loggedInUserID, string dept = "", string date = "", string toDate="", bool isRejected = false);

        RepositoryResponse GetNominationByID(string loggedInUserID, long ID, string fPath);

        RepositoryResponse GetReportDetails(string loggedInUserID, string dept="", string Fdate="", string Tdate = "");

        RepositoryResponse NominationFormDHOperations(string loggedInUserID, NominationModel model, int nominationStatus);

        RepositoryResponse NominationRestrictionCount(string loggedInUserID);

        RepositoryResponse GetNominationByEmplyoeeNumber(string loggedInUserID);

        RepositoryResponse LoadNominationForDH(string loggedInUserID, string dept = "", string date = "", string status="");

        void AddEntryIntoAuditLog(AuditLogModel auditLog);

        void AddFileNameToNomination(string nominationID, string FileNames);

        List<AutoCompleteBox> GetEvaltorDetailsForAutoCompleteBox(bool isRequiredDept=false);

        RepositoryResponse AssignEvalatorToNominationByTQCHead(DashboardModel model, string loggedInUserID);

        int GetReportingIDByEmpID(string EmployeeNumber);

        string GetNomineeContentsForPDFExport(string content, string ID);
    }

    public class NominationRepo : INominationRepo
    {
        private ILoginRepo LoginRepo;
        public SOMEntities objSOMEntities;
        public IntranetPortalEntities objIPEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;
        IPanelMembersRepo panelMembersRepo;
        string _message = string.Empty;
        IConfigurationRepo configRepo;

        public object HttpContext { get; private set; }

        public RepositoryResponse AddOrEditNominationDetails(string loggedInUserID, NominationModel model,  bool IsSubmit)
        {
            baseModel = new RepositoryResponse();
            Nomination dbModel;
            string nominationID = "";
            long _nominationID = 0;
            bool newInsert = false;
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var _config = objSOMEntities.Configurations.Where(r => r.Module == "SOM" 
                                && r.Type == "NominationID" && r.IsActive == true)
                                .Select(r => r.Value).FirstOrDefault();
                    if (_config != null)
                    {
                        nominationID = _config;
                        string year = nominationID.Substring(0, 4);
                        string currentYear = DateTime.Now.ToString("yyyy");
                        if (year == currentYear)
                        {
                            _nominationID = long.Parse(nominationID);
                            _nominationID = _nominationID + 1;
                        }
                        else
                        {
                            nominationID = nominationID.Replace(year, currentYear);
                            _nominationID = long.Parse(nominationID);
                            _nominationID = _nominationID + 1;
                        }
                    }
                }
                using (objSOMEntities = new SOMEntities()) 
                using (objIPEntities = new IntranetPortalEntities())
                {
                    DateTime now = DateTime.Now;
                    dbModel = new Nomination();
                    var usr = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == loggedInUserID).FirstOrDefault();
                    dbModel = objSOMEntities.Nominations.Where(r => r.ID == model.ID).FirstOrDefault();

                    if (dbModel != null)
                    {   
                        dbModel.EmployeeNumber = usr.EmployeeNumber;
                        dbModel.Acknowledgement = model.Acknowledgement;
                        dbModel.Benefits = model.Benefits;
                        dbModel.BriefDescription = model.BriefDescription;
                        //dbModel.StartDate = model.StartDate.Value.ToString("ddMMyyyy");
                        //dbModel.EndDate = model.EndDate.Value.ToString("ddMMyyyy");
                        dbModel.StartDate = model.sStartDate.Replace("/", "");
                        dbModel.EndDate = model.sEndDate.Replace("/", "");
                        dbModel.ProjectTitle = model.ProjectTitle;
                        dbModel.Idea = model.Idea;
                        dbModel.IsActive = true;
                        dbModel.PrimaryObjective = model.PrimaryObjective; 
                        
                        if (IsSubmit)
                        {
                            if (dbModel.Status == (int)NominationStatus.HoD_Assign_EmployeeClarification)
                            {
                                dbModel.Status = (int)NominationStatus.Employee_ReAssign_HoD;
                            }
                            else
                            {
                                dbModel.Status = (int)NominationStatus.Employee_Assign_HOD;
                            }
                           
                            dbModel.NomSubmittedDate = DateTime.Now.ToString("ddMMyyyy");
                            dbModel.NomSignature = loggedInUserID.ToString();
                        }
                        else
                        {
                            dbModel.Status = (int)NominationStatus.Employee_Save;
                        }
                        dbModel.SubmittedMonth = Assistant.GetMonthFromCurrentDate();
                        dbModel.SubmittedYear = Assistant.GetYearFromCurrentDate();
                        dbModel.ModifiedBy = loggedInUserID.ToString();

                        dbModel.Cost = model.Cost;
                        dbModel.Time = model.Time;
                        dbModel.Paper = model.Paper;
                        dbModel.Savings = string.Join(",", model.SavingsIds);
                        //dbModel.UploadedFileNames = model.UploadedFileNames;
                        dbModel.NominationId = model.NominationID;
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Nomination Details updated Successfully", Data = dbModel.NominationId };
                    }
                    else
                    {
                        dbModel = new Nomination();
                        dbModel.EmployeeNumber = usr.EmployeeNumber;
                        dbModel.Acknowledgement = model.Acknowledgement;
                        dbModel.Benefits = model.Benefits;
                        dbModel.BriefDescription = model.BriefDescription;
                        dbModel.StartDate = model.sStartDate.Replace("/", "");
                        dbModel.EndDate = model.sEndDate.Replace("/", "");
                        dbModel.ProjectTitle = model.ProjectTitle;
                        //dbModel.StartDate = model.StartDate.Value.ToString("ddMMyyyy");
                        //dbModel.EndDate = model.EndDate.Value.ToString("ddMMyyyy");
                        dbModel.Idea = model.Idea;
                        dbModel.IsActive = true;
                        dbModel.PrimaryObjective = model.PrimaryObjective;
                        
                        if (IsSubmit)
                        {
                            dbModel.Status = (int)NominationStatus.Employee_Assign_HOD;
                            dbModel.NomSubmittedDate = DateTime.Now.ToString("ddMMyyyy");
                            dbModel.NomSignature = loggedInUserID.ToString();
                        }
                        else
                        {
                            dbModel.Status = (int)NominationStatus.Employee_Save;
                        }
                        dbModel.SubmittedMonth = Assistant.GetMonthFromCurrentDate();
                        dbModel.SubmittedYear = Assistant.GetYearFromCurrentDate();
                        dbModel.CreatedBy = loggedInUserID.ToString();

                        dbModel.Cost = model.Cost;
                        dbModel.Time = model.Time;
                        dbModel.Paper = model.Paper;
                        dbModel.Savings = string.Join(",", model.SavingsIds);
                        //dbModel.UploadedFileNames = model.UploadedFileNames;
                        

                        dbModel.NominationId = _nominationID.ToString();

                        objSOMEntities.Nominations.Add(dbModel);
                        objSOMEntities.SaveChanges();
                        newInsert = true;
                        baseModel = new RepositoryResponse { success = true, message = "Nomination Details added Successfully", Data = dbModel.NominationId };
                    }
                }

                if (newInsert)
                {
                    using (objSOMEntities = new SOMEntities())
                    {
                        var _config = objSOMEntities.Configurations.Where(r => r.Module == "SOM"
                                   && r.Type == "NominationID" && r.IsActive == true)
                                   .FirstOrDefault();
                        if (_config != null)
                        {
                            _config.Value = _nominationID.ToString();
                            objSOMEntities.SaveChanges();
                        }
                    }
                }
             

                using (objSOMEntities = new SOMEntities())
                {
                    var _hdep = objSOMEntities.HorizontalDeployments.Where(r => r.NominationID == dbModel.NominationId).ToList();
                    foreach (var item in _hdep)
                    {
                        item.IsActive = false;
                        objSOMEntities.SaveChanges();
                    }
                }
                BindHoriozontalDeploymentData(dbModel.NominationId, model.lstHDIDs, model.lstAreaOfInterest, model.lstImplementationStatus, loggedInUserID);

                if (IsSubmit)
                {
                    //TQCHead _TQCHead;
                    //using (objSOMEntities = new SOMEntities())
                    //{
                    //    _TQCHead = objSOMEntities.TQCHeads.Where(r => r.IsActive == true).FirstOrDefault();
                    //}
                    long HODId = GetReportingIDByEmpID(dbModel.EmployeeNumber);
                    AuditLogModel objAuditLog = new AuditLogModel();
                    if (dbModel.Status == 2002)
                    {
                        objAuditLog.CurrentStatus = NominationStatus.Employee_Assign_HOD;
                    }
                    else if (dbModel.Status == 2013)
                    {
                        objAuditLog.CurrentStatus = NominationStatus.Employee_ReAssign_HoD;
                    }
                    objAuditLog.EmployeeNumber = dbModel.EmployeeNumber;
                    objAuditLog.IsNewAlert = true;
                    objAuditLog.IsNotification = true;
                    objAuditLog.NominationID = dbModel.NominationId;
                    objAuditLog.DepartmentHeadID = HODId.ToString();
                    //objAuditLog.TQCHeadID = _TQCHead.EmployeeNumber;
                    objAuditLog.CreatedBy = loggedInUserID;
                    objAuditLog.Comments = model.AuditLogComments;
                    AddEntryIntoAuditLog(objAuditLog);
                    baseModel = new RepositoryResponse { success = true, message = "Nomination Details saved and assigned to HoD", Data = dbModel.NominationId };
                }
                else
                {
                    AuditLogModel objAuditLog = new AuditLogModel();
                    objAuditLog.CurrentStatus = NominationStatus.Employee_Save;
                    objAuditLog.EmployeeNumber = dbModel.EmployeeNumber;
                    objAuditLog.IsNewAlert = true;
                    objAuditLog.IsNotification = true;
                    objAuditLog.NominationID = dbModel.NominationId;
                    objAuditLog.CreatedBy = loggedInUserID;
                    objAuditLog.Comments = model.AuditLogComments;
                    AddEntryIntoAuditLog(objAuditLog);
                    baseModel = new RepositoryResponse { success = true, message = "Nomination Details Updated Successfully", Data = dbModel.NominationId };
                }
                if (IsSubmit)
                {
                    configRepo = new ConfigurationRepo();
                    bool result = configRepo.SendEmailUsingSOM_Nominee_Submit_HOD(dbModel.NominationId, loggedInUserID);
                    if (result)
                    {
                        baseModel = new RepositoryResponse { success = true, message = "Nomination Details Updated Successfully and Sent Mail to HoD", Data = dbModel.NominationId };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = true, message = "Nomination Details Updated Successfully but Unable to Send a Mail", Data = dbModel.NominationId };
                    }

                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            
            return baseModel;
        }

        public RepositoryResponse NominationFormDHOperations(string loggedInUserID, NominationModel model, int nominationStatus)
        {
            baseModel = new RepositoryResponse();
            Nomination objNomination;
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    objNomination = objSOMEntities.Nominations.Where(r => r.ID == model.ID).FirstOrDefault();
                    if (objNomination != null)
                    {
                        objNomination.DHComments = model.CommentsByDH;
                        objNomination.Status = nominationStatus;
                        if (nominationStatus == (int)NominationStatus.HoD_Assign_TQC || nominationStatus == (int)NominationStatus.HoD_Reject)
                        {
                            objNomination.CreatedBy = loggedInUserID;
                            objNomination.ModifiedBy = loggedInUserID;
                            objNomination.DHSubmittedDate = DateTime.Now.ToString("ddMMyyyy");
                            objNomination.DHSignature = loggedInUserID.ToString();
                        }
                        objSOMEntities.SaveChanges();
                    }
                }

                AuditLogModel objAuditLog = new AuditLogModel();
                configRepo = new ConfigurationRepo();
    
                if (nominationStatus == 2008) //Reject
                {
                    objAuditLog.CurrentStatus = NominationStatus.HoD_Reject;
                    objAuditLog.Comments = model.AuditLogComments;
                    _message = "Nomination Rejected Successfully";

                    bool _result =  configRepo.SendEmailUsingSOM_HoD_Reject_Nominee(objNomination.NominationId, loggedInUserID);
                    if (_result)
                    {
                        _message = "Nomination Rejected Successfully and Send Mail to Nominee";
                    }
                    else
                    {
                        _message = "Nomination Rejected Successfully. Unable to Send Mail to Nominee";
                    }
                }
                else if (nominationStatus == 2003) //Reassign
                {
                    objAuditLog.CurrentStatus = NominationStatus.HoD_Assign_EmployeeClarification;
                    objAuditLog.Comments = model.AuditLogComments;
                    _message = "Nomination Reassigned Successfully";
                 
                    bool result1 = configRepo.SendEmailUsingSOM_HOD_ReAssign_Nominee(objNomination.NominationId, loggedInUserID);
                    if (result1)
                    {
                        _message = "Nomination Reassigned Successfully and Send Mail to HoD";
                    }
                    else
                    {
                        _message = "Nomination Reassigned Successfully but unable to Send Mail to HoD";
                    }
                }
                else if (nominationStatus == 2004) //Assign to TQC
                {
                    objAuditLog.CurrentStatus = NominationStatus.HoD_Assign_TQC;
                    objAuditLog.Comments = model.AuditLogComments;
                    _message = "Nomination details assigned to TQC Successfully";
                    TQCHead _TQCHead;
                    using (objSOMEntities = new SOMEntities())
                    {
                        _TQCHead = objSOMEntities.TQCHeads.Where(r => r.IsActive == true).FirstOrDefault();
                    }
                    objAuditLog.TQCHeadID = _TQCHead.EmployeeNumber;
                }
                long HODId = GetReportingIDByEmpID(objNomination.EmployeeNumber);
                objAuditLog.CreatedBy = loggedInUserID;
                objAuditLog.ModifiedBy = loggedInUserID;
                objAuditLog.DepartmentHeadID = HODId.ToString();
                objAuditLog.EmployeeNumber = objNomination.EmployeeNumber;
                objAuditLog.IsNewAlert = true;
                objAuditLog.IsNotification = true;
                objAuditLog.NominationID = objNomination.NominationId;
                AddEntryIntoAuditLog(objAuditLog);

                bool result = configRepo.SendEmailUsingSOM_HOD_Assign_TQC(objNomination.NominationId, loggedInUserID);
                if (result)
                {
                    _message = _message + " and Send Mail to TQC";
                }
                else
                {
                    _message = _message + ". Unable to Send Mail to TQC";
                }

                //if (nominationStatus == (int)NominationStatus.HoD_Assign_TQC)
                //{
                //    AddEntryIntoCurrentMonthEvalator(model, loggedInUserID);
                //}

                baseModel = new RepositoryResponse { success = true, message = _message };
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public RepositoryResponse LoadNomination(string loggedInUserID,  string dept, string FDate, string TDate, bool isRejected)
        {
            baseModel = new RepositoryResponse();
            try
            {
               
                string F_subMonth = string.Empty;
                string F_subYear = string.Empty;

                string TO_subMonth = string.Empty;
                string TO_subYear = string.Empty;

                int _empSomRole = 0;
                string _dept = string.Empty;
                LoginRepo = new LoginRepo();
                var _emp = LoginRepo.GetUserDetailsByUserID(loggedInUserID.ToString());
                if (_emp != null && _emp.success == true)
                {
                    EmpMasterModel _empModel = _emp.Data;
                    _empSomRole = _empModel.EmployeeSOMRole;
                    _dept = _empModel.Department;
                }
                 

                if (!string.IsNullOrEmpty(TDate))
                {
                    string[] sDate = TDate.Split('-');
                    TO_subMonth = sDate[0];
                    TO_subYear = sDate[1];
                }

                if (!string.IsNullOrEmpty(FDate))
                {
                    string[] sDate = FDate.Split('-');
                    F_subMonth = sDate[0];
                    F_subYear = sDate[1];
                }

                List<NominationModel> _lstNominations = new List<NominationModel>();
                using (IntranetPortalEntities objIPEntities = new IntranetPortalEntities())
                using (SOMEntities objSOMEntities = new SOMEntities())
                {
                    var lstNominationLoad = (from a in objIPEntities.EmpMasters.AsEnumerable()
                                         join b in objSOMEntities.Nominations.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                                         where b.IsActive == true
                                         select new
                                         {
                                             a.EmployeeNumber,
                                             a.EmployeeName,
                                             a.Department,
                                             b.StartDate,
                                             b.EndDate,
                                             b.SubmittedMonth,
                                             b.SubmittedYear,
                                             b.ID,
                                             b.Status,
                                             b.CreatedDate,
                                             b.CreatedBy,
                                             a.ReportingPersonId,
                                             b.NominationId,
                                             b.Cost,
                                             b.Time,
                                             b.Paper,
                                             b.IsActive//,
                                             //filterDate = "01/" + b.SubmittedMonth + "/" + b.SubmittedYear,
                                         }).OrderByDescending(r => r.ID).ToList();

                    List<NominationModel> lstNominations = new List<NominationModel>();

                    foreach (var item in lstNominationLoad)
                    {
                        var auditLog = objSOMEntities.AuditLogs.Where(r => r.NominationID == item.NominationId).OrderByDescending(r => r.ID).FirstOrDefault();

                        NominationModel nomination = new NominationModel();
                        if (auditLog != null)
                        {
                            var person = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == auditLog.CreatedBy).FirstOrDefault();
                            if (person != null)
                            {
                                nomination.currentHoldingPerson = person.EmployeeName;
                            }
                            nomination.Status = (NominationStatus)auditLog.CurrentStatus;
                            nomination.StatusText = EnumHelper.GetDescription((NominationStatus)auditLog.CurrentStatus);
                        }
                        else
                        {
                            var person = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == loggedInUserID).FirstOrDefault();
                            if (person != null)
                            {
                                nomination.currentHoldingPerson = person.EmployeeName;
                            }
                            nomination.Status = NominationStatus.Employee_Save;
                            nomination.StatusText = EnumHelper.GetDescription(NominationStatus.Employee_Save);
                        }
                        nomination.NominationID = item.NominationId;
                        nomination.ID = item.ID;
                        nomination.EmployeeNumber = item.EmployeeNumber;
                        nomination.Name = item.EmployeeName;
                        nomination.Department = item.Department;
                        nomination.StartDate_ForGrid = Assistant.SOMDbToUIDateConversion(item.StartDate);
                        nomination.EndDate_ForGrid = Assistant.SOMDbToUIDateConversion(item.EndDate);
                        if (!string.IsNullOrEmpty(item.CreatedDate))
                        {
                            nomination.CreatedDate_ForGrid = Assistant.SOMDbToUIDateConversion_New(item.CreatedDate);
                        }
                        nomination.Cost = item.Cost;
                        Evaluation objEvaluation = new Evaluation();
                        objEvaluation = objSOMEntities.Evaluations.Where(r => r.NominationID == item.NominationId).FirstOrDefault();
                        if (objEvaluation == null)
                        {
                            nomination.IsEvalatorAssigned = false;
                        }
                        else
                        {
                            nomination.IsEvalatorAssigned = true;
                        }
                        //nomination.CreatedDateForFilter = Assistant.SOMDbToDateTimeForFilter(item.CreatedDate);
                        nomination.CreatedDateForFilterAsDateTime = Assistant.SOMDbToDateTimePicker(item.CreatedDate.Substring(0,8));
                        nomination.Status_int = (int)item.Status;
                        nomination.IsActive = (bool)item.IsActive;
                        nomination.CreatedBy = item.CreatedBy;
                        lstNominations.Add(nomination);
                    }

                    // if Nomination user
                    if (_empSomRole == (int)SOMEmpRole.Nomination)
                    {
                        lstNominations = lstNominations.Where(r => r.EmployeeNumber == loggedInUserID && r.IsActive == true).ToList();
                    }
                    else 
                    {
                        lstNominations = lstNominations.Where(r => r.Status_int != (int)NominationStatus.Employee_Save).ToList();
                    }

                    //if (_empSomRole == (int)SOMEmpRole.Admin)
                    //{
                    //    //var _lstNomination = lstNomination.Where(r => r.EmployeeNumber == loggedInUserID).ToList();
                    //    //lstNomination = lstNomination.Where(r => r.Status != (int)NominationStatus.Employee_Save && r.EmployeeNumber == loggedInUserID).ToList();
                    //}
                    //else if (_empSomRole == (int)SOMEmpRole.TQCHead)
                    //{
                    //    var Codes = new[] { (int)NominationStatus.Employee_Save,
                    //                    (int)NominationStatus.Employee_Assign_HOD,
                    //                    (int)NominationStatus.Employee_ReAssign_HoD,
                    //                    (int)NominationStatus.HoD_Reject,
                    //                    (int)NominationStatus.AdminReopen,
                    //                    (int)NominationStatus.HoD_Assign_EmployeeClarification };

                    //    lstNominations = lstNominations.Where(r => r.IsActive == true && !Codes.Contains((int)r.Status)).ToList();
                    //}
                    //else if (_empSomRole == (int)SOMEmpRole.Evaluation)
                    //{
                    //    var Codes = new[] { (int)NominationStatus.Employee_Save,
                    //                    (int)NominationStatus.Employee_Assign_HOD,
                    //                    (int)NominationStatus.Employee_ReAssign_HoD,
                    //                    (int)NominationStatus.HoD_Reject,
                    //                    (int)NominationStatus.AdminReopen,
                    //                    (int)NominationStatus.HoD_Assign_EmployeeClarification,
                    //                    (int)NominationStatus.HoD_Assign_TQC};

                    //    lstNominations = lstNominations.Where(r => r.IsActive == true && !Codes.Contains((int)r.Status)).ToList();

                    //    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    //    {
                    //        dept = _dept;
                    //    }
                    //}
                    //else if (_empSomRole == (int)SOMEmpRole.DepartmentHead)
                    //{
                    //    lstNominations = lstNominations.Where(r => r.Status_int != (int)NominationStatus.Employee_Save).ToList();

                    //    if (string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    //    {
                    //        dept = _dept;
                    //    }
                    //}
                    //else if (_empSomRole == (int)SOMEmpRole.Nomination)
                    //{
                    //    lstNominations = lstNominations.Where(r => r.EmployeeNumber == loggedInUserID).ToList();
                    //}

                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        lstNominations = lstNominations.Where(r => r.Department == dept).ToList();
                    }
                    if (!string.IsNullOrEmpty(F_subMonth) && !string.IsNullOrEmpty(F_subYear) && !string.IsNullOrEmpty(TO_subMonth) && !string.IsNullOrEmpty(TO_subYear))
                    {
                        string sDate = "01" + F_subMonth + ""+ F_subYear;
                        string eDate = "30" + TO_subMonth + "" + TO_subYear;

                        DateTime sdt = Assistant.SOMDbToDateTimeForFilter(sDate, true);
                        DateTime edt = Assistant.SOMDbToDateTimeForFilter(eDate, false);

                        lstNominations = lstNominations.Where(r => r.IsActive == true
                                        && (r.CreatedDateForFilterAsDateTime.Date >= sdt.Date
                                        && r.CreatedDateForFilterAsDateTime.Date <= edt.Date)).ToList();
                    }

                    baseModel = new RepositoryResponse
                    {
                        
                        success = true,
                        message = "Get Nomination details Successfully",
                        Data = lstNominations.OrderByDescending(r => string.IsNullOrEmpty(r.Cost) ? 0 : Decimal.Parse(r.Cost))
                                .ThenBy(r => string.IsNullOrEmpty(r.Time) ? 0 : int.Parse(r.Time))
                                .ThenBy(r => string.IsNullOrEmpty(r.Paper) ? 0 : int.Parse(r.Paper))
                    };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            
            return baseModel;
        }

        public RepositoryResponse GetNominationByID(string loggedInUserID,long ID, string fPath)
        {
            baseModel = new RepositoryResponse();
            try
            {
                NominationModel model = new NominationModel();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var data = (from a in objIPEntities.EmpMasters.AsEnumerable()
                                join b in objSOMEntities.Nominations.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                                where b.ID == ID
                                select new { a, b }).FirstOrDefault();

                    //var data = objSOMEntities.Nominations.Where(r => r.ID == ID && r.IsActive == true).FirstOrDefault();
                    if (data != null)
                    {
                        var nomCurrentStatus = objSOMEntities.AuditLogs.Where(r => r.NominationID == data.b.NominationId).OrderByDescending(r => r.ID).Select(s => s.CurrentStatus).FirstOrDefault();

                        model.Acknowledgement = data.b.Acknowledgement;
                        model.Benefits = data.b.Benefits;
                        model.BriefDescription = data.b.BriefDescription;
                        model.Department = data.a.Department;
                        model.EmployeeNumber = data.a.EmployeeNumber;
                        model.sEndDate = Assistant.SOMDbToDateTimePickerAsString(data.b.EndDate);
                        model.ID = data.b.ID;
                        model.ProjectTitle = data.b.ProjectTitle;
                        model.Idea = data.b.Idea;
                        model.IsActive = (bool)data.b.IsActive;
                        model.Name = data.a.EmployeeName;
                        model.PrimaryObjective = data.b.PrimaryObjective;
                        model.PrimaryObjective = data.b.PrimaryObjective;
                        model.sStartDate = Assistant.SOMDbToDateTimePickerAsString(data.b.StartDate);
                        if (nomCurrentStatus != null)
                        {
                            model.Status = (NominationStatus)nomCurrentStatus;
                        }
                        //model.Status = (NominationStatus)data.b.Status;
                        model.NominationSubmittedDate = Assistant.SOMDbToDateTimePickerAsString(data.b.NomSubmittedDate);

                        if (!string.IsNullOrEmpty(data.b.NomSignature))
                        {
                            RepositoryResponse _baseModel = new RepositoryResponse();
                            LoginRepo = new LoginRepo();
                            _baseModel = LoginRepo.GetUserDetailsByUserID(data.b.NomSignature);
                            if (_baseModel.success)
                            {
                                EmpMasterModel empModel = _baseModel.Data;
                                if (empModel != null)
                                {
                                    model.NominationSignature = empModel.EmployeeName;
                                }
                            }
                        }

                        model.CommentsByDH = data.b.DHComments;
                        model.DHSubmittedDate = Assistant.SOMDbToDateTimePickerAsString(data.b.DHSubmittedDate);
                        if (!string.IsNullOrEmpty(data.b.DHSignature))
                        {
                            RepositoryResponse _baseModel = new RepositoryResponse();
                            LoginRepo = new LoginRepo();
                            _baseModel = LoginRepo.GetUserDetailsByUserID(data.b.DHSignature);
                            if (_baseModel.success)
                            {
                                EmpMasterModel empModel = _baseModel.Data;
                                if (empModel != null)
                                {
                                    model.DHSignature = empModel.EmployeeName;
                                }
                            }
                        }


                        model.NominationID = data.b.NominationId;
                        if (!string.IsNullOrEmpty(data.b.Savings))
                        {
                            model.SavingsIds = data.b.Savings.Split(',').Select(Int32.Parse).ToArray();
                        }
                        model.Cost = data.b.Cost;
                        model.Time = data.b.Time;
                        model.Paper = data.b.Paper;
                        model.UploadedFileNames = data.b.UploadedFileNames;

                        fPath = fPath.Replace("nominationID", data.b.NominationId);
                        if (!string.IsNullOrEmpty(data.b.UploadedFileNames))
                        {
                            string[] splt = data.b.UploadedFileNames.Split(';');
                            List<fileProperty> lstString = new List<fileProperty>();
                            foreach (var item in splt)
                            {
                                fileProperty mod = new fileProperty();
                                mod.FileName = item;
                                mod.NominationID = data.b.NominationId;
                                lstString.Add(mod);
                            }
                            model.uploadFilelst = lstString;
                        }

                        List<HorizontaldeploymentModel> _lstHD = new List<HorizontaldeploymentModel>();
                        _lstHD = LoadHorinzontalDepolymentByNominationID(data.b.NominationId);
                        model.lstHD = _lstHD;

                        List<AuditLogModel> _lstAuditLog = new List<AuditLogModel>();
                        _lstAuditLog = GetAuditLogByNominationID(model.NominationID);
                        model.lstAuditLogs = _lstAuditLog;
                        model.ReportingPersonID = data.a.ReportingPersonId.ToString();

                        model.CommentsForSOM = data.b.SOMComments;
                        model.SOMSubmittedDate = data.b.SOMSubmittedDate;
                        if (!string.IsNullOrEmpty(data.b.SOMSignature))
                        {
                            RepositoryResponse _baseModel = new RepositoryResponse();
                            LoginRepo = new LoginRepo();
                            _baseModel = LoginRepo.GetUserDetailsByUserID(data.b.SOMSignature);
                            if (_baseModel.success)
                            {
                                EmpMasterModel empModel = _baseModel.Data;
                                if (empModel != null)
                                {
                                    model.SOMSignature = empModel.EmployeeName;
                                }
                            }
                        }

                        baseModel = new RepositoryResponse { success = true, message = "Get Nomination details Successfully", Data = model };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = false, message = "Unable to get Nomination details", Data = model };
                    }

                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            
            return baseModel;
        }

        public RepositoryResponse GetNominationByEmplyoeeNumber(string loggedInUserID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                NominationModel model = new NominationModel();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var data = (from a in objIPEntities.EmpMasters.AsEnumerable()
                                join b in objSOMEntities.Nominations.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                                where b.EmployeeNumber == loggedInUserID.ToString() &&
                                b.IsActive == true && b.Status == (int)NominationStatus.Employee_Save
                                select new { a, b }).OrderByDescending(r => r.b.ID).FirstOrDefault();

                    //var data = objSOMEntities.Nominations.Where(r => r.ID == ID && r.IsActive == true).FirstOrDefault();
                    if (data != null)
                    {


                        model.Acknowledgement = data.b.Acknowledgement;
                        //model.AreaOfInterest_1 = data.b.AreaOfInterest_1;
                        //model.AreaOfInterest_2 = data.b.AreaOfInterest_2;
                        //model.AreaOfInterest_3 = data.b.AreaOfInterest_3;
                        model.Benefits = data.b.Benefits;
                        model.BriefDescription = data.b.BriefDescription;
                        model.Department = data.a.Department;
                        model.EmployeeNumber = data.a.EmployeeNumber;
                        //model.StartDate = Assistant.stringToDatePickerUI(data.b.StartDate);
                        //model.EndDate = Assistant.stringToDatePickerUI(data.b.EndDate);

                        model.sStartDate = Assistant.SOMDbToDateTimePickerAsString(data.b.StartDate);
                        model.sEndDate = Assistant.SOMDbToDateTimePickerAsString(data.b.EndDate);

                        model.ID = data.b.ID;
                        model.Idea = data.b.Idea;
                        model.NominationID = data.b.NominationId;
                        //model.ImplementationStatus_1 = data.b.ImplementationStatus_1;
                        //model.ImplementationStatus_2 = data.b.ImplementationStatus_2;
                        //model.ImplementationStatus_3 = data.b.ImplementationStatus_2;
                        model.IsActive = (bool)data.b.IsActive;
                        model.Name = data.a.EmployeeName;
                        model.PrimaryObjective = data.b.PrimaryObjective;

                        model.Status = (NominationStatus)data.b.Status;
                        List<HorizontaldeploymentModel> _lstHD = new List<HorizontaldeploymentModel>();
                        _lstHD = LoadHorinzontalDepolymentByNominationID(data.b.NominationId);
                        model.lstHD = _lstHD;

                        baseModel = new RepositoryResponse { success = true, message = "Get Nomination details Successfully", Data = model };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = false, message = "No Nomination details available", Data = model };
                    }

                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }

        public void AddEntryIntoAuditLog(AuditLogModel auditLog)
        {
            using (objSOMEntities = new SOMEntities())
            {
                var auditLoglst = objSOMEntities.AuditLogs.Where(r => r.NominationID == auditLog.NominationID).ToList();
                foreach (var item in auditLoglst)
                {
                    item.IsNewAlert = false;
                    item.ModifiedBy = auditLog.CreatedBy;
                    objSOMEntities.SaveChanges();
                }
            }
            if (auditLog.CurrentStatus == NominationStatus.TQC_Declare_SOM)
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var auditLoglst = objSOMEntities.AuditLogs.Where(r => r.CurrentStatus == (int)NominationStatus.Evaluators_Assign_TQC).ToList();
                    foreach (var item in auditLoglst)
                    {
                        item.IsNewAlert = false;
                        item.ModifiedBy = auditLog.CreatedBy;
                        objSOMEntities.SaveChanges();
                    }
                    var _auditLoglst = objSOMEntities.AuditLogs.Where(r => r.CurrentStatus == (int)NominationStatus.TQC_Declare_SOM).ToList();
                    foreach (var item in _auditLoglst)
                    {
                        item.IsNewAlert = false;
                        item.ModifiedBy = auditLog.CreatedBy;
                        objSOMEntities.SaveChanges();
                    }
                }
            }
            //long rptPersonID = GetReportingIDByEmpID(auditLog.EmployeeNumber);
            using (objSOMEntities = new SOMEntities())
            {
                if (auditLog != null)
                {
                    var dbModel = objSOMEntities.AuditLogs.Where(r => r.NominationID == auditLog.NominationID && r.CurrentStatus == (int)auditLog.CurrentStatus).FirstOrDefault();
                    if (dbModel == null)
                    {
                        dbModel = new AuditLog();
                        dbModel.CurrentStatus = (int)auditLog.CurrentStatus;
                        dbModel.EmployeeNumber = auditLog.EmployeeNumber;
                        dbModel.IsNewAlert = auditLog.IsNewAlert;
                        dbModel.IsNotification = auditLog.IsNotification;
                        dbModel.NominationID = auditLog.NominationID;
                        dbModel.CreatedBy = auditLog.CreatedBy;
                        dbModel.DepartmentHeadID = auditLog.DepartmentHeadID;
                        dbModel.TQCHeadID = auditLog.TQCHeadID;
                        dbModel.IsActive = true;
                        dbModel.EvaluatorID = auditLog.EvaluatorID;
                        dbModel.Comments = auditLog.Comments;
                        //dbModel.CreatedDate = Assistant.FormatedDateTime().Substring(0, 9);
                        objSOMEntities.AuditLogs.Add(dbModel);
                        objSOMEntities.SaveChanges();
                    }
                    else
                    {
                        dbModel.IsNewAlert = auditLog.IsNewAlert;
                        dbModel.IsNotification = auditLog.IsNotification;
                        dbModel.IsActive = true;
                        dbModel.ModifiedBy = auditLog.CreatedBy;
                        dbModel.Comments = auditLog.Comments;
                        objSOMEntities.SaveChanges();
                    }
                        
                    //var _user = objSOMEntities.EmpMasters.Where(r => r.EmpID == auditLog.EmployeeID).FirstOrDefault();
                    //var _nomination = objSOMEntities.Nominations.Where(r => r.ID == auditLog.NominationID).FirstOrDefault();

                    //AuditLog dbModel = new AuditLog();
                    ////dbModel = objSOMEntities.AuditLogs.Where(r => r.NominationID == auditLog.NominationID
                    ////&& r.CurrentStatus  == (int)auditLog.CurrentStatus
                    ////&& r.IsActive == true).FirstOrDefault();
                    ////if (dbModel == null)
                    ////{
                    //    //dbModel = new AuditLog();
                    //    dbModel.CurrentStatus = (int)auditLog.CurrentStatus;
                    //    dbModel.EmployeeNumber = auditLog.EmployeeNumber;
                    //    dbModel.IsNewAlert = auditLog.IsNewAlert;
                    //    dbModel.IsNotification = auditLog.IsNotification;
                    //    dbModel.NominationID = auditLog.NominationID;
                    //    dbModel.CreatedBy = auditLog.CreatedBy;
                    //    dbModel.DepartmentHeadID = auditLog.DepartmentHeadID;
                    //    dbModel.TQCHeadID = auditLog.TQCHeadID;
                    //    dbModel.IsActive = true;
                    //    dbModel.EvaluatorID = auditLog.EvaluatorID;
                    //    //dbModel.CreatedDate = Assistant.FormatedDateTime().Substring(0, 9);
                    //    objSOMEntities.AuditLogs.Add(dbModel);
                    //    objSOMEntities.SaveChanges();
                    ////}
                    //else
                    //{
                    //    dbModel.IsActive = true;
                    //    dbModel.DepartmentHeadID = rptPersonID.ToString();
                    //    dbModel.CurrentStatus = (int)auditLog.CurrentStatus;
                    //    dbModel.EmployeeNumber = auditLog.EmployeeNumber;
                    //    dbModel.IsNewAlert = auditLog.IsNewAlert;
                    //    dbModel.IsNotification = auditLog.IsNotification;
                    //    dbModel.NominationID = auditLog.NominationID;
                    //    dbModel.EvaluatorID = auditLog.EvaluatorID;
                    //    dbModel.ModifiedBy = auditLog.CreatedBy;
                    //    //dbModel.ModifiedDate = Assistant.FormatedDateTime().Substring(0, 9);
                    //    objSOMEntities.SaveChanges();
                    //}
                }
            }
        }

        public RepositoryResponse GetReportDetails(string loggedInUserID, string dept, string Fdate, string Tdate)
        {
            baseModel = new RepositoryResponse();
            try
            {
                string F_subMonth = string.Empty;
                string F_subYear = string.Empty;
                string T_subMonth = string.Empty;
                string T_subYear = string.Empty;

                if (!string.IsNullOrEmpty(Fdate))
                {
                    string[] sDate = Fdate.Split('-');
                    F_subMonth = sDate[0];
                    F_subYear = sDate[1];
                }

                if (!string.IsNullOrEmpty(Tdate))
                {
                    string[] sDate = Tdate.Split('-');
                    T_subMonth = sDate[0];
                    T_subYear = sDate[1];
                }

                ParticipatedCount model = new ParticipatedCount();
                //Get Total Participate Count
                string[] valu;            
                List<string> empNumber = new List<string>();
                using (objSOMEntities = new SOMEntities())
                {  
                    var nomGrades = objSOMEntities.Configurations.Where(r => r.Module == "SOM"
                                    && r.Type == "NominationUserGrades" && r.IsActive == true).Select(r => r.Value).FirstOrDefault();
                    valu = nomGrades.Split(',');
                }

                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    List<EmpMaster> lstEmpMaster = new List<EmpMaster>();
                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        lstEmpMaster = objIPEntities.EmpMasters.Where(r => r.IsActive == true
                                    && !string.IsNullOrEmpty(r.Grade)
                                    && r.Department == dept).ToList();
                    }
                    else
                    {
                        lstEmpMaster = objIPEntities.EmpMasters.Where(r => r.IsActive == true
                                && !string.IsNullOrEmpty(r.Grade)).ToList();
                    }
                    foreach (var item in lstEmpMaster)
                    {
                        if (valu.Contains(item.Grade))
                        {
                            empNumber.Add(item.EmployeeNumber);
                        }
                    }
                }

                model.TotalEmpCount = empNumber.Count;

                //To Get Participate Count
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var lstEmpMaster = (from a in empNumber.AsEnumerable()
                                        join b in objIPEntities.EmpMasters.AsEnumerable() on a equals b.EmployeeNumber
                                        join c in objSOMEntities.Nominations.AsEnumerable() on b.EmployeeNumber equals c.EmployeeNumber
                                        select new { 
                                            c.CreatedDate, c.IsActive, c.EmployeeNumber, b.Department }).ToList();

                    List<NominationModel> lstModel = new List<NominationModel>();
                    foreach (var item in lstEmpMaster)
                    {
                        NominationModel mod = new NominationModel();
                        mod.CreatedDateForFilterAsDateTime = Assistant.SOMDbToDateTimePicker(item.CreatedDate.Substring(0, 8));
                        mod.IsActive = (bool)item.IsActive;
                        mod.EmployeeNumber = item.EmployeeNumber;
                        mod.Department = item.Department;
                        lstModel.Add(mod);
                    }
                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        lstModel = lstModel.Where(r => r.IsActive == true
                                            && r.Department == dept).ToList();
                    }

                    if (!string.IsNullOrEmpty(F_subMonth) && !string.IsNullOrEmpty(F_subYear) && !string.IsNullOrEmpty(T_subMonth) && !string.IsNullOrEmpty(T_subYear))
                    {
                        string sDate = "01" + F_subMonth + "" + F_subYear;
                        string eDate = "30" + T_subMonth + "" + T_subYear;

                        DateTime sdt = Assistant.SOMDbToDateTimeForFilter(sDate, true);
                        DateTime edt = Assistant.SOMDbToDateTimeForFilter(eDate, false);

                        lstModel = lstModel.Where(r => r.IsActive == true
                                        && (r.CreatedDateForFilterAsDateTime.Date >= sdt.Date
                                        && r.CreatedDateForFilterAsDateTime.Date <= edt.Date)).ToList();
                    }

                    var _data = lstModel.Where(r => r.IsActive == true).Select(s => s.EmployeeNumber).Distinct();


                    var dat = lstModel.GroupBy(l => l.EmployeeNumber).Select(g => new { totCount = g.Select(l => l.EmployeeNumber).Distinct().Count() });

                    model.ParticipatedEmpCount = _data.Count();
                }

                baseModel = new RepositoryResponse { success = true, message= "Get report details Successfully...", Data = model };
               
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;

        }

        public int GetReportingIDByEmpID(string EmployeeNumber)
        {
            int rptPersonId = 0;
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var usr = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == EmployeeNumber).FirstOrDefault();
                rptPersonId = (int)usr.ReportingPersonId;
                //if (rptPersonId > 0)
                //{
                //    var rptPerson = objSOMEntities.EmpMasters.Where(r => r.EmpID == rptPersonId).FirstOrDefault();
                //}
            }
            return rptPersonId;
        }

        public RepositoryResponse NominationRestrictionCount(string loggedInUserID)
        {
            
            baseModel = new RepositoryResponse();
            try
            {
                if (string.IsNullOrEmpty(loggedInUserID))
                {
                    baseModel = new RepositoryResponse { success = true, message = "Nomination allowed Submit the Form", Data = true };
                }
                else
                {
                    string message = string.Empty;

                    int NomCount = 0; int configCount = 0;
                    using (objSOMEntities = new SOMEntities())
                    {
                        string month = Assistant.GetMonthFromCurrentDate();
                        string year = Assistant.GetYearFromCurrentDate();
                        var data = objSOMEntities.Nominations.Where(r => r.EmployeeNumber == loggedInUserID
                                    && r.IsActive == true && r.SubmittedMonth == month
                                    && r.SubmittedYear == year).ToList();

                        NomCount = data.Count;
                    }

                    using (objSOMEntities = new SOMEntities())
                    {
                        var _data = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationSubmissionCount"
                                    && r.IsActive == true).FirstOrDefault();
                        if (_data != null)
                        {
                            configCount = int.Parse(_data.Value);
                        }
                        if (NomCount >= configCount)
                        {
                            message = "Nomination doesn't allow to submit. Nomination Count is exceeded.";
                        }
                    }
                    
                    using (objSOMEntities = new SOMEntities())
                    {
                        var _date = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationDeadLineDate"
                                    && r.IsActive == true).FirstOrDefault();
                        if (_date != null)
                        {
                            configCount = int.Parse(_date.Value); //25 
                            int date = int.Parse(DateTime.Now.ToString("dd")); //28
                            if (configCount <= date)
                            {
                                if (string.IsNullOrEmpty(message))
                                {
                                    message = "Nomination doesn't allow to submit. Nomination Submit Date is exceeded.";
                                }
                                else
                                {
                                    message = "Nomination doesn't allow to submit. Nomination Ccount, Submit Date is exceeded.";
                                }
                                
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(message))
                    {
                        baseModel = new RepositoryResponse { success = true, message = "Nomination allowed Submit the Form", Data = true };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = false, message = message, Data = false };
                    }
                }
                
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public RepositoryResponse LoadNominationForDH(string loggedInUserID, string dept, string date, string status)
        {
            baseModel = new RepositoryResponse();
            try
            {
                string subMonth = string.Empty;
                string subYear = string.Empty;

                if (!string.IsNullOrEmpty(date))
                {
                    string[] sDate = date.Split('-');
                    subMonth = sDate[0];
                    subYear = sDate[1];
                }

                List<NominationModel> _lstNominations = new List<NominationModel>();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var lstNomination = (from a in objIPEntities.EmpMasters.AsEnumerable()
                                         join b in objSOMEntities.Nominations.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                                         where b.IsActive == true
                                         select new
                                         {
                                             a.EmployeeNumber,
                                             a.EmployeeName,
                                             a.Department,
                                             b.StartDate,
                                             b.EndDate,
                                             b.SubmittedMonth,
                                             b.SubmittedYear,
                                             b.ID,
                                             b.Status,
                                             b.CreatedDate,
                                             b.NominationId,
                                             b.Cost,
                                             a.ReportingPersonId
                                         }).OrderByDescending(r => r.ID).ToList();

                    //if (userRole == "DepartmentHead")
                    //{
                    //    lstNomination = lstNomination.Where(r => r.Status == (int)NominationStatus.Employee_Assign_HOD).ToList();
                    //}

                    //if (!string.IsNullOrEmpty(dept))
                    //{
                    //    lstNomination = lstNomination.Where(r => r.Department == dept).ToList();
                    //}
                    //if (!string.IsNullOrEmpty(subMonth) && !string.IsNullOrEmpty(subYear))
                    //{
                    //    lstNomination = lstNomination.Where(r => r.SubmittedMonth == subMonth && r.SubmittedYear == subYear).ToList();
                    //}
                    if (status == "pending")
                    {
                        var Codes = new[] { (int)NominationStatus.Employee_ReAssign_HoD,
                                        (int)NominationStatus.Employee_Assign_HOD};

                        //var nomUsrCount = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID
                        //                    && r.IsNewAlert == true
                        //                    && Codes.Contains((int)r.CurrentStatus)).ToList();

                        lstNomination = lstNomination.Where(r => r.ReportingPersonId == int.Parse(loggedInUserID)
                                        && Codes.Contains((int)r.Status)).ToList();
                    }
                    //else if (status == "approved")
                    //{
                    //    lstNomination = lstNomination.Where(r => r.Status == (int)NominationStatus.HoD_Assign_TQC).ToList();
                    //}
                    int i = 1;
                    foreach (var item in lstNomination)
                    {

                        NominationModel nomination = new NominationModel();
                        nomination.ID = item.ID;
                        nomination.SlNo = i;
                        nomination.NominationID = item.NominationId;
                        nomination.EmployeeNumber = item.EmployeeNumber;
                        nomination.Name = item.EmployeeName;
                        nomination.Department = item.Department;
                        nomination.StartDate_ForGrid = Assistant.SOMDbToUIDateConversion(item.StartDate);
                        nomination.EndDate_ForGrid = Assistant.SOMDbToUIDateConversion(item.EndDate);
                        nomination.CreatedDate_ForGrid = Assistant.SOMDbToUIDateConversion_New(item.CreatedDate);
                        nomination.Cost = item.Cost;
                        //nomination.StartDate_ForGrid = Assistant.DateConversion(item.StartDate);
                        //nomination.EndDate_ForGrid = Assistant.DateConversion(item.EndDate);
                        _lstNominations.Add(nomination);
                        i++;
                    }

                    baseModel = new RepositoryResponse
                    {
                        success = true,
                        message = "Get Nomination details Successfully",
                        Data = _lstNominations.OrderByDescending(r => string.IsNullOrEmpty(r.Cost) ? 0 : Decimal.Parse(r.Cost))
                               .ThenBy(r => string.IsNullOrEmpty(r.Time) ? 0 : int.Parse(r.Time))
                               .ThenBy(r => string.IsNullOrEmpty(r.Paper) ? 0 : int.Parse(r.Paper))
                    };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }    

        public RepositoryResponse GetAuditLogByNominationID_new(string nominationID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var auditLogs = objSOMEntities.AuditLogs.Where(r => r.NominationID == nominationID && r.IsActive == true).ToList();
                    List<AuditLogModel> lstAuditLog = new List<AuditLogModel>();
                    if (auditLogs != null)
                    {
                        int i = 1;
                        foreach (var item in auditLogs)
                        {
                            AuditLogModel model = new AuditLogModel();
                            model.SlNo = i;
                            if (!string.IsNullOrEmpty(item.CreatedDate))
                            {
                                model.CreatedDate = item.CreatedDate;
                            }
                            model.EmployeeNumber = item.EmployeeNumber;
                            model.CurrentStatus = (NominationStatus)item.CurrentStatus;
                            model.DepartmentHeadID = item.DepartmentHeadID;
                            model.EvaluatorID = item.EvaluatorID;
                            model.NominationID = item.NominationID;
                            model.TQCHeadID = item.TQCHeadID;

                            LoginRepo = new LoginRepo();
                            if (!string.IsNullOrEmpty(model.DepartmentHeadID))
                            {
                                var _emp = LoginRepo.GetUserDetailsByUserID(model.DepartmentHeadID);
                                EmpMasterModel empModel = new EmpMasterModel();
                                if (_emp != null && _emp.success == true)
                                {
                                    empModel = _emp.Data;
                                    model.DepartmentHeadName = empModel.EmployeeName;
                                }
                            }

                            if (!string.IsNullOrEmpty(model.EvaluatorID))
                            {
                                var _emp = LoginRepo.GetUserDetailsByUserID(model.EvaluatorID);
                                EmpMasterModel empModel = new EmpMasterModel();
                                if (_emp != null && _emp.success == true)
                                {
                                    empModel = _emp.Data;
                                    model.EvaluatorName = empModel.EmployeeName;
                                }
                            }

                            if (!string.IsNullOrEmpty(model.TQCHeadID))
                            {
                                var _emp = LoginRepo.GetUserDetailsByUserID(model.TQCHeadID);
                                EmpMasterModel empModel = new EmpMasterModel();
                                if (_emp != null && _emp.success == true)
                                {
                                    empModel = _emp.Data;
                                    model.TQCHeadName = empModel.EmployeeName;
                                }
                            }
                            model.InfoMessage = GetNominationStringById((int)item.CurrentStatus);
                            lstAuditLog.Add(model);
                            i = i + 1;
                        }

                        baseModel = new RepositoryResponse { success = true, message = "Get Audit log details Successfully", Data = lstAuditLog };
                    }

                }
                
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public List<AuditLogModel> GetAuditLogByNominationID(string nominationID)
        {
            List<AuditLogModel> lstAuditLog = new List<AuditLogModel>();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var auditLogs = objSOMEntities.AuditLogs.Where(r => r.NominationID == nominationID && r.IsActive == true).ToList();

                    if (auditLogs != null)
                    {
                        int i = 1;
                        foreach (var item in auditLogs)
                        {
                            AuditLogModel model = new AuditLogModel();
                            model.SlNo = i;
                            if (!string.IsNullOrEmpty(item.CreatedDate))
                            {
                                model.CreatedDate = Assistant.SOMDbToUIDateConversion_New(item.CreatedDate);
                            }
                          
                            model.EmployeeNumber = item.EmployeeNumber;
                            model.CurrentStatus = (NominationStatus)item.CurrentStatus;
                            model.DepartmentHeadID = item.DepartmentHeadID;
                            model.EvaluatorID = item.EvaluatorID;
                            model.NominationID = item.NominationID;
                            model.TQCHeadID = item.TQCHeadID;
                            model.Comments = item.Comments;

                            LoginRepo = new LoginRepo();
                            if (!string.IsNullOrEmpty(model.DepartmentHeadID))
                            {
                                var _emp = LoginRepo.GetUserDetailsByUserID(model.DepartmentHeadID);
                                EmpMasterModel empModel = new EmpMasterModel();
                                if (_emp != null && _emp.success == true)
                                {
                                    empModel = _emp.Data;
                                    model.DepartmentHeadName = empModel.EmployeeName;
                                }
                            }

                            if (!string.IsNullOrEmpty(model.EvaluatorID))
                            {
                                var _emp = LoginRepo.GetUserDetailsByUserID(model.EvaluatorID);
                                EmpMasterModel empModel = new EmpMasterModel();
                                if (_emp != null && _emp.success == true)
                                {
                                    empModel = _emp.Data;
                                    model.EvaluatorName = empModel.EmployeeName;
                                }
                            }

                            if (!string.IsNullOrEmpty(model.TQCHeadID))
                            {
                                var _emp = LoginRepo.GetUserDetailsByUserID(model.TQCHeadID);
                                EmpMasterModel empModel = new EmpMasterModel();
                                if (_emp != null && _emp.success == true)
                                {
                                    empModel = _emp.Data;
                                    model.TQCHeadName = empModel.EmployeeName;
                                }
                            }


                            string _message = string.Empty;
                            switch (item.CurrentStatus)
                            {
                                case 2001:
                                    _message = "Nomination Created";
                                    break;
                                case 2002:
                                    _message = "Nomination Form assigned to HoD";
                                    break;
                                case 2003:
                                    _message = "HoD assigned to Employee for Clarification";
                                    break;
                                case 2004:
                                    _message = "HoD assigned to TQC";
                                    break;
                                case 2005:
                                    _message = "TQC assigned to Evaluators";
                                    break;
                                case 2006:
                                    _message = "Evaluator added score for Nomination Form";
                                    break;
                                case 2007:
                                    _message = "Evaluator assigned TQC";
                                    break;
                                case 2008:
                                    _message = "HoD rejected Nomination Form";
                                    break;
                                case 2009:
                                    _message = "TQC rejected Nomination Form";
                                    break;
                                case 2010:
                                    _message = "Evaluator rejected Nomination Form";
                                    break;
                                case 2011:
                                    _message = "Completed";
                                    break;
                                case 2012:
                                    _message = "Admin has Re-opened Nomination Form";
                                    break;
                                case 2013:
                                    _message = "Employee Reassigned HoD";
                                    break;
                                case 2014:
                                    _message = "TQC declared SOM";
                                    break;
                                default:
                                    break;
                            }

                            model.InfoMessage = _message;
                            lstAuditLog.Add(model);
                            i = i + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            return lstAuditLog;
        }


        public void AddEntryIntoCurrentMonthEvalator(NominationModel nomModel, string loggedInUserID)
        {
            panelMembersRepo = new PanelMembersRepo();
            baseModel = new RepositoryResponse();
            baseModel = panelMembersRepo.LoadPanelMembers();
            if (baseModel.success)
            {
                List<PanelMembersModel> lstpanelMembersModel = new List<PanelMembersModel>();
                lstpanelMembersModel = baseModel.Data;

                foreach (var item in lstpanelMembersModel)
                {
                    using (objSOMEntities = new SOMEntities())
                    {
                        var _evalDbModel = objSOMEntities.Evaluations.Where(r => r.EvaluatorID == item.EvaluatorID && r.NominationID == nomModel.NominationID).FirstOrDefault();
                        if (_evalDbModel == null)
                        {
                            Evaluation dbModel = new Evaluation();
                            dbModel.EvaluatorID = item.EvaluatorID;
                            dbModel.EmployeeNumber = nomModel.EmployeeNumber;
                            dbModel.IsActive = true;
                            dbModel.NominationID = nomModel.NominationID;
                            dbModel.Status = (int)NominationStatus.TQC_Assign_Evaluator;
                            dbModel.CreatedBy = loggedInUserID;

                            objSOMEntities.Evaluations.Add(dbModel);
                            objSOMEntities.SaveChanges();
                        }
                        else
                        {
                            _evalDbModel.EmployeeNumber = nomModel.EmployeeNumber;
                            _evalDbModel.IsActive = true;
                            _evalDbModel.NominationID = nomModel.NominationID;
                            _evalDbModel.Status = (int)NominationStatus.TQC_Assign_Evaluator;
                            _evalDbModel.ModifiedBy = loggedInUserID;
                            objSOMEntities.SaveChanges();
                        }
                    }
                }
            }
        }

        public void AddFileNameToNomination(string nominationID, string FileNames)
        {
            using (objSOMEntities = new SOMEntities())
            {
                var nom = objSOMEntities.Nominations.Where(r => r.NominationId == nominationID).FirstOrDefault();
                if (nom != null)
                {
                    if (string.IsNullOrEmpty(nom.UploadedFileNames))
                    {
                        nom.UploadedFileNames = FileNames;
                    }
                    else
                    {
                        nom.UploadedFileNames = nom.UploadedFileNames + ";"+ FileNames;
                    }
                    
                    objSOMEntities.SaveChanges();
                }
               
            }
        }

        public List<AutoCompleteBox> GetEvaltorDetailsForAutoCompleteBox(bool isRequiredDept)
        {
            List<AutoCompleteBox> lstAutoCompBox = new List<AutoCompleteBox>();

            string lstValue = string.Empty;
            string[] valu;
            using (objSOMEntities = new SOMEntities())
            {
                lstValue = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "EvaluationUserGrades" && r.IsActive == true)
                    .Select(r => r.Value).FirstOrDefault();
                valu = lstValue.Split(',');
            }

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var emp = objIPEntities.EmpMasters.Where(r => r.IsActive == true && !string.IsNullOrEmpty(r.Grade)).ToList();

                foreach (var item in emp)
                {
                    if (valu.Contains(item.Grade))
                    {
                        AutoCompleteBox objAutoCompleteBox = new AutoCompleteBox();
                        if (isRequiredDept)
                        {
                            objAutoCompleteBox.ID = item.EmployeeNumber + "###" + item.Department;
                        }
                        else
                        {
                            objAutoCompleteBox.ID = item.EmployeeNumber;
                        }
                        objAutoCompleteBox.Value = item.EmployeeName;
                        lstAutoCompBox.Add(objAutoCompleteBox);
                    }
                }
            }
            return lstAutoCompBox;
        }

        public RepositoryResponse AssignEvalatorToNominationByTQCHead(DashboardModel model, string loggedInUserID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                Nomination objNomination = new Nomination();
                using (objSOMEntities = new SOMEntities())
                {
                    objNomination = objSOMEntities.Nominations.Where(r => r.ID == model.ID).FirstOrDefault();
                    objNomination.Status = (int)NominationStatus.TQC_Assign_Evaluator;
                    objNomination.ModifiedBy = loggedInUserID;
                    objSOMEntities.SaveChanges();
                }

                if (objNomination != null)
                {
                    using (objSOMEntities = new SOMEntities())
                    {
                        var _evalDbModel = objSOMEntities.Evaluations.Where(r => r.NominationID == objNomination.NominationId).FirstOrDefault();
                        if (_evalDbModel == null)
                        {
                            Evaluation dbModel = new Evaluation();
                            dbModel.EvaluatorID = model.EvaluatorNumber;
                            dbModel.EmployeeNumber = objNomination.EmployeeNumber;
                            dbModel.IsActive = true;
                            dbModel.NominationID = objNomination.NominationId;
                            dbModel.Status = (int)NominationStatus.TQC_Assign_Evaluator;
                            dbModel.CreatedBy = loggedInUserID;

                            objSOMEntities.Evaluations.Add(dbModel);
                            objSOMEntities.SaveChanges();
                        }
                        else
                        {
                            _evalDbModel.EmployeeNumber = objNomination.EmployeeNumber;
                            _evalDbModel.IsActive = true;
                            _evalDbModel.NominationID = objNomination.NominationId;
                            _evalDbModel.Status = (int)NominationStatus.TQC_Assign_Evaluator;
                            _evalDbModel.ModifiedBy = loggedInUserID;
                            objSOMEntities.SaveChanges();
                        }
                        baseModel = new RepositoryResponse { success = true, message = "Evaluator Assigned to Nomination Successfully By TQC", Data = "" };
                    }

                    long HODId = GetReportingIDByEmpID(objNomination.EmployeeNumber);
                    AuditLogModel objAuditLog = new AuditLogModel();
                    objAuditLog.CurrentStatus = NominationStatus.TQC_Assign_Evaluator;
                    objAuditLog.EmployeeNumber = objNomination.EmployeeNumber;
                    objAuditLog.IsNewAlert = true;
                    objAuditLog.IsNotification = true;
                    objAuditLog.DepartmentHeadID = HODId.ToString();
                    objAuditLog.TQCHeadID = loggedInUserID;
                    objAuditLog.EvaluatorID = model.EvaluatorNumber;
                    objAuditLog.NominationID = objNomination.NominationId;
                    objAuditLog.CreatedBy = loggedInUserID;
                    objAuditLog.Comments = "";
                    AddEntryIntoAuditLog(objAuditLog);

                    configRepo = new ConfigurationRepo();
                    bool result = configRepo.SendEmailUsingSOM_TQC_Assign_Evaluator(objNomination.NominationId, loggedInUserID);
                    if (result)
                    {
                        baseModel = new RepositoryResponse { success = true, message = "Evaluator Assigned to Nomination Successfully By TQC and Send Mail to Evaluator", Data = "" };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = true, message = "Evaluator Assigned to Nomination Successfully By TQC. Unable to Send mail to Evaluator", Data = "" };
                    }
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = "Unable to Assign Evaluator to Nomination By TQC", Data = ex.ToString() };
            }
            return baseModel;
        }

        public string GetNomineeContentsForPDFExport(string content, string ID)
        {
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var data = (from a in objIPEntities.EmpMasters.AsEnumerable()
                            join b in objSOMEntities.Nominations.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                            where b.NominationId == ID
                            select new { a, b }).FirstOrDefault();

                List<HorizontaldeploymentModel> _lstHD = new List<HorizontaldeploymentModel>();
                _lstHD = LoadHorinzontalDepolymentByNominationID(data.b.NominationId);

                string horizontalDep = string.Empty;
                horizontalDep = "<tr> " +
                             "    <td align='center'><span>##AreaOfInterest1##</span></td> " +
                             "    <td align='center'><span>##ImplementationStatus1##</span></td> " +
                             "</tr>";
                string finalHD = string.Empty;
                if (_lstHD.Count > 0)
                {
                    foreach (var item in _lstHD)
                    {
                        if (string.IsNullOrEmpty(finalHD))
                        {
                            finalHD = horizontalDep.Replace("##AreaOfInterest1##", item.AreaOfInterest);
                            finalHD = horizontalDep.Replace("##ImplementationStatus1##", item.ImplementationStatus);
                        }
                        else
                        {
                            string _temp;
                            _temp = horizontalDep.Replace("##AreaOfInterest1##", item.AreaOfInterest);
                            _temp = horizontalDep.Replace("##ImplementationStatus1##", item.ImplementationStatus);
                            finalHD = finalHD + _temp;
                        }
                    }
                }
                else
                {
                    finalHD = "<tr><td></td><td></td></tr>";
                }
                


                content = content.Replace("##HorizontalDeployment##", finalHD);

                string fileUpload = "<tr> " +
                "    <td> " +
                "        <span> " +
                "            ##FileName1## " +
                "        </span> " +
                "    </td> " +
                "</tr> ";


                List<fileProperty> lstString = new List<fileProperty>();

                if (!string.IsNullOrEmpty(data.b.UploadedFileNames))
                {
                    string[] splt = data.b.UploadedFileNames.Split(';');
                   
                    foreach (var item in splt)
                    {
                        fileProperty mod = new fileProperty();
                        mod.FileName = item;
                        mod.NominationID = data.b.NominationId;
                        lstString.Add(mod);
                    }
                }

                string finalNames = string.Empty;
                if (lstString.Count >0)
                {
                    foreach (var item in lstString)
                    {
                        if (string.IsNullOrEmpty(finalHD))
                        {
                            finalNames = fileUpload.Replace(" ##FileName1##", item.FileName);

                        }
                        else
                        {
                            string _temp;
                            _temp = fileUpload.Replace(" ##FileName1##", item.FileName);
                            finalNames = finalNames + _temp;
                        }
                    }
                }
                else
                {
                    finalNames = "<tr><td></td></tr>";
                }
                
                
                content = content.Replace("##UploadedFileName##", finalNames);

                content = content.Replace("##Name##", data.a.EmployeeName);
                content = content.Replace("##EmployeeNumber##", data.a.EmployeeNumber);
                content = content.Replace("##Department##", data.a.Department);
                content = content.Replace("##sStartDate##", Assistant.SOMDbToDateTimePickerAsString(data.b.StartDate));
                content = content.Replace("##sEndDate##", Assistant.SOMDbToDateTimePickerAsString(data.b.EndDate));
                content = content.Replace("##ProjectTitle##", data.b.ProjectTitle);
                content = content.Replace("##Idea##", data.b.Idea);
                content = content.Replace("##PrimaryObjective##", data.b.PrimaryObjective);
                content = content.Replace("##BriefDescription##", data.b.BriefDescription);
                content = content.Replace("##Benefits##", data.b.Benefits);

                content = content.Replace("##AreaOfInterest1##", data.a.EmployeeName);
                content = content.Replace("##ImplementationStatus1##", data.a.EmployeeName);
                content = content.Replace("##FileName##", data.a.EmployeeName);
                content = content.Replace("##FileName1##", data.a.EmployeeName);

                content = content.Replace("##Acknowledgement##", data.b.Acknowledgement);

                if (!string.IsNullOrEmpty(data.b.NomSignature))
                {
                    RepositoryResponse _baseModel = new RepositoryResponse();
                    LoginRepo = new LoginRepo();
                    _baseModel = LoginRepo.GetUserDetailsByUserID(data.b.NomSignature);
                    if (_baseModel.success)
                    {
                        EmpMasterModel empModel = _baseModel.Data;
                        if (empModel != null)
                        {   
                            content = content.Replace("##NominationSignature##", empModel.EmployeeName);
                        }
                    }
                }
               
                content = content.Replace("##NominationSubmittedDate##", Assistant.SOMDbToDateTimePickerAsString(data.b.NomSubmittedDate));
                content = content.Replace("##DHComments##", data.b.DHComments);
                content = content.Replace("##DHSubmittedDate##", Assistant.SOMDbToDateTimePickerAsString(data.b.DHSubmittedDate));
                

                if (!string.IsNullOrEmpty(data.b.DHSignature))
                {
                    RepositoryResponse _baseModel = new RepositoryResponse();
                    LoginRepo = new LoginRepo();
                    _baseModel = LoginRepo.GetUserDetailsByUserID(data.b.DHSignature);
                    if (_baseModel.success)
                    {
                        EmpMasterModel empModel = _baseModel.Data;
                        if (empModel != null)
                        {
                            content = content.Replace("##DHSignature##", empModel.EmployeeName);
                        }
                    }
                }
            }
            return content;
        }

        public string GetNominationStringById(int id)
        {
            string _message = string.Empty;
            switch (id)
            {
                case 2001:
                    _message = "Nomination created";
                    break;
                case 2002:
                    _message = "Nomination Form assigned to HoD";
                    break;
                case 2003:
                    _message = "HoD assigned to Nominee for Clarification";
                    break;
                case 2004:
                    _message = "HoD assigned to Evaluators";
                    break;
                case 2005:
                    _message = "Evaluator added Score for Nomination Form";
                    break;
                case 2006:
                    _message = "Evaluator submited Score to TQC";
                    break;
                case 2007:
                    _message = "HoD rejected Nomination Form";
                    break;
                case 2008:
                    _message = "TQC rejected Nomination Form";
                    break;
                case 2009:
                    _message = "Evaluator rejected Nomination Form";
                    break;
                case 2010:
                    _message = "Completed";
                    break;
                case 2011:
                    _message = "Admin has re-opened Nomination Form";
                    break;
                case 2012:
                    _message = "Nomination re-assigned to HoD";
                    break;
                default:
                    break;
            }
            return _message;
        }


        #region Horizontal Deployment

        private void BindHoriozontalDeploymentData(string NominationID, string lstIDs, string lstArea, string lstImpStatus, string loggedInUserID)
        {
            string[] stringSeparators = new string[] { "###" };
            List<HorizontaldeploymentModel> lst = new List<HorizontaldeploymentModel>();
            string[] spltArea = lstArea.Split(stringSeparators, StringSplitOptions.None);
            string[] spltImpStatus = lstImpStatus.Split(stringSeparators, StringSplitOptions.None);
            string[] spltlstIDs = lstIDs.Split(stringSeparators, StringSplitOptions.None);
            if (spltArea.Length == spltImpStatus.Length)
            {
                for (int i = 0; i < spltArea.Length; i++)
                {
                    string fulldata = spltArea[i] + "" + spltImpStatus[i];
                    if (!string.IsNullOrEmpty(fulldata))
                    {
                        HorizontaldeploymentModel _model = new HorizontaldeploymentModel();
                        _model.NominationID = NominationID;
                        _model.AreaOfInterest = spltArea[i];
                        _model.ImplementationStatus = spltImpStatus[i];
                        //_model.ID = long.Parse(spltlstIDs[i]);
                        _model.IsActive = true;
                        lst.Add(_model);
                    }
                }
                AddOrEditHoriozontalDeployment(lst, NominationID, loggedInUserID);
            }
        }

        private void AddOrEditHoriozontalDeployment(List<HorizontaldeploymentModel> lst, string NominationID, string loggedInUserID)
        {
            foreach (var item in lst)
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var data = objSOMEntities.HorizontalDeployments.Where(r => r.AreaOfInterest == item.AreaOfInterest
                                && r.ImplementationStatus == item.ImplementationStatus
                                && r.NominationID == NominationID).FirstOrDefault();
                    if (data == null)
                    {
                        HorizontalDeployment dbModel = new HorizontalDeployment();
                        dbModel.EmpNumber = loggedInUserID;
                        dbModel.AreaOfInterest = item.AreaOfInterest;
                        dbModel.NominationID = NominationID;
                        dbModel.ImplementationStatus = item.ImplementationStatus;
                        dbModel.IsActive = true;
                        dbModel.CreatedBy = loggedInUserID;
                        objSOMEntities.HorizontalDeployments.Add(dbModel);
                        objSOMEntities.SaveChanges();
                    }
                    else
                    {
                        data.EmpNumber = loggedInUserID;
                        data.AreaOfInterest = item.AreaOfInterest;
                        data.NominationID = NominationID;
                        data.ImplementationStatus = item.ImplementationStatus;
                        data.IsActive = true;
                        data.ModifiedBy = loggedInUserID;
                        objSOMEntities.SaveChanges();
                    }
                }
            }
        }


        public List<HorizontaldeploymentModel> LoadHorinzontalDepolymentByNominationID(string NominationID)
        {
            List<HorizontaldeploymentModel> lstHorDep = new List<HorizontaldeploymentModel>();
            using (objSOMEntities = new SOMEntities())
            {
                var _hd = objSOMEntities.HorizontalDeployments.Where(r => r.NominationID == NominationID && r.IsActive == true).ToList();
                foreach (var item in _hd)
                {
                    HorizontaldeploymentModel _model = new HorizontaldeploymentModel();
                    _model.AreaOfInterest = item.AreaOfInterest;
                    _model.ImplementationStatus = item.ImplementationStatus;
                    _model.NominationID = NominationID;
                    _model.IsActive = (bool)item.IsActive;
                    _model.ID = item.ID;
                    lstHorDep.Add(_model);
                }
            }
            return lstHorDep;
        }

        #endregion

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

    }
}
