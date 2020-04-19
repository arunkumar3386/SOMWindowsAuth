using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StarOfTheMonth.Repo
{
    public interface ILoginRepo : IDisposable
    {
        //EmployeeModel CheckUser(LoginModel login);
        RepositoryResponse CheckUser(LoginModel login);
        List<AutoCompleteBox> GetEmpDetailsByEmpName(bool isRequiredDept);
        List<SelectListItem> GetMonthYearFilter(int Count = 12);
        List<SelectListItem> GetDepartmentDetails(string selectedDept ="", string selectDetpOnly="");
        RepositoryResponse GetLoginUserDetails(string userName);
        RepositoryResponse GetPageAccessListByUserGrade(string grade, string loggedInuser);
        RepositoryResponse GetUserDetailsByUserID(string userID);
        int GetCountForUser(string loggedInUserID, int SOMUserGrade);
    }

    public class LoginRepo : ILoginRepo
    {
        private SOMEntities objSOMEntities;
        private IntranetPortalEntities objIPEntities;
        private bool disposed = false;


        public RepositoryResponse CheckUser(LoginModel login)
        {
            RepositoryResponse baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    EmployeeModel model = new EmployeeModel();
                    var emp = objIPEntities.EmpMasters.Where(r => r.EmployeeName == login.Username
                                        && r.Password == login.Password && r.IsActive == true).FirstOrDefault();
                    if (emp != null && emp.EmpID > 0)
                    {
                        model = new EmployeeModel();
                        model.ID = emp.EmpID;
                        model.Name = emp.EmployeeName;
                        //model.
                        //model.Role = emp.Role;
                        model.UserName = emp.EmployeeName;
                        model.EmailID = emp.EmployeeEmail;
                        baseModel.Data = model;
                        baseModel.message = "Date Retrieved Successfully";
                        baseModel.success = true;
                        return baseModel;
                    }
                    else
                    {
                        baseModel.Data = model;
                        baseModel.message = "Unable to retrieve data";
                        baseModel.success = false;
                        return baseModel;
                    }
                }
            }
            catch (Exception ex)
            {

                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
     
            }
            return baseModel;
            
        }

        public List<AutoCompleteBox> GetEmpDetailsByEmpName(bool isRequiredDept)
        {
            List<AutoCompleteBox> lstAutoCompBox = new List<AutoCompleteBox>();
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {

                var emp = objIPEntities.EmpMasters.Where(r => r.IsActive == true).ToList();
                foreach (var item in emp)
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

        public List<SelectListItem> GetDepartmentDetails(string selectedDept, string selectDetpOnly)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            if (string.IsNullOrEmpty(selectDetpOnly))
            {
                lstSelect.Add(new SelectListItem { Text = "--ALL--", Value = "0", Selected = true });
            }

            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var emp = objIPEntities.EmpMasters.Where(r => r.IsActive == true).Select(r => r.Department).Distinct().ToList();
                foreach (var item in emp)
                {
                    SelectListItem objSelectListItem = new SelectListItem();
                    if (string.IsNullOrEmpty(selectDetpOnly))
                    {
                        objSelectListItem.Text = item;//its displayed in UI
                        objSelectListItem.Value = item;
                        if (!string.IsNullOrEmpty(selectedDept) && selectedDept == item)
                        {
                            objSelectListItem.Selected = true;
                        }
                        lstSelect.Add(objSelectListItem);
                    }
                    else
                    {
                        if (item == selectDetpOnly)
                        {
                            objSelectListItem.Text = item;//its displayed in UI
                            objSelectListItem.Value = item;
                            if (!string.IsNullOrEmpty(selectedDept) && selectedDept == item)
                            {
                                objSelectListItem.Selected = true;
                            }
                            lstSelect.Add(objSelectListItem);
                        }
                    }
                }
            }
            return lstSelect;
        }

        public List<SelectListItem> GetMonthYearFilter(int Count)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            lstSelect.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
            var lstMonths = Enumerable.Range(0, Count).Select(i => DateTime.Now.AddMonths(i - Count).ToString("MMM/yyyy"));
            foreach (var item in lstMonths)
            {
                SelectListItem objSelectListItem = new SelectListItem();
                objSelectListItem.Text = item; //its displayed in UI
                objSelectListItem.Value = "01-" + item; ;
                lstSelect.Add(objSelectListItem);
            }
            var currentMonth = DateTime.Now.ToString("MMM/yyyy");
            lstSelect.Add(new SelectListItem { Text = currentMonth, Value = "01-" + currentMonth });
            
            return lstSelect;
        }

        public RepositoryResponse GetLoginUserDetails(string userName)
        {
            RepositoryResponse baseModel = new RepositoryResponse();
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var userDet = objIPEntities.EmpMasters.Where(r => r.UserName == userName && r.IsActive == true).FirstOrDefault();
                if (userDet != null)
                {
                    EmpMasterModel model = new EmpMasterModel();
                    model.UserName = userDet.UserName;
                    model.EmployeeName = userDet.EmployeeName;
                    model.UserRole = userDet.UserRole;
                    model.EmployeeNumber = userDet.EmployeeNumber;
                    model.Department = userDet.Department;
                    model.Grade = userDet.Grade;
                    baseModel = new RepositoryResponse { success = true, message = "user Details retrieved Successfully", Data = model };
                }
                else
                {
                    baseModel = new RepositoryResponse { success = false, message = "user Details not available" };
                }
            }
            return baseModel;
        }



        public RepositoryResponse GetUserDetailsByUserID(string userID)
        {
            RepositoryResponse baseModel = new RepositoryResponse();
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                var userDet = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == userID && r.IsActive == true).FirstOrDefault();
                if (userDet != null)
                {
                    EmpMasterModel model = new EmpMasterModel();
                    model.UserName = userDet.UserName;
                    model.UserRole = userDet.UserRole;
                    model.EmployeeNumber = userDet.EmployeeNumber;
                    model.Department = userDet.Department;
                    model.Grade = userDet.Grade;
                    model.EmployeeName = userDet.EmployeeName;
                    model.EmployeeSOMRole = GetEmployeeRole(userID, userDet.Grade);
                    baseModel = new RepositoryResponse { success = true, message = "user Details retrieved Successfully", Data = model };
                }
                else
                {
                    baseModel = new RepositoryResponse { success = false, message = "user Details not available" };
                }
            }
            return baseModel;
        }

        public int GetEmployeeRole(string loggedInUserID, string userGrade)
        {
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {

                var tqcHead = objSOMEntities.TQCHeads.Where(r => r.EmployeeNumber == loggedInUserID
                            && r.IsActive == true).FirstOrDefault();

                if (tqcHead != null)
                {
                    return (int)EmployeeRole.TQCHead;
                }

                var empAdmin = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == loggedInUserID
                                && r.IsActive == true).FirstOrDefault();

                if (empAdmin.UserRole == 102)
                {
                    var _emp = objSOMEntities.Configurations.Where(r => r.Type == "NominationUserGrades" && r.IsActive == true).FirstOrDefault();
                    var _eva = objSOMEntities.Configurations.Where(r => r.Type == "EvaluationUserGrades" && r.IsActive == true).FirstOrDefault();

                    int _empNum = int.Parse(loggedInUserID);
                    var _reptPersonDH = objIPEntities.EmpMasters.Where(r => r.ReportingPersonId == _empNum).FirstOrDefault();

                    if (!string.IsNullOrEmpty(userGrade) && _emp.Value.ToLower().Contains(userGrade.ToLower()) && _reptPersonDH != null)
                    {
                        return (int)EmployeeRole.AdminNominationHoD;
                    }
                    else if (!string.IsNullOrEmpty(userGrade) && _eva.Value.ToLower().Contains(userGrade.ToLower()) && _reptPersonDH != null)
                    {
                        return (int)EmployeeRole.AdminEvaluationHoD;
                    }
                    else if (!string.IsNullOrEmpty(userGrade) && _emp.Value.ToLower().Contains(userGrade.ToLower()))
                    {
                        return (int)EmployeeRole.AdminNomination;
                    }
                    else if (!string.IsNullOrEmpty(userGrade) && _eva.Value.ToLower().Contains(userGrade.ToLower()))
                    {
                        return (int)EmployeeRole.AdminEvaluation;
                    }
                    else if (_reptPersonDH != null)
                    {
                        return (int)EmployeeRole.AdminHoD;
                    }
                    else
                    {
                        return (int)EmployeeRole.Admin;
                    }
                }

                

                var emp = objSOMEntities.Configurations.Where(r => r.Type == "NominationUserGrades" && r.IsActive == true).FirstOrDefault();

                if (!string.IsNullOrEmpty(userGrade) && emp.Value.ToLower().Contains(userGrade.ToLower()))
                {
                    return (int)EmployeeRole.Nomination;
                }

                var eva = objSOMEntities.Configurations.Where(r => r.Type == "EvaluationUserGrades" && r.IsActive == true).FirstOrDefault();

                if (!string.IsNullOrEmpty(userGrade) && eva.Value.ToLower().Contains(userGrade.ToLower()))
                {
                    return (int)EmployeeRole.Evaluation;
                }

                return (int)EmployeeRole.DepartmentHead;

            }
        }

        public RepositoryResponse GetPageAccessListByUserGrade(string grade, string loggedInuser)
        {
            RepositoryResponse baseModel = new RepositoryResponse();
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                string OrgGrade = string.Empty;
                var emp = objIPEntities.EmpMasters.Where(r => r.UserName == loggedInuser).FirstOrDefault();
                int _empNum = int.Parse(emp.EmployeeNumber);
                var reptPerson = objIPEntities.EmpMasters.Where(r => r.ReportingPersonId == _empNum).FirstOrDefault();
                if (reptPerson != null)
                {
                    OrgGrade = emp.Grade;//reptPerson.Grade;
                    grade = "DH";
                }
                var NomUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationUserGrades" && r.IsActive == true).
                                    Select(r => r.Value.ToLower()).FirstOrDefault();

                //var DHUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationUserGrades" && r.IsActive == true).
                //                    Select(r => r.Value).FirstOrDefault();

                var EvaUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "EvaluationUserGrades" && r.IsActive == true).
                                    Select(r => r.Value.ToLower()).FirstOrDefault();

                var TqCUser = objSOMEntities.TQCHeads.Where(r => r.IsActive == true && r.EmployeeNumber == emp.EmployeeNumber).FirstOrDefault();

                Configuration objConfig = new Configuration();
                if (emp.UserRole == 102)
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "AdminUserRole" && r.IsActive == true).FirstOrDefault();
                    if (!string.IsNullOrEmpty(emp.Grade) && NomUsrRole.Contains(emp.Grade.ToLower()))
                    {
                        var _objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
                        if (_objConfig != null)
                        {
                            objConfig.Value = "liAddNomination," + objConfig.Value;
                        }
                    }
                    if (grade == "DH" && !string.IsNullOrEmpty(OrgGrade.ToLower()))
                    {
                        // var _objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
                        if (objConfig != null)
                        {
                            objConfig.Value = "liNominationPendinglst," + objConfig.Value;
                        }
                    }
                    if (grade == "DH" && EvaUsrRole.Contains(OrgGrade.ToLower()))
                    {
                        // var _objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
                        if (objConfig != null)
                        {
                            objConfig.Value = "liEvaluationlst," + objConfig.Value;
                        }
                    }
                }
                else if (grade == "DH" && !string.IsNullOrEmpty(OrgGrade.ToLower()) && EvaUsrRole.Contains(OrgGrade.ToLower()))
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "DHEvalUser" && r.IsActive == true).FirstOrDefault();
                }
                else if (grade == "DH")
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "DepartmentHeadUserRole-102" && r.IsActive == true).FirstOrDefault();
                }
                else if (!string.IsNullOrEmpty(emp.Grade) && NomUsrRole.Contains(emp.Grade.ToLower()))
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
                }
                else if (!string.IsNullOrEmpty(emp.Grade) && EvaUsrRole.Contains(emp.Grade.ToLower()))
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "EvaluationUserRole-103" && r.IsActive == true).FirstOrDefault();
                }

                if (TqCUser != null)
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "TQCHeadUserRole-104" && r.IsActive == true).FirstOrDefault();
                }
                if (objConfig != null)
                {
                    //EmpMasterModel model = new EmpMasterModel();
                    //model.UserName = confDet.Value;
                    baseModel = new RepositoryResponse { success = true, message = "user Details retrieved Successfully", Data = objConfig.Value };
                }
                else
                {
                    baseModel = new RepositoryResponse { success = false, message = "user Details not available" };
                }
            }
            return baseModel;
        }

        public int GetCountForUser(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                switch (SOMUserGrade)
                {
                    case (int)EmployeeRole.Nomination: //Nomination
                        var Codes = new[] { (int)NominationStatus.HoD_Reject,
                                        (int)NominationStatus.HoD_Assign_EmployeeClarification,
                                        (int)NominationStatus.AdminReopen,
                                        (int)NominationStatus.Evaluator_Reject};

                        var nomUsrCount = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID 
                                            && r.IsNewAlert == true 
                                            && Codes.Contains((int)r.CurrentStatus)).ToList();
                        count = nomUsrCount.Count;
                        break;

                    case (int)EmployeeRole.AdminNomination: //Nomination
                        var CodesAN = new[] { (int)NominationStatus.HoD_Reject,
                                        (int)NominationStatus.HoD_Assign_EmployeeClarification,
                                        (int)NominationStatus.AdminReopen,
                                        (int)NominationStatus.Evaluator_Reject};

                        var nomUsrCountAN = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID
                                            && r.IsNewAlert == true
                                            && CodesAN.Contains((int)r.CurrentStatus)).ToList();
                
                        count = nomUsrCountAN.Count;
                        break;

                    case (int)EmployeeRole.AdminNominationHoD: // Admin Nomination DH
                        var CodesANDH = new[] { (int)NominationStatus.HoD_Reject,
                                        (int)NominationStatus.HoD_Assign_EmployeeClarification,
                                        (int)NominationStatus.AdminReopen,
                                        (int)NominationStatus.Evaluator_Reject};

                        var nomUsrCountANDH = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID
                                            && r.IsNewAlert == true
                                            && CodesANDH.Contains((int)r.CurrentStatus)).ToList();

                        count = nomUsrCountANDH.Count;
                        break;

                    case (int)EmployeeRole.DepartmentHead: //DepartmentHead
                        var Codes1 = new[] { (int)NominationStatus.Employee_Assign_HOD,
                                        (int)NominationStatus.Employee_ReAssign_HoD};

                        var DHUsrCount = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID
                                        && r.IsNewAlert == true
                                        && Codes1.Contains((int)r.CurrentStatus)).ToList();
                        count = DHUsrCount.Count;
   
                     break;

                    case (int)EmployeeRole.AdminHoD: //DepartmentHead Admin
                        var _Codes1 = new[] { (int)NominationStatus.Employee_Assign_HOD,
                                        (int)NominationStatus.Employee_ReAssign_HoD};

                        var _DHUsrCount = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID
                                        && r.IsNewAlert == true
                                        && _Codes1.Contains((int)r.CurrentStatus)).ToList();
                        count = _DHUsrCount.Count;
                        break;

                    case (int)EmployeeRole.TQCHead: //TQCHead
                        var Codes2 = new[] { (int)NominationStatus.HoD_Assign_TQC,
                                        (int)NominationStatus.Evaluators_Assign_TQC};

                        var tqcUsrCount = objSOMEntities.AuditLogs.Where(r => 
                                      r.IsNewAlert == true
                                     && Codes2.Contains((int)r.CurrentStatus)).ToList();
                        count = tqcUsrCount.Count;
                        break;

                    case (int)EmployeeRole.Evaluation: //Evaluation
                        var eavUsrCount = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
                                    (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
                        count = eavUsrCount.Count;
                        break;

                    case (int)EmployeeRole.AdminEvaluation: //Evaluation Admin
                        var _eavUsrCount = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
                                    (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
                        count = _eavUsrCount.Count;
                        break;

                    case (int)EmployeeRole.AdminEvaluationHoD: //Evaluation Admin DH
                        var _eavUsrCountDH = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
                                    (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
                        count = _eavUsrCountDH.Count;
                        break;
                }
            }
            return count;
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
