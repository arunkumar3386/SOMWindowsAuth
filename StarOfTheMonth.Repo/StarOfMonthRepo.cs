using StarOfTheMonth.DataBase;
using StarOfTheMonth.Model.Models;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StarOfTheMonth.Repo
{
    public interface IStarOfMonthRepo : IDisposable
    {
        RepositoryResponse LoadStarOfMonthDetailsForGrid(string dept, string date);
        RepositoryResponse AddOrEditStarOfMonth(StarOfMonthModel model, string loggedInuserID);
        RepositoryResponse LoadStarOfMonthDetailsByID(int ID, string loggedInuserID);
        RepositoryResponse DeleteStarOfMonthDetailsByID(int ID, string loggedInuserID);

        List<AutoCompleteBox> GetStarOfMembersDetailsByEmpName(bool isRequiredDept);
        IEnumerable<SelectListItem> LoadNominationIDsByEmpID(string empNum);
        IEnumerable<SelectListItem> LoadNominationIDsEvalCompByMonthAndYear(string sMonth, string sYear);

        List<EmpMasterModel> GetLastThreeStarOftheMonthEmpDetails();
    }
    public class StarOfMonthRepo : IStarOfMonthRepo
    {
        private SOMEntities objSOMEntities;
        private IntranetPortalEntities objIPEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;
        INominationRepo _nominationRepo;

        public static DateTime toISTDate(DateTime input)
        {
            return TimeZoneInfo.ConvertTime(input.Date, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public RepositoryResponse LoadStarOfMonthDetailsForGrid(string dept, string date)
        {
            baseModel = new RepositoryResponse();
            string subMonth = string.Empty;
            string subYear = string.Empty;

            try
            {
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {

                    var _StarDetails = (from em in objIPEntities.EmpMasters.AsEnumerable()
                                        join nom in objSOMEntities.Nominations.AsEnumerable() on em.EmployeeNumber equals nom.EmployeeNumber
                                        join st in objSOMEntities.StarOfTheMonths.AsEnumerable() on nom.NominationId equals st.NominationID
                                        select new { em, nom, st }).OrderByDescending(r => r.nom.ID).ToList();

                    if (!string.IsNullOrEmpty(date))
                    {
                        string[] sDate = date.Split('-');
                        subMonth = sDate[0];
                        subYear = sDate[1];
                    }

                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        _StarDetails = _StarDetails.Where(r => r.em.Department == dept).ToList();
                    }
                    if (!string.IsNullOrEmpty(subMonth) && !string.IsNullOrEmpty(subYear))
                    {
                        _StarDetails = _StarDetails.Where(r => r.nom.SubmittedMonth == subMonth && r.nom.SubmittedYear == subYear).ToList();
                    }
                    List<StarOfMonthModel> lstStar = new List<StarOfMonthModel>();
                    foreach (var item in _StarDetails)
                    {
                        StarOfMonthModel model = new StarOfMonthModel();
                        model.TransID = item.st.TransID;
                        model.EmpId = item.st.EmpId.ToString();
                        model.EmpName = item.em.EmployeeName;
                        model.NominationID = item.nom.NominationId;
                        model.Department = item.em.Department;
                        model.Month = item.st.Month;
                        model.Year = item.st.Year;
                        lstStar.Add(model);
                    }
                    return baseModel = new RepositoryResponse { success = true, message = "", Data = lstStar };

                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public RepositoryResponse AddOrEditStarOfMonth(StarOfMonthModel model, string loggedInuserID)
        {
            _nominationRepo = new NominationRepo();
            baseModel = new RepositoryResponse();
            try
            {
                string nomID = string.Empty;
                //string IPValue = string.Empty;
                Nomination dbNomModel = new Nomination();
                using (objSOMEntities = new SOMEntities())
                {
                    //var config = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "UPDATEIP" && r.IsActive == true).FirstOrDefault();
                    //IPValue = config.Value;
                    var dbModel = objSOMEntities.StarOfTheMonths.Where(r => r.TransID == model.TransID).FirstOrDefault();
                    if (string.IsNullOrEmpty(model.SelectedNominationID))
                    {
                        dbNomModel = objSOMEntities.Nominations.Where(r => r.NominationId == dbModel.NominationID).FirstOrDefault();
                    }
                    else
                    {
                        dbNomModel = objSOMEntities.Nominations.Where(r => r.NominationId == model.SelectedNominationID.Replace("\r\n", "")).FirstOrDefault();
                    }
                    
                   
                    if (dbModel == null)
                    {
                        nomID = dbNomModel.NominationId;
                        dbModel = new StarOfTheMonth.DataBase.StarOfTheMonth();

                        dbModel.EmpId = int.Parse(dbNomModel.EmployeeNumber);
                        dbModel.IsApproved = true;
                        dbModel.IsDisplay = true;
                       
                        if (string.IsNullOrEmpty(model.Month))
                        {
                            dbModel.Month = model.MonthFilter;
                        }
                        else
                        {
                            dbModel.Month = model.Month;
                        }
                        if (string.IsNullOrEmpty(model.Year))
                        {
                            dbModel.Year = model.YearFilter;
                        }
                        else
                        {
                            dbModel.Year = model.Year;
                        }
                        
                        dbModel.NominationID = dbNomModel.NominationId;
                        dbModel.Description = dbNomModel.Idea;
                        dbModel.ApprovedBy = int.Parse(loggedInuserID);
                        dbModel.CreatedBy = int.Parse(loggedInuserID);
                        //dbModel.CreatedDate = toISTDate(DateTime.Now);
                        dbModel.ModifiedBy = int.Parse(loggedInuserID);
                       // dbModel.ModifiedDate = toISTDate(DateTime.Now);

                        objSOMEntities.StarOfTheMonths.Add(dbModel);
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Star of Month Declared Successfully", Data = "" };
                    }
                    else
                    {
                        nomID = dbModel.NominationID;
                        //dbModel.EmpId = int.Parse(model.EmpId);
                        //dbModel.IsApproved = true;
                        //dbModel.IsDisplay = true;
                        //dbModel.Description = model.Description;
                        dbModel.Month = model.MonthFilter;
                        dbModel.Year = model.YearFilter;
                        //dbModel.NominationID = model.NominationID;
                        dbModel.CreatedBy = int.Parse(loggedInuserID);
                        //dbModel.CreatedDate = DateTime.UtcNow;
                        dbModel.ModifiedBy = int.Parse(loggedInuserID);
                        //dbModel.ModifiedDate = DateTime.UtcNow;
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Star of Month Declared updated Successfully", Data = "" };
                    }

                    if (dbNomModel != null)
                    {
                        dbNomModel.SOMComments = model.SOMComments;
                        dbNomModel.SOMSubmittedDate = DateTime.Now.ToString("ddMMyyyy");
                        dbNomModel.SOMSignature = loggedInuserID;
                        dbNomModel.Status = (int)NominationStatus.TQC_Declare_SOM;
                        dbNomModel.ModifiedBy = loggedInuserID;
                        objSOMEntities.SaveChanges();
                    }

                    Evaluation evalDbModel = objSOMEntities.Evaluations.Where(r => r.NominationID == nomID && r.EvaluatorID != "").FirstOrDefault();
                    long HODId = _nominationRepo.GetReportingIDByEmpID(dbNomModel.EmployeeNumber);
                    AuditLogModel objAuditLog = new AuditLogModel();
                    objAuditLog.CurrentStatus = NominationStatus.TQC_Declare_SOM;
                    objAuditLog.EmployeeNumber = dbNomModel.EmployeeNumber;
                    objAuditLog.IsNewAlert = true;
                    objAuditLog.IsNotification = true;
                    objAuditLog.DepartmentHeadID = HODId.ToString();
                    objAuditLog.TQCHeadID = loggedInuserID;
                    objAuditLog.EvaluatorID = evalDbModel.EvaluatorID;
                    objAuditLog.NominationID = nomID;
                    objAuditLog.CreatedBy = loggedInuserID;
                    _nominationRepo.AddEntryIntoAuditLog(objAuditLog);
                }

                
                //if (string.IsNullOrEmpty(IPValue) && IPValue == "true")
                //{
                //    AddOrEditStarOfMonth_IntranetPortal(model, loggedInuserID);
                //}



            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }


        public RepositoryResponse AddOrEditStarOfMonth_IntranetPortal(StarOfMonthModel model, string loggedInuserID)
        {
            _nominationRepo = new NominationRepo();
            baseModel = new RepositoryResponse();
            try
            {
                string nomID = string.Empty;
                Nomination dbNomModel = new Nomination();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var dbModel = objIPEntities.StarOfTheMonths.Where(r => r.TransID == model.TransID).FirstOrDefault();
                    if (string.IsNullOrEmpty(model.SelectedNominationID))
                    {
                        dbNomModel = objSOMEntities.Nominations.Where(r => r.NominationId == dbModel.NominationID).FirstOrDefault();
                    }
                    else
                    {
                        dbNomModel = objSOMEntities.Nominations.Where(r => r.NominationId == model.SelectedNominationID.Replace("\r\n", "")).FirstOrDefault();
                    }

                    if (dbModel == null)
                    {
                        nomID = dbNomModel.NominationId;
                        dbModel = new StarOfTheMonth.DataBase.StarOfTheMonth();

                        dbModel.EmpId = int.Parse(dbNomModel.EmployeeNumber);
                        dbModel.IsApproved = true;
                        dbModel.IsDisplay = true;

                        if (string.IsNullOrEmpty(model.Month))
                        {
                            dbModel.Month = model.MonthFilter;
                        }
                        else
                        {
                            dbModel.Month = model.Month;
                        }
                        if (string.IsNullOrEmpty(model.Year))
                        {
                            dbModel.Year = model.YearFilter;
                        }
                        else
                        {
                            dbModel.Year = model.Year;
                        }

                        dbModel.NominationID = dbNomModel.NominationId;
                        dbModel.Description = dbNomModel.Idea;

                        dbModel.CreatedBy = int.Parse(loggedInuserID);
                        dbModel.ModifiedBy = int.Parse(loggedInuserID);

                        objIPEntities.StarOfTheMonths.Add(dbModel);
                        objIPEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Star of Month Details Added Successfully", Data = "" };
                    }
                    else
                    {
                        nomID = dbModel.NominationID;
                        dbModel.Month = model.MonthFilter;
                        dbModel.Year = model.YearFilter;
                        dbModel.CreatedBy = int.Parse(loggedInuserID);
                        dbModel.ModifiedBy = int.Parse(loggedInuserID);
                        objIPEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Star of Month Details updated Successfully", Data = "" };
                    }
                }


            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public RepositoryResponse LoadStarOfMonthDetailsByID(int ID, string loggedInuserID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {                    
                    var data = objSOMEntities.StarOfTheMonths.Where(r => r.TransID == ID).FirstOrDefault();
                    if (data != null)
                    {
                        var usr = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == data.EmpId.ToString()).FirstOrDefault();
                        var nom = objSOMEntities.Nominations.Where(r => r.NominationId == data.NominationID).FirstOrDefault();
                        StarOfMonthModel model = new StarOfMonthModel();
                        model.TransID = data.TransID;
                        model.EmpId = data.EmpId.ToString();
                        model.EmpName = usr.EmployeeName;
                        model.Description = data.Description;
                        model.Month = data.Month;
                        model.Year = data.Year;
                        model.NominationID = data.NominationID;
                        model.SOMComments = nom.SOMComments;
                        baseModel = new RepositoryResponse { success = true, message = "", Data = model };
                    }
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = "", Data = ex.ToString() }; ;
            }
            return baseModel;
        }

        public RepositoryResponse DeleteStarOfMonthDetailsByID(int ID, string loggedInuserID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var data = objSOMEntities.StarOfTheMonths.Where(r => r.TransID == ID).FirstOrDefault();
                    if (data != null)
                    {
                        objSOMEntities.StarOfTheMonths.Remove(data);
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Star of month details deleted Sucessfully", Data = "" };
                    }
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = "", Data = ex.ToString() }; ;
            }
            return baseModel;
        }

        public List<AutoCompleteBox> GetStarOfMembersDetailsByEmpName(bool isRequiredDept)
        {
            List<AutoCompleteBox> lstAutoCompBox = new List<AutoCompleteBox>();

            List<AuditLog> lstValue = new List<AuditLog>();
            using (objSOMEntities = new SOMEntities())
            {
                lstValue = objSOMEntities.AuditLogs.Where(r => r.CurrentStatus == (int)NominationStatus.Evaluators_Assign_TQC && r.IsActive == true).ToList();
            }

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var empDet = (from a in lstValue.AsEnumerable()
                              join b in objIPEntities.EmpMasters.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                              select new { b.EmployeeNumber, b.Department, b.EmployeeName });

                foreach (var item in empDet)
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
            return lstAutoCompBox;
        }

        public IEnumerable<SelectListItem> LoadNominationIDsByEmpID(string empNum)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(empNum))
            {
                using (objSOMEntities = new SOMEntities())
                {
                    var lstNominationIDs = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == empNum
                                            && r.CurrentStatus == (int)NominationStatus.Evaluators_Assign_TQC).Select(r=>r.NominationID).Distinct().ToList();
                    foreach (var item in lstNominationIDs)
                    {
                        SelectListItem objSelectListItem = new SelectListItem();
                        string _nomID = item;
                        objSelectListItem.Text = _nomID;//its displayed in UI
                        objSelectListItem.Value = _nomID;
                        lstSelect.Add(objSelectListItem);
                    }
                }
            }
            return lstSelect;
        }

        public IEnumerable<SelectListItem> LoadNominationIDsEvalCompByMonthAndYear(string sMonth, string sYear)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(sMonth) && !string.IsNullOrEmpty(sYear))
            {
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    //var lstNominationIDs = objSOMEntities.Nominations.Where(r => r.SubmittedMonth == sMonth
                    //                        && r.SubmittedYear == sYear
                    //                        && r.Status == (int)NominationStatus.Evaluators_Assign_TQC).Select(r => r.NominationId).Distinct().ToList();

                    var det = (from a in objSOMEntities.Nominations.AsEnumerable()
                               join b in objIPEntities.EmpMasters.AsEnumerable() on a.EmployeeNumber equals b.EmployeeNumber
                               where a.SubmittedMonth == sMonth && a.SubmittedYear == sYear
                               && a.Status == (int)NominationStatus.Evaluators_Assign_TQC
                               select new { a, b }).ToList();

                    SelectListItem objSelectListItem = new SelectListItem();
                    objSelectListItem.Text = "--Select--";
                    objSelectListItem.Value = "-1";
                    objSelectListItem.Selected = true;
                    lstSelect.Add(objSelectListItem);
                    foreach (var item in det)
                    {
                        objSelectListItem = new SelectListItem();
                        string _nomID = item.a.NominationId;
                        objSelectListItem.Text = _nomID;//its displayed in UI
                        objSelectListItem.Value = item.b.EmployeeName + " (" + item.b.EmployeeNumber + ")";
                        lstSelect.Add(objSelectListItem);
                    }
                }
            }
            return lstSelect;
        }

        public List<EmpMasterModel> GetLastThreeStarOftheMonthEmpDetails()
        {
            string currentYear = DateTime.Now.ToString("yyyy");
            List<EmpMasterModel> lstEmpModel = new List<EmpMasterModel>();
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var somData = objSOMEntities.StarOfTheMonths.Where(r=>r.Year == currentYear).OrderByDescending(r => r.TransID).ToList();

                foreach (var item in somData)
                {
                    var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == item.EmpId.ToString()).FirstOrDefault();
                    if (emp != null)
                    {
                        EmpMasterModel model = new EmpMasterModel();
                        model.EmpID = emp.EmpID;
                        model.EmployeeName = emp.EmployeeName;
                        model.EmployeeNumber = emp.EmployeeNumber;
                        model.ImagePath = emp.ImagePath;
                        model.EMPMonth = item.Month;
                        model.EMPYear = item.Year;
                        lstEmpModel.Add(model);
                    }
                }
            }
            return lstEmpModel;
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
    }
}
