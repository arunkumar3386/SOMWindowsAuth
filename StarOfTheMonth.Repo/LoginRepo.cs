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
        RepositoryResponse GetPageAccessListByUserGrade(string grade);
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
                var empAdmin = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == loggedInUserID
                                && r.IsActive == true).FirstOrDefault();

                if (empAdmin.UserRole == 102)
                {
                    return (int)EmployeeRole.Admin;
                }

                var tqcHead = objSOMEntities.TQCHeads.Where(r => r.EmployeeNumber == loggedInUserID
                            && r.IsActive == true).FirstOrDefault();

                if (tqcHead != null)
                {
                    return (int)EmployeeRole.TQCHead;
                }

                var emp = objSOMEntities.Configurations.Where(r => r.Type == "NominationUserGrades" && r.IsActive == true).FirstOrDefault();

                if (emp.Value.Contains(userGrade))
                {
                    return (int)EmployeeRole.Nomination;
                }

                var eva = objSOMEntities.Configurations.Where(r => r.Type == "EvaluationUserGrades" && r.IsActive == true).FirstOrDefault();

                if (eva.Value.Contains(userGrade))
                {
                    return (int)EmployeeRole.Evaluation;
                }

                return (int)EmployeeRole.DepartmentHead;

            }
        }


        public RepositoryResponse GetPageAccessListByUserGrade(string grade)
        {
            RepositoryResponse baseModel = new RepositoryResponse();
            using (objSOMEntities = new SOMEntities())
            {
                var NomUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationUserGrades" && r.IsActive == true).
                                    Select(r=>r.Value).FirstOrDefault();

                var DHUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationUserGrades" && r.IsActive == true).
                                    Select(r => r.Value).FirstOrDefault();

                var EvaUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "EvaluationUserGrades" && r.IsActive == true).
                                    Select(r => r.Value).FirstOrDefault();

                Configuration objConfig = new Configuration();
                if (NomUsrRole.Contains(grade))
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type== "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
                }
                else if (grade == "DH")
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "DepartmentHeadUserRole-102" && r.IsActive == true).FirstOrDefault();
                }
                else if (EvaUsrRole.Contains(grade))
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "EvaluationUserRole-103" && r.IsActive == true).FirstOrDefault();
                }
                else if (grade == "TQC")
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "TQCHeadUserRole-104" && r.IsActive == true).FirstOrDefault();
                }
                else if (grade == "Admin")
                {
                    objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "AdminUserRole" && r.IsActive == true).FirstOrDefault();
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

                        var Codes = new[] { (int)NominationStatus.DH_Reject,
                                        (int)NominationStatus.DH_Assign_EmployeeClarification,
                                        (int)NominationStatus.AdminReopen,
                                        (int)NominationStatus.Evaluator_Reject};

                        var nomUsrCount = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID 
                                            && r.IsNewAlert == true 
                                            && Codes.Contains((int)r.CurrentStatus)).ToList();

                        //var nomUsrCount = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID && r.IsNewAlert == true
                        //            && (r.CurrentStatus == (int)NominationStatus.DH_Reject
                        //            || r.CurrentStatus == (int)NominationStatus.DH_Assign_EmployeeClarification
                        //            || r.CurrentStatus == (int)NominationStatus.AdminReopen 
                        //            || r.CurrentStatus == (int)NominationStatus.Evaluator_Reject)
                        //            ).ToList();
                        count = nomUsrCount.Count;
                        break;
                    case (int)EmployeeRole.DepartmentHead: //DepartmentHead
                        var Codes1 = new[] { (int)NominationStatus.Employee_Assign_DH,
                                        (int)NominationStatus.Employee_ReAssign_DH};

                        var DHUsrCount = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID
                                        && r.IsNewAlert == true
                                        && Codes1.Contains((int)r.CurrentStatus)).ToList();

                        //var DHUsrCount = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID && r.IsNewAlert == true
                        //            && (r.CurrentStatus == (int)NominationStatus.Employee_Assign_DH
                        //            || r.CurrentStatus == (int)NominationStatus.Employee_ReAssign_DH)).ToList();
                        count = DHUsrCount.Count;
                        break;
                    case (int)EmployeeRole.TQCHead: //TQCHead
                        var Codes2 = new[] { (int)NominationStatus.DH_Assign_TQC,
                                        (int)NominationStatus.Evaluators_Assign_TQC};

                        var tqcUsrCount = objSOMEntities.AuditLogs.Where(r => r.TQCHeadID == loggedInUserID
                                     && r.IsNewAlert == true
                                     && Codes2.Contains((int)r.CurrentStatus)).ToList();

                        //var tqcUsrCount = objSOMEntities.AuditLogs.Where(r => r.TQCHeadID == loggedInUserID && r.IsNewAlert == true &&
                        //        (r.CurrentStatus == (int)NominationStatus.DH_Assign_TQC || r.CurrentStatus == (int)NominationStatus.Evaluators_Assign_TQC)).ToList();
                        count = tqcUsrCount.Count;
                        break;
                    case (int)EmployeeRole.Evaluation: //Evaluation
                        var eavUsrCount = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
                                    (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
                        count = eavUsrCount.Count;
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
