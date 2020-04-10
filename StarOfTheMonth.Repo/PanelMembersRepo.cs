using Helper;
using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace StarOfTheMonth.Repo
{
    public interface IPanelMembersRepo : IDisposable
    {
        RepositoryResponse LoadPanelMembers(string dept = "", string date = "");
        RepositoryResponse GetPanelMembersByID(long ID);
        RepositoryResponse AddOrEditPanelMembersDetails(PanelMembersModel model, string _loggedInUserID);
        RepositoryResponse AddPanelMembers(string data, string _loggedInUserID);
        List<AutoCompleteBox> GetPanelMembersDetailsByEmpName(bool isRequiredDept);

        IEnumerable<SelectListItem> LoadListofMonth(string selectedMonth);
        IEnumerable<SelectListItem> LoadListofFutureYears(int count, string SelectedYear);
    }


    public class PanelMembersRepo : IPanelMembersRepo
    {
        private SOMEntities objSOMEntities;
        private IntranetPortalEntities objIPEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;

        public RepositoryResponse LoadPanelMembers(string dept, string date)
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
                List<PanelMembersModel> _lstPanelMembers = new List<PanelMembersModel>();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var lstPanelMembers = (from a in objIPEntities.EmpMasters.AsEnumerable()
                                           join b in objSOMEntities.EvaluatorAvailabilities.AsEnumerable() on a.EmployeeNumber equals b.EvaluatorID.ToString()
                                           where b.IsActive == true
                                           select new
                                           {
                                               a.EmployeeNumber,
                                               a.EmployeeName,
                                               a.Department,
                                               b.Month,
                                               b.Year,
                                               b.IsActive,
                                               b.EvaluatorID,
                                               b.ID
                                           }).OrderByDescending(r => r.ID).ToList();

                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        lstPanelMembers = lstPanelMembers.Where(r => r.Department == dept).ToList();
                    }
                    if (!string.IsNullOrEmpty(subMonth) && !string.IsNullOrEmpty(subYear))
                    {
                        lstPanelMembers = lstPanelMembers.Where(r => r.Month == subMonth && r.Year == subYear).ToList();
                    }
                    int i = 1;
                    foreach (var item in lstPanelMembers)
                    {

                        PanelMembersModel _panelMembers = new PanelMembersModel();
                        _panelMembers.ID = item.ID;
                        _panelMembers.SlNo = i;
                        _panelMembers.EvaluatorID = item.EmployeeNumber;
                        _panelMembers.Name = item.EmployeeName;
                        _panelMembers.Department = item.Department;
                        _panelMembers.Month = item.Month;
                        _panelMembers.Year = item.Year;
                        _lstPanelMembers.Add(_panelMembers);
                        i++;
                    }

                    baseModel = new RepositoryResponse { success = true, message = "Get Panel member details Successfully", Data = _lstPanelMembers };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }

        public RepositoryResponse GetPanelMembersByID(long ID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                PanelMembersModel model = new PanelMembersModel();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    var data = (from a in objIPEntities.EmpMasters.AsEnumerable()
                                join b in objSOMEntities.EvaluatorAvailabilities.AsEnumerable() on a.EmployeeNumber equals b.EvaluatorID.ToString()
                                where b.ID == ID && b.IsActive == true
                                select new { a, b }).FirstOrDefault();

                    //var data = objSOMEntities.Nominations.Where(r => r.ID == ID && r.IsActive == true).FirstOrDefault();
                    if (data != null)
                    {

                        model.MonthFilterlst = LoadListofMonth(data.b.Month);
                        model.YearFilterlst = LoadListofFutureYears(5, data.b.Year);
                        model.ID = data.b.ID;
                        model.EvaluatorID = data.b.EvaluatorID;
                        model.Name = data.a.EmployeeName;
                        model.MonthFilter = data.b.Month;
                        model.YearFilter = data.b.Year;

                        baseModel = new RepositoryResponse { success = true, message = "Get Panel Members details Successfully", Data = model };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = false, message = "Unable to get Panel Members details", Data = model };
                    }

                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }

        public IEnumerable<SelectListItem> LoadListofFutureYears(int count, string SelectedYear)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            var currentYear = DateTime.Today.Year;
            for (int i = count; i >= 0; i--)
            {
                SelectListItem objSelectListItem = new SelectListItem();
                string _year = (currentYear + i).ToString();
                objSelectListItem.Text = _year;//its displayed in UI
                objSelectListItem.Value = _year;
                if (!string.IsNullOrEmpty(SelectedYear) && SelectedYear == _year)
                {
                    objSelectListItem.Selected = true;
                }
                lstSelect.Add(objSelectListItem);
            }
            return lstSelect;
        }

        public IEnumerable<SelectListItem> LoadListofMonth(string selectedMonth)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            foreach (var item in months)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    SelectListItem objSelectListItem = new SelectListItem();
                    string _month = item.Substring(0, 3);
                    objSelectListItem.Text = _month;//its displayed in UI
                    objSelectListItem.Value = _month;
                    if (!string.IsNullOrEmpty(selectedMonth) && selectedMonth == _month)
                    {
                        objSelectListItem.Selected = true;
                    }
                    lstSelect.Add(objSelectListItem);
                }
            }
            return lstSelect;
        }

        public RepositoryResponse AddOrEditPanelMembersDetails(PanelMembersModel model, string _loggedInUserID)
        {
            baseModel = new RepositoryResponse();
            EvaluatorAvailability dbModel;
            try
            {
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    DateTime now = DateTime.Now;
                    dbModel = new EvaluatorAvailability();
                    var usr = objIPEntities.EmpMasters.Where(r => r.EmployeeNumber == model.EvaluatorID.ToString()).FirstOrDefault();
                    dbModel = objSOMEntities.EvaluatorAvailabilities.Where(r => r.ID == model.ID && r.Month == model.MonthFilter && r.Year == model.YearFilter).FirstOrDefault();
                    if (dbModel != null)
                    {
                        dbModel.EvaluatorID = usr.EmployeeNumber;
                        dbModel.Month = model.MonthFilter;
                        dbModel.Year = model.YearFilter;
                        dbModel.IsActive = true;
                        dbModel.ModifiedBy = _loggedInUserID;
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Panel Members Details updated Successfully" };
                    }
                    else
                    {
                        dbModel = new EvaluatorAvailability();
                        dbModel.EvaluatorID = usr.EmployeeNumber;
                        dbModel.Month = model.MonthFilter;
                        dbModel.Year = model.YearFilter;
                        dbModel.IsActive = true;
                        dbModel.CreatedBy = _loggedInUserID;
                        objSOMEntities.EvaluatorAvailabilities.Add(dbModel);
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "Panel Members Details added Successfully" };
                    }
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }

        public RepositoryResponse AddPanelMembers(string data, string _loggedInUserID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    string[] _dataStr = data.Split(',');
                    foreach (var item in _dataStr)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {                            
                            string month = Assistant.GetMonthFromCurrentDate();
                            string year = Assistant.GetYearFromCurrentDate();

                            using (objSOMEntities = new SOMEntities())
                            {
                                var _details = objSOMEntities.EvaluatorAvailabilities.Where(r => r.EvaluatorID == item && r.IsActive == true &&
                                                r.Month == month && r.Year == year).FirstOrDefault();
                                if (_details == null)
                                {
                                    EvaluatorAvailability dbModel = new EvaluatorAvailability();
                                    dbModel.EvaluatorID = item;
                                    dbModel.CreatedBy = _loggedInUserID;
                                    dbModel.IsActive = true;
                                    dbModel.Month = month;
                                    dbModel.Year = year;
                                    objSOMEntities.EvaluatorAvailabilities.Add(dbModel);
                                    objSOMEntities.SaveChanges();
                                }
                               
                            }
                            baseModel = new RepositoryResponse { success = true, message = "Panel Members Added Successfully" };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public List<AutoCompleteBox> GetPanelMembersDetailsByEmpName(bool isRequiredDept)
        {
            List<AutoCompleteBox> lstAutoCompBox = new List<AutoCompleteBox>();

            string lstValue = string.Empty;
            string[] valu;
            using (objSOMEntities = new SOMEntities())
            {
                lstValue = objSOMEntities.Configurations.Where(r => r.Module == "SOM" && r.Type == "EvaluationUserGrades" && r.IsActive == true)
                    .Select(r=>r.Value).FirstOrDefault();
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
