using Helper;
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
        //RepositoryResponse GetPageAccessListByUserGrade(string grade, string loggedInuser);
        RepositoryResponse GetUserDetailsByUserID(string userID);
        //int GetCountForUser(string loggedInUserID, int SOMUserGrade);
        int getActionCounts(string loggedInUserID, int SOMUserGrade);
        string getMenuForUser(string loggedInUserID, int SOMUserGrade);
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
                    //model.EmployeeSOMRole = GetEmployeeRole(userID, userDet.Grade);
                    int value = GetSOMEmployeeRoleByUser(userDet);
                    model.EmployeeSOMRole = value;
                    model.EmployeeSOMRoleAsString = EnumHelper.GetDescription((SOMEmpRole)value);
                    baseModel = new RepositoryResponse { success = true, message = "user Details retrieved Successfully", Data = model };
                }
                else
                {
                    baseModel = new RepositoryResponse { success = false, message = "user Details not available" };
                }
            }
            return baseModel;
        }

        //public int GetEmployeeRole(string loggedInUserID, string userGrade)
        //{
        //    using (objSOMEntities = new SOMEntities())
        //    using (objIPEntities = new IntranetPortalEntities())
        //    {

        //        var tqcHead = objSOMEntities.TQCHeads.Where(r => r.EmployeeNumber == loggedInUserID
        //                    && r.IsActive == true).FirstOrDefault();

        //        if (tqcHead != null)
        //        {
        //            return (int)SOMEmpRole.TQCHead;
        //        }

        //        var empAdmin = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == loggedInUserID
        //                        && r.IsActive == true).FirstOrDefault();

        //        if (empAdmin.UserRole == 102)
        //        {
        //            var _emp = objSOMEntities.Configurations.Where(r => r.Type == "NominationUserGrades" && r.IsActive == true).FirstOrDefault();
        //            var _eva = objSOMEntities.Configurations.Where(r => r.Type == "EvaluationUserGrades" && r.IsActive == true).FirstOrDefault();

        //            int _empNum = int.Parse(loggedInUserID);
        //            var _reptPersonDH = objIPEntities.EmpMasters.Where(r => r.ReportingPersonId == _empNum).FirstOrDefault();

        //            if (!string.IsNullOrEmpty(userGrade) && _emp.Value.ToLower().Contains(userGrade.ToLower()) && _reptPersonDH != null)
        //            {
        //                return (int)EmployeeRole.AdminNominationHoD;
        //            }
        //            else if (!string.IsNullOrEmpty(userGrade) && _eva.Value.ToLower().Contains(userGrade.ToLower()) && _reptPersonDH != null)
        //            {
        //                return (int)EmployeeRole.AdminEvaluationHoD;
        //            }
        //            else if (!string.IsNullOrEmpty(userGrade) && _emp.Value.ToLower().Contains(userGrade.ToLower()))
        //            {
        //                return (int)EmployeeRole.AdminNomination;
        //            }
        //            else if (!string.IsNullOrEmpty(userGrade) && _eva.Value.ToLower().Contains(userGrade.ToLower()))
        //            {
        //                return (int)EmployeeRole.AdminEvaluation;
        //            }
        //            else if (_reptPersonDH != null)
        //            {
        //                return (int)EmployeeRole.AdminHoD;
        //            }
        //            else
        //            {
        //                return (int)EmployeeRole.Admin;
        //            }
        //        }

                

        //        var emp = objSOMEntities.Configurations.Where(r => r.Type == "NominationUserGrades" && r.IsActive == true).FirstOrDefault();

        //        if (!string.IsNullOrEmpty(userGrade) && emp.Value.ToLower().Contains(userGrade.ToLower()))
        //        {
        //            return (int)EmployeeRole.Nomination;
        //        }

        //        var eva = objSOMEntities.Configurations.Where(r => r.Type == "EvaluationUserGrades" && r.IsActive == true).FirstOrDefault();

        //        if (!string.IsNullOrEmpty(userGrade) && eva.Value.ToLower().Contains(userGrade.ToLower()))
        //        {
        //            return (int)EmployeeRole.Evaluation;
        //        }

        //        return (int)EmployeeRole.DepartmentHead;

        //    }
        //}

        //public RepositoryResponse GetPageAccessListByUserGrade(string grade, string loggedInuser)
        //{
        //    RepositoryResponse baseModel = new RepositoryResponse();
        //    using (objSOMEntities = new SOMEntities())
        //    using (objIPEntities = new IntranetPortalEntities())
        //    {
        //        string OrgGrade = string.Empty;
        //        var emp = objIPEntities.EmpMasters.Where(r => r.UserName == loggedInuser).FirstOrDefault();
        //        int _empNum = int.Parse(emp.EmployeeNumber);
        //        var reptPerson = objIPEntities.EmpMasters.Where(r => r.ReportingPersonId == _empNum).FirstOrDefault();
        //        if (reptPerson != null)
        //        {
        //            OrgGrade = emp.Grade;//reptPerson.Grade;
        //            grade = "DH";
        //        }
        //        var NomUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationUserGrades" && r.IsActive == true).
        //                            Select(r => r.Value.ToLower()).FirstOrDefault();

        //        //var DHUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "NominationUserGrades" && r.IsActive == true).
        //        //                    Select(r => r.Value).FirstOrDefault();

        //        var EvaUsrRole = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "EvaluationUserGrades" && r.IsActive == true).
        //                            Select(r => r.Value.ToLower()).FirstOrDefault();

        //        var TqCUser = objSOMEntities.TQCHeads.Where(r => r.IsActive == true && r.EmployeeNumber == emp.EmployeeNumber).FirstOrDefault();

        //        Configuration objConfig = new Configuration();
        //        if (emp.UserRole == 102)
        //        {
        //            objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "AdminUserRole" && r.IsActive == true).FirstOrDefault();
        //            if (!string.IsNullOrEmpty(emp.Grade) && NomUsrRole.Contains(emp.Grade.ToLower()))
        //            {
        //                var _objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
        //                if (_objConfig != null)
        //                {
        //                    objConfig.Value = "liAddNomination," + objConfig.Value;
        //                }
        //            }
        //            if (grade == "DH" && !string.IsNullOrEmpty(OrgGrade.ToLower()))
        //            {
        //                // var _objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
        //                if (objConfig != null)
        //                {
        //                    objConfig.Value = "liNominationPendinglst," + objConfig.Value;
        //                }
        //            }
        //            if (grade == "DH" && EvaUsrRole.Contains(OrgGrade.ToLower()))
        //            {
        //                // var _objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
        //                if (objConfig != null)
        //                {
        //                    objConfig.Value = "liEvaluationlst," + objConfig.Value;
        //                }
        //            }
        //        }
        //        else if (grade == "DH" && !string.IsNullOrEmpty(OrgGrade.ToLower()) && EvaUsrRole.Contains(OrgGrade.ToLower()))
        //        {
        //            objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "DHEvalUser" && r.IsActive == true).FirstOrDefault();
        //        }
        //        else if (grade == "DH")
        //        {
        //            objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "DepartmentHeadUserRole-102" && r.IsActive == true).FirstOrDefault();
        //        }
        //        else if (!string.IsNullOrEmpty(emp.Grade) && NomUsrRole.Contains(emp.Grade.ToLower()))
        //        {
        //            objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole-101" && r.IsActive == true).FirstOrDefault();
        //        }
        //        else if (!string.IsNullOrEmpty(emp.Grade) && EvaUsrRole.Contains(emp.Grade.ToLower()))
        //        {
        //            objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "EvaluationUserRole-103" && r.IsActive == true).FirstOrDefault();
        //        }

        //        if (TqCUser != null)
        //        {
        //            objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "TQCHeadUserRole-104" && r.IsActive == true).FirstOrDefault();
        //        }
        //        if (objConfig != null)
        //        {
        //            //EmpMasterModel model = new EmpMasterModel();
        //            //model.UserName = confDet.Value;
        //            baseModel = new RepositoryResponse { success = true, message = "user Details retrieved Successfully", Data = objConfig.Value };
        //        }
        //        else
        //        {
        //            baseModel = new RepositoryResponse { success = false, message = "user Details not available" };
        //        }
        //    }
        //    return baseModel;
        //}

        //public int GetCountForUser(string loggedInUserID, int SOMUserGrade)
        //{
        //    int count = 0;
        //    using (objSOMEntities = new SOMEntities())
        //    {
        //        switch (SOMUserGrade)
        //        {
        //            case (int)EmployeeRole.Nomination: //Nomination
        //                var Codes = new[] { (int)NominationStatus.HoD_Reject,
        //                                (int)NominationStatus.HoD_Assign_EmployeeClarification,
        //                                (int)NominationStatus.AdminReopen,
        //                                (int)NominationStatus.Evaluator_Reject};

        //                var nomUsrCount = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID 
        //                                    && r.IsNewAlert == true 
        //                                    && Codes.Contains((int)r.CurrentStatus)).ToList();
        //                count = nomUsrCount.Count;
        //                break;

        //            case (int)EmployeeRole.AdminNomination: //Nomination
        //                var CodesAN = new[] { (int)NominationStatus.HoD_Reject,
        //                                (int)NominationStatus.HoD_Assign_EmployeeClarification,
        //                                (int)NominationStatus.AdminReopen,
        //                                (int)NominationStatus.Evaluator_Reject};

        //                var nomUsrCountAN = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID
        //                                    && r.IsNewAlert == true
        //                                    && CodesAN.Contains((int)r.CurrentStatus)).ToList();
                
        //                count = nomUsrCountAN.Count;
        //                break;

        //            case (int)EmployeeRole.AdminNominationHoD: // Admin Nomination DH
        //                var CodesANDH = new[] { (int)NominationStatus.HoD_Reject,
        //                                (int)NominationStatus.HoD_Assign_EmployeeClarification,
        //                                (int)NominationStatus.AdminReopen,
        //                                (int)NominationStatus.Evaluator_Reject};

        //                var nomUsrCountANDH = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID
        //                                    && r.IsNewAlert == true
        //                                    && CodesANDH.Contains((int)r.CurrentStatus)).ToList();

        //                count = nomUsrCountANDH.Count;
        //                break;

        //            case (int)EmployeeRole.DepartmentHead: //DepartmentHead
        //                var Codes1 = new[] { (int)NominationStatus.Employee_Assign_HOD,
        //                                (int)NominationStatus.Employee_ReAssign_HoD};

        //                var DHUsrCount = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID
        //                                && r.IsNewAlert == true
        //                                && Codes1.Contains((int)r.CurrentStatus)).ToList();
        //                count = DHUsrCount.Count;
   
        //             break;

        //            case (int)EmployeeRole.AdminHoD: //DepartmentHead Admin
        //                var _Codes1 = new[] { (int)NominationStatus.Employee_Assign_HOD,
        //                                (int)NominationStatus.Employee_ReAssign_HoD};

        //                var _DHUsrCount = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID
        //                                && r.IsNewAlert == true
        //                                && _Codes1.Contains((int)r.CurrentStatus)).ToList();
        //                count = _DHUsrCount.Count;
        //                break;

        //            case (int)EmployeeRole.TQCHead: //TQCHead
        //                var Codes2 = new[] { (int)NominationStatus.HoD_Assign_TQC,
        //                                (int)NominationStatus.Evaluators_Assign_TQC};

        //                var tqcUsrCount = objSOMEntities.AuditLogs.Where(r => 
        //                              r.IsNewAlert == true
        //                             && Codes2.Contains((int)r.CurrentStatus)).ToList();
        //                count = tqcUsrCount.Count;
        //                break;

        //            case (int)EmployeeRole.Evaluation: //Evaluation
        //                var eavUsrCount = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
        //                            (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
        //                count = eavUsrCount.Count;
        //                break;

        //            case (int)EmployeeRole.AdminEvaluation: //Evaluation Admin
        //                var _eavUsrCount = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
        //                            (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
        //                count = _eavUsrCount.Count;
        //                break;

        //            case (int)EmployeeRole.AdminEvaluationHoD: //Evaluation Admin DH
        //                var _eavUsrCountDH = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
        //                            (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
        //                count = _eavUsrCountDH.Count;
        //                break;
        //        }
        //    }
        //    return count;
        //}

        public int getActionCounts(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                switch (SOMUserGrade)
                {
                    case (int)SOMEmpRole.TQCHead: //3001
                        count = GetTQCCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.Admin: //3002
                        count = GetAdminCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.HoD: //3003
                        count = GetHoDCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.Evaluation: //3004
                        count = GetEvaluationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.Nomination: //3005
                        count = GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.EvaluationNomination: //3006
                        count = GetEvaluationCount(loggedInUserID, SOMUserGrade) + GetNominationCount(loggedInUserID, SOMUserGrade) ;
                        break;

                    case (int)SOMEmpRole.AdminHoD: //3007
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetHoDCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.AdminEvaluation: //3008
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetEvaluationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.AdminNomination: //3009
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.AdminEvaluationNomination: //3010
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetEvaluationCount(loggedInUserID, SOMUserGrade) 
                                + GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.HoDEvaluation: //3011
                        count = GetHoDCount(loggedInUserID, SOMUserGrade) + GetEvaluationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.HoDNomination: //3012
                        count = GetHoDCount(loggedInUserID, SOMUserGrade) + GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.HoDEvaluationNomination: //3013
                        count = GetHoDCount(loggedInUserID, SOMUserGrade) + GetEvaluationCount(loggedInUserID, SOMUserGrade)
                                + GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.AdminHoDEvaluation: //3014
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetHoDCount(loggedInUserID, SOMUserGrade)
                                + GetEvaluationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.AdminHoDNomination: //3015
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetHoDCount(loggedInUserID, SOMUserGrade)
                                + GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    case (int)SOMEmpRole.AdminHoDEvaluationNomination: //3016
                        count = GetAdminCount(loggedInUserID, SOMUserGrade) + GetHoDCount(loggedInUserID, SOMUserGrade)
                                + GetEvaluationCount(loggedInUserID, SOMUserGrade) + GetNominationCount(loggedInUserID, SOMUserGrade);
                        break;

                    default:
                        break;
                }
            }
            return count;
        }

        public int GetSOMEmployeeRoleByUser(EmpMaster usr)
        {
            using (objSOMEntities = new SOMEntities())
            using (objIPEntities = new IntranetPortalEntities())
            {
                int empNumber = Convert.ToInt32(usr.EmployeeNumber);
                var rptPerson = objIPEntities.EmpMasters.Where(r => r.ReportingPersonId == empNumber && r.IsActive == true).FirstOrDefault();
                var _nom = objSOMEntities.Configurations.Where(r => r.Type == "NominationUserGrades" && r.IsActive == true).FirstOrDefault();
                var _eva = objSOMEntities.Configurations.Where(r => r.Type == "EvaluationUserGrades" && r.IsActive == true).FirstOrDefault();

                var tqCHead = objSOMEntities.TQCHeads.Where(r => r.EmployeeNumber == usr.EmployeeNumber && r.IsActive == true).FirstOrDefault();
                if (tqCHead != null)
                {
                    return (int)SOMEmpRole.TQCHead; //3001
                }

                else if (usr.UserRole == 102 && (string.IsNullOrEmpty(usr.Grade) && rptPerson == null) )
                {
                    return (int)SOMEmpRole.Admin; //3002
                }

                else if (usr.UserRole == 102 && (string.IsNullOrEmpty(usr.Grade) && rptPerson != null) )
                {
                    return (int)SOMEmpRole.AdminHoD; //3007
                }

                else if (usr.UserRole == 102 && (!string.IsNullOrEmpty(usr.Grade) && rptPerson != null) )
                {
                    if ( _nom.Value.ToLower().Contains(usr.Grade.ToLower()) )
                    {
                        return (int)SOMEmpRole.AdminHoDNomination; //3015
                    }
                    else if ( _eva.Value.ToLower().Contains(usr.Grade.ToLower()) )
                    {
                        return (int)SOMEmpRole.AdminHoDEvaluation; //3014
                    }

                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()) && _nom.Value.ToLower().Contains(usr.Grade.ToLower()) )
                    {
                        return (int)SOMEmpRole.AdminHoDEvaluationNomination; //3016
                    }
                }

                else if (usr.UserRole == 102 && (!string.IsNullOrEmpty(usr.Grade) && rptPerson == null))
                {
                    if (_nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.AdminNomination; //3009
                    }
                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.AdminEvaluation; //3008
                    }

                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()) && _nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.AdminEvaluationNomination; //3010
                    }
                }

                else if (usr.UserRole == 101 && (!string.IsNullOrEmpty(usr.Grade) && rptPerson != null))
                {
                    if (_nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.HoDNomination; //3012
                    }
                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.HoDEvaluation; //3011
                    }

                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()) && _nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.HoDEvaluationNomination; //3013
                    }
                }

                else if (usr.UserRole == 101 && (!string.IsNullOrEmpty(usr.Grade) && rptPerson == null))
                {
                    if (_nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.Nomination; //3005
                    }
                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.Evaluation; //3004
                    }

                    else if (_eva.Value.ToLower().Contains(usr.Grade.ToLower()) && _nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.EvaluationNomination; //3006
                    }
                }

                else if (usr.UserRole == 101 && (string.IsNullOrEmpty(usr.Grade) && rptPerson != null))
                {
                    if (_nom.Value.ToLower().Contains(usr.Grade.ToLower()))
                    {
                        return (int)SOMEmpRole.HoD; //3003
                    }
                }

            }
            return -1;
        }

        #region getIndividualUserCount

        public int GetTQCCount(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                var Codes = new[] { (int)NominationStatus.HoD_Assign_TQC,
                                        (int)NominationStatus.Evaluators_Assign_TQC};

                var tqcUsrCount = objSOMEntities.AuditLogs.Where(r =>
                              r.IsNewAlert == true
                             && Codes.Contains((int)r.CurrentStatus)).ToList();
                if (tqcUsrCount != null)
                {
                    count = tqcUsrCount.Count;
                }
            }
            return count;
        }

        public int GetAdminCount(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                var Codes1 = new[] { (int)NominationStatus.HoD_Reject };
                var _Codes1 = objSOMEntities.AuditLogs.Where(r =>
                            r.IsNewAlert == true
                           && Codes1.Contains((int)r.CurrentStatus)).ToList();
                if (_Codes1 != null)
                {
                    count = _Codes1.Count;
                }
            }
            return count;
        }

        public int GetHoDCount(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                var Codes2 = new[] { (int)NominationStatus.Employee_Assign_HOD,
                                        (int)NominationStatus.Employee_ReAssign_HoD};

                var _Codes2 = objSOMEntities.AuditLogs.Where(r => r.DepartmentHeadID == loggedInUserID
                                && r.IsNewAlert == true
                                && Codes2.Contains((int)r.CurrentStatus)).ToList();
                if (_Codes2 != null)
                {
                    count = _Codes2.Count;
                }
            }
            return count;
        }

        public int GetEvaluationCount(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                var Codes3 = objSOMEntities.AuditLogs.Where(r => r.EvaluatorID == loggedInUserID && r.IsNewAlert == true &&
                           (r.CurrentStatus == (int)NominationStatus.TQC_Assign_Evaluator)).ToList();
                if (Codes3 != null)
                {
                    count = Codes3.Count;
                }
            }
            return count;
        }

        public int GetNominationCount(string loggedInUserID, int SOMUserGrade)
        {
            int count = 0;
            using (objSOMEntities = new SOMEntities())
            {
                var Codes4 = new[] { (int)NominationStatus.HoD_Reject,
                                        (int)NominationStatus.HoD_Assign_EmployeeClarification,
                                        (int)NominationStatus.AdminReopen,
                                        (int)NominationStatus.Evaluator_Reject};

                var _Codes4 = objSOMEntities.AuditLogs.Where(r => r.EmployeeNumber == loggedInUserID
                                    && r.IsNewAlert == true
                                    && Codes4.Contains((int)r.CurrentStatus)).ToList();
                if (_Codes4 != null)
                {
                    count = _Codes4.Count;
                }
            }
            return count;
        }

        #endregion



        public string getMenuForUser(string loggedInUserID, int SOMUserGrade)
        {
            string menu = string.Empty;
            using (objSOMEntities = new SOMEntities())
            {
                switch (SOMUserGrade)
                {
                    case (int)SOMEmpRole.TQCHead: //3001
                        menu = getPageAccessForTQCHead();
                        break;

                    case (int)SOMEmpRole.Admin: //3002
                        menu = getPageAccessForAdmin();
                        break;

                    case (int)SOMEmpRole.HoD: //3003
                        menu = getPageAccessForHoD();
                        break;

                    case (int)SOMEmpRole.Evaluation: //3004
                        menu = getPageAccessForEvaluation();
                        break;

                    case (int)SOMEmpRole.Nomination: //3005
                        menu = getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.EvaluationNomination: //3006
                        menu = getPageAccessForEvaluation() + getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.AdminHoD: //3007
                        menu = getPageAccessForAdmin() + getPageAccessForHoD();
                        break;

                    case (int)SOMEmpRole.AdminEvaluation: //3008
                        menu = getPageAccessForAdmin() + getPageAccessForEvaluation();
                        break;

                    case (int)SOMEmpRole.AdminNomination: //3009
                        menu = getPageAccessForAdmin() + getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.AdminEvaluationNomination: //3010
                        menu = getPageAccessForAdmin() + getPageAccessForEvaluation()
                                + getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.HoDEvaluation: //3011
                        menu = getPageAccessForHoD() + getPageAccessForEvaluation();
                        break;

                    case (int)SOMEmpRole.HoDNomination: //3012
                        menu = getPageAccessForHoD() + getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.HoDEvaluationNomination: //3013
                        menu = getPageAccessForHoD() + getPageAccessForEvaluation()
                                + getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.AdminHoDEvaluation: //3014
                        menu = getPageAccessForAdmin() + getPageAccessForHoD()
                                + getPageAccessForEvaluation();
                        break;

                    case (int)SOMEmpRole.AdminHoDNomination: //3015
                        menu = getPageAccessForAdmin() + getPageAccessForHoD()
                                + getPageAccessForNomination();
                        break;

                    case (int)SOMEmpRole.AdminHoDEvaluationNomination: //3016
                        menu = getPageAccessForAdmin() + getPageAccessForHoD()
                                + getPageAccessForEvaluation() + getPageAccessForNomination();
                        break;

                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(menu))
            {
                // remove Duplicates values from List
                string[] a = menu.Split(',');
                var b = new HashSet<string>(a);
                menu = String.Join(", ", b);
            }
            return menu;
        }

        #region GetPage Access individual

        public string getPageAccessForTQCHead()
        {
            using (objSOMEntities = new SOMEntities())
            {
                var objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "TQCHeadUserRole" && r.IsActive == true).FirstOrDefault();
                if (objConfig != null)
                {
                    return objConfig.Value;
                }
                else
                {
                    return "";
                }
            }
        }

        public string getPageAccessForAdmin()
        {
            using (objSOMEntities = new SOMEntities())
            {
                var objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "AdminUserRole" && r.IsActive == true).FirstOrDefault();
                if (objConfig != null)
                {
                    return objConfig.Value;
                }
                else
                {
                    return "";
                }
            }
        }

        public string getPageAccessForHoD()
        {
            using (objSOMEntities = new SOMEntities())
            {
                var objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "HoDUserRole" && r.IsActive == true).FirstOrDefault();
                if (objConfig != null)
                {
                    return objConfig.Value;
                }
                else
                {
                    return "";
                }
            }
        }

        public string getPageAccessForEvaluation()
        {
            using (objSOMEntities = new SOMEntities())
            {
                var objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "EvaluationUserRole" && r.IsActive == true).FirstOrDefault();
                if (objConfig != null)
                {
                    return objConfig.Value;
                }
                else
                {
                    return "";
                }
            }
        }

        public string getPageAccessForNomination()
        {
            using (objSOMEntities = new SOMEntities())
            {
                var objConfig = objSOMEntities.Configurations.Where(r => r.Module == "USERRole" && r.Type == "NominationUserRole" && r.IsActive == true).FirstOrDefault();
                if (objConfig != null)
                {
                    return objConfig.Value;
                }
                else
                {
                    return "";
                }
            }
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
