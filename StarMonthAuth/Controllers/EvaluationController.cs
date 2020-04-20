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
    public class EvaluationController : Controller
    {
        private IEvaluationRepo _evaluationRepo;
        private ILoginRepo _loginRepo;
        private RepositoryResponse _repoResponse;
        private string _message = string.Empty;

        // GET: Evaluation
        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            _loginRepo = new LoginRepo();
            //string _dept = System.Web.HttpContext.Current.Session["UserDepartment"].ToString();
            List<SelectListItem> deptFilter = new List<SelectListItem>();
           // int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            deptFilter = _loginRepo.GetDepartmentDetails();
            //if (_empSOMRole == (int)SOMEmpRole.TQCHead)
            //{
            //    deptFilter = _loginRepo.GetDepartmentDetails();
            //}
            //else
            //{
            //    deptFilter = _loginRepo.GetDepartmentDetails(_dept, "");
            //}
            model.DeptFilterlst = deptFilter;
            model.From_Date = DateTime.Now.ToString("MMMM yyyy");
            model.To_Date = DateTime.Now.ToString("MMMM yyyy");

            return View(model);
        }

        [HttpPost]
        public JsonResult LoadEvaluationDetailsForGrid(string dept = "", string fromDate = "", string toDate = "")
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            if (dept == "--Select--")
            {
                dept = "";
            }
            if (fromDate == "--Select--")
            {
                fromDate = "";
            }
            else if (!string.IsNullOrEmpty(fromDate))
            {
                string[] monthYear = fromDate.Split(' ');
                string month = monthYear[0].Substring(0, 3);
                string year = monthYear[1];
                fromDate = month + "-" + year;
            }

            if (toDate == "--Select--")
            {
                toDate = "";
            }
            else if (!string.IsNullOrEmpty(toDate))
            {
                string[] monthYear = toDate.Split(' ');
                string month = monthYear[0].Substring(0, 3);
                string year = monthYear[1];
                toDate = month + "-" + year;
            }
            _repoResponse = new RepositoryResponse();
            _evaluationRepo = new EvaluationRepo();
            _repoResponse = _evaluationRepo.LoadAllEvaluationData(_loggedInUserID, dept, fromDate, toDate);
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

        public ActionResult EvaluationDataExport()
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _evaluationRepo = new EvaluationRepo();
            _repoResponse = new RepositoryResponse();
            _repoResponse = _evaluationRepo.LoadAllEvaluationData(_loggedInUserID);

            if (_repoResponse.success)
            {
                DataSet ds = Assistant.ToDataSet(_repoResponse.Data);
                string path = HostingEnvironment.MapPath("~/bin/Pdf/");
                string fName = "SOM_EvaluationDetails_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (Assistant.DatasetToExcelExport(ds, path, fName, 20, "EvaluationDetails")) // 4th parameter is field Count
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path + "" + fName);

                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fName);
                }
            }
            return Json(new { success = _repoResponse.success.ToString(), message = _repoResponse.message });
        }

        [HttpPost]
        public ActionResult EvaluationPopupValues(string ID)
        {
            long _id = long.Parse(ID);
            _evaluationRepo = new EvaluationRepo();
            _repoResponse = new RepositoryResponse();
            if (_id > 0)
            {
                _repoResponse = _evaluationRepo.LoadEvaluationDataByID(_id);
            }
            //else
            //{
            //    _repoResponse = _evaluationRepo.LoadEmpNominationDetails(_id, long.Parse(nominationID), empNum);
            //}

            if (_repoResponse.success == true)
            {
                EvaluationModel model = new EvaluationModel();
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
        public ActionResult EvaluationPopupSave(DashboardModel model)
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            model.evaluationModel.EvaluatorID = _loggedInUserID;
            //model.EvaluatorNumber = _loggedInUserID;
            //model.EvaluatorName = "test"; //work arround
            if (ModelState.IsValid)
            {
                _evaluationRepo = new EvaluationRepo();
                _repoResponse = new RepositoryResponse();
                _repoResponse = _evaluationRepo.AddorUpdateEvaluationData(model.evaluationModel, _loggedInUserID, false);

                _loginRepo = new LoginRepo();
                int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                int count = _loginRepo.getActionCounts(_loggedInUserID, _empSOMRole);
                HttpContext.Session["NotifyCount"] = count;

                return Json(new { success = _repoResponse.success, message = _repoResponse.message });
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

                return Json(new { success = false, message = _message1 });
            }
        }

        [HttpPost]
        public ActionResult EvaluationPopupSubmit(DashboardModel model)
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            model.evaluationModel.EvaluatorID = _loggedInUserID;
            if (ModelState.IsValid)
            {
                _evaluationRepo = new EvaluationRepo();
                _repoResponse = new RepositoryResponse();
                _repoResponse = _evaluationRepo.AddorUpdateEvaluationData(model.evaluationModel, _loggedInUserID, true);

                _loginRepo = new LoginRepo();
                int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                int count = _loginRepo.getActionCounts(_loggedInUserID, _empSOMRole);
                HttpContext.Session["NotifyCount"] = count;

                return Json(new { success = _repoResponse.success, message = _repoResponse.message });
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

                return Json(new { success = false, message = _message1 });
            }
        }

        public ActionResult Evaluationlst()
        {
            DashboardModel model = new DashboardModel();
            _loginRepo = new LoginRepo();
            //string _dept = System.Web.HttpContext.Current.Session["UserDepartment"].ToString();

            List<SelectListItem> deptFilter = new List<SelectListItem>();
            //int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            //if (_empSOMRole == (int)SOMEmpRole.TQCHead)
            //{
            //    deptFilter = _loginRepo.GetDepartmentDetails();
            //}
            //else
            //{
            //    deptFilter = _loginRepo.GetDepartmentDetails(_dept, "");
            //}    
            deptFilter = _loginRepo.GetDepartmentDetails();
            model.DeptFilterlst = deptFilter;

            model.From_Date = DateTime.Now.ToString("MMMM yyyy");
            model.To_Date = DateTime.Now.ToString("MMMM yyyy");
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadAllEvaluationDetailsForGrid(string dept = "", string fromDate = "", string toDate = "")
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            if (dept == "--Select--")
            {
                dept = "";
            }
            if (fromDate == "--Select--")
            {
                fromDate = "";
            }
            else if (!string.IsNullOrEmpty(fromDate))
            {
                string[] monthYear = fromDate.Split(' ');
                string month = monthYear[0].Substring(0, 3);
                string year = monthYear[1];
                fromDate = month + "-" + year;
            }

            if (toDate == "--Select--")
            {
                toDate = "";
            }
            else if (!string.IsNullOrEmpty(toDate))
            {
                string[] monthYear = toDate.Split(' ');
                string month = monthYear[0].Substring(0, 3);
                string year = monthYear[1];
                toDate = month + "-" + year;
            }
            _repoResponse = new RepositoryResponse();
            _evaluationRepo = new EvaluationRepo();
            _repoResponse = _evaluationRepo.LoadAllEvaluationData_Alltime(_loggedInUserID, dept, fromDate, toDate);
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
    }
}