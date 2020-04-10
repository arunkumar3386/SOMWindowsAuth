using Helper;
//using StarOfTheMonth.ActionFilter;
using StarOfTheMonth.Model.Models;
using StarOfTheMonth.Models;
using StarOfTheMonth.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMonthAuth.Controllers
{
    //[CheckSessionIsAvailable]
    public class StarOfMonthController : Controller
    {
        private IStarOfMonthRepo _statRepo;
        private ILoginRepo _loginRepo;
        RepositoryResponse _repoResponse;
        string _message = string.Empty;
        private IPanelMembersRepo _panelMembersRepo;

        // GET: StarOfMonth
        public ActionResult Index()
        {
            _loginRepo = new LoginRepo();
            _panelMembersRepo = new PanelMembersRepo();
            _statRepo = new StarOfMonthRepo();
            string sMonth = Assistant.GetMonthFromCurrentDate();
            string sYear = Assistant.GetYearFromCurrentDate();

            StarOfMonthModel model = new StarOfMonthModel();
            model.DeptFilterlst = _loginRepo.GetDepartmentDetails();
            model.DateFilterlst = _loginRepo.GetMonthYearFilter();
            model.MonthFilterlst = _panelMembersRepo.LoadListofMonth(sMonth);
            model.YearFilterlst = _panelMembersRepo.LoadListofFutureYears(5, sYear);
            //model.NominationIDlst = _statRepo.LoadNominationIDsByEmpID("");
            model.NominationIDlst = _statRepo.LoadNominationIDsEvalCompByMonthAndYear(sMonth, sYear);
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadStarOfMonthDetailsForGrid(string dept = "", string date = "")
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
            else if (!string.IsNullOrEmpty(date))
            {
                string[] monthYear = date.Split(' ');
                string month = monthYear[0].Substring(0, 3);
                string year = monthYear[1];
                date = month + "-" + year;
            }
            _statRepo = new StarOfMonthRepo();
           
            _repoResponse = _statRepo.LoadStarOfMonthDetailsForGrid(dept, date);
            if (_repoResponse.success)
            {
                var _sa = new Newtonsoft.Json.JsonSerializerSettings();
                return Json(_repoResponse.Data);
            }
            else
            {
                return Json(new { success = _repoResponse.success.ToString(), message = _repoResponse.message });
            }
        }

        // GET: StarOfMonth Details By ID

        public ActionResult GetStarOfMonthDetails(int ID = 0)
        {
            _repoResponse = new RepositoryResponse();
            StarOfMonthModel model = new StarOfMonthModel();
            if (ID > 0)
            {
                _statRepo = new StarOfMonthRepo();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _statRepo.LoadStarOfMonthDetailsByID(ID,_loggedInUserID);
                if (_repoResponse.success)
                {
                    model = _repoResponse.Data;
                    return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
                }
            }
            else if (ID == -1)
            {
                _statRepo = new StarOfMonthRepo();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _statRepo.LoadStarOfMonthDetailsByID(ID, _loggedInUserID);
                if (_repoResponse.success)
                {
                    model = _repoResponse.Data;
                    return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
                }
            }
            return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
        }

        [HttpPost]
        public ActionResult Save(StarOfMonthModel model)
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = new RepositoryResponse();
            _statRepo = new StarOfMonthRepo();

            if (ModelState.IsValid)
            {
                _repoResponse = _statRepo.AddOrEditStarOfMonth(model, _loggedInUserID);
                if (_repoResponse.success)
                {
                    _loginRepo = new LoginRepo();
                    int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                    int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
                    HttpContext.Session["NotifyCount"] = count;

                    return Json(new { success = true, message = _repoResponse.message });
                }
                else
                {
                    return Json(new { success = false, message = _repoResponse.message });
                }
            }
            else
            {
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
        public ActionResult Delete(int ID)
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = new RepositoryResponse();
            _statRepo = new StarOfMonthRepo();

            _repoResponse = _statRepo.DeleteStarOfMonthDetailsByID(ID, _loggedInUserID);
            if (_repoResponse.success)
            {
                _loginRepo = new LoginRepo();
                int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
                HttpContext.Session["NotifyCount"] = count;

                return Json(new { success = true, message = _repoResponse.message });
            }
            else
            {
                return Json(new { success = false, message = _repoResponse.message });
            }
        }

        [HttpPost]
        public JsonResult GetEmployeeDataByName(string Prefix)
        {
            _statRepo = new StarOfMonthRepo();
            List<AutoCompleteBox> lstAutoCompleteBox = _statRepo.GetStarOfMembersDetailsByEmpName(true);

            var _empData = (from N in lstAutoCompleteBox
                            where N.Value.ToLower().Contains(Prefix.ToLower())
                            select new
                            {
                                label = N.Value,
                                val = N.ID
                            }).Distinct();
            return Json(_empData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNominationDataByEmpName(string empNum)
        {
            _statRepo = new StarOfMonthRepo();
            IEnumerable<SelectListItem> lstCompleteBox = _statRepo.LoadNominationIDsByEmpID(empNum);

            //var _empData = (from N in lstCompleteBox
            //                where N.Value.ToLower().Contains(Prefix.ToLower())
            //                select new
            //                {
            //                    label = N.Value,
            //                    val = N.ID
            //                }).Distinct();
            //return Json(lstCompleteBox, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, message = lstCompleteBox });
        }

        [HttpPost]
        public JsonResult GetNominationDataByMonthYear(string month, string year)
        {
            _statRepo = new StarOfMonthRepo();
            IEnumerable<SelectListItem> lstCompleteBox = _statRepo.LoadNominationIDsEvalCompByMonthAndYear(month, year);

            //var _empData = (from N in lstCompleteBox
            //                where N.Value.ToLower().Contains(Prefix.ToLower())
            //                select new
            //                {
            //                    label = N.Value,
            //                    val = N.ID
            //                }).Distinct();
            //return Json(lstCompleteBox, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, message = lstCompleteBox });
        }
    }
}