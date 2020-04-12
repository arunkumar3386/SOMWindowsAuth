using Helper;
using Newtonsoft.Json;
using StarMonthAuth.ActionFilter;
//using StarOfTheMonth.ActionFilter;
using StarOfTheMonth.Models;
using StarOfTheMonth.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;


namespace StarMonthAuth.Controllers
{
    [CheckSessionIsAvailable]
    public class PanelMembersController : Controller
    {
        private IPanelMembersRepo _panelMembersRepo;
        private ILoginRepo _loginRepo;
        RepositoryResponse _repoResponse;

        // GET: PanelMembers
        public ActionResult Index()
        {
            PanelMembersModel model = new PanelMembersModel();
            _loginRepo = new LoginRepo();
            _panelMembersRepo = new PanelMembersRepo();
            var monthFilter = _loginRepo.GetMonthYearFilter();
            var deptFilter = _loginRepo.GetDepartmentDetails();

            model.DateFilterlst = monthFilter;
            model.DeptFilterlst = deptFilter;
            model.MonthFilterlst = _panelMembersRepo.LoadListofMonth(Assistant.GetMonthFromCurrentDate());
            model.YearFilterlst = _panelMembersRepo.LoadListofFutureYears(5, Assistant.GetYearFromCurrentDate());

            return View(model);
        }

        [HttpPost]
        public JsonResult LoadPanelMembersDetailsForGrid(string dept = "", string date = "")
        {
            _repoResponse = new RepositoryResponse();
            if (dept == "--Select--")
            {
                dept = "";
            }
            if (date == "--Select--")
            {
                date = "";
            }
            _panelMembersRepo = new PanelMembersRepo();
            _repoResponse = _panelMembersRepo.LoadPanelMembers(dept, date);
            if (_repoResponse.success)
            {
                var _sa = new JsonSerializerSettings();
                return Json(_repoResponse.Data);
            }
            else
            {
                return Json(new { success = _repoResponse.success.ToString(), message = _repoResponse.message });
            }
        }

        [HttpPost]
        public ActionResult PanelMembersPopupValues(string ID)
        {
            long _id = long.Parse(ID);
            _panelMembersRepo = new PanelMembersRepo();
            _repoResponse = new RepositoryResponse();
            if (_id > 0)
            {
                _repoResponse = _panelMembersRepo.GetPanelMembersByID(_id);
            }

            if (_repoResponse.success == true)
            {
                PanelMembersModel model = new PanelMembersModel();
                model = _repoResponse.Data;
                //return JSon(model);
                return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
            }
            else
            {
                EvaluationModel model = new EvaluationModel();
                return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
            }
        }

        [HttpPost]
        public ActionResult PanelMembersPopupSave(PanelMembersModel model)
        {
            if (ModelState.IsValid)
            {
                _panelMembersRepo = new PanelMembersRepo();
                _repoResponse = new RepositoryResponse();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _panelMembersRepo.AddOrEditPanelMembersDetails(model, _loggedInUserID);
                
                return Json(new { success = _repoResponse.success, message = _repoResponse.message });
            }
            else
            {
                string _message = string.Empty;
                List<string> fieldOrder = new List<string>(new string[] {
                                 "UserName","Password"  })
                        .Select(f => f.ToLower()).ToList();

                var _message1 = ModelState
                    .Select(m => new { Order = fieldOrder.IndexOf(m.Key.ToLower()), Error = m.Value })
                    .OrderBy(m => m.Order)
                    .SelectMany(m => m.Error.Errors.Select(e => e.ErrorMessage)).ToList();

                _message = string.Join("<br/>", _message1);

                return Json(new { success = false, message = _message });
            }
           
        }

        [HttpPost]
        public ActionResult AddPanelmembers(string data, string Month, string Year)
        {
            string _message = string.Empty;
            if (string.IsNullOrEmpty(data))
            {
                _message = string.Join("<br/>", "Please Select Employee");
            }
            if (string.IsNullOrEmpty(Month))
            {
                _message = string.Join("<br/>", "Please Select Month");
            }
            if (string.IsNullOrEmpty(Year))
            {
                _message = string.Join("<br/>", "Please Select Year");
            }
            if (string.IsNullOrEmpty(_message))
            {
                _panelMembersRepo = new PanelMembersRepo();
                _repoResponse = new RepositoryResponse();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _panelMembersRepo.AddPanelMembers(data, _loggedInUserID);
                return Json(new { success = _repoResponse.success, message = _repoResponse.message });
            }
            else
            {
                return Json(new { success = false, message = _message });
            }

        }

        [HttpPost]
        public JsonResult GetEmployeeDataByName(string Prefix)
        {
            _panelMembersRepo = new PanelMembersRepo();
            List<AutoCompleteBox> lstAutoCompleteBox = _panelMembersRepo.GetPanelMembersDetailsByEmpName(true);

            var _empData = (from N in lstAutoCompleteBox
                            where N.Value.ToLower().Contains(Prefix.ToLower())
                            select new
                            {
                                label = N.Value,
                                val = N.ID
                            }).Distinct();
            return Json(_empData, JsonRequestBehavior.AllowGet);
        }

    }
}