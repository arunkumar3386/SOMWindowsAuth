using Helper;
using Newtonsoft.Json;
using StarMonthAuth.ActionFilter;
//using StarOfTheMonth.ActionFilter;
using StarOfTheMonth.Models;
using StarOfTheMonth.Repo;
using System;
using System.Data;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace StarMonthAuth.Controllers
{
    [CheckSessionIsAvailable]
    public class DashboardController : Controller
    {
        private INominationRepo nominationRepo;
        //private IEvaluationRepo evaluationRepo;
        private ILoginRepo loginRepo;
        private RepositoryResponse repoResponse;
        private INotificationRepo notifyRepo;

        public ActionResult Index(string dept = "", string date = "")
        {
            DashboardModel model = new DashboardModel();
            ParticipatedCount partModel = new ParticipatedCount();
            repoResponse = new RepositoryResponse();
            nominationRepo = new NominationRepo();
            loginRepo = new LoginRepo();
            int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            string _dept = System.Web.HttpContext.Current.Session["UserDepartment"].ToString();

            var monthFilter = loginRepo.GetMonthYearFilter();
            var deptFilter = loginRepo.GetDepartmentDetails();

            model.Nom_DateFilterlst = monthFilter;
            model.Nom_DeptFilterlst = deptFilter;


            if (dept == "--Select--")
            {
                dept = "";
            }
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("MMMM yyyy");
                model.Nom_dateFilter = date;
            }
            else if (!string.IsNullOrEmpty(date))
            {
                string[] monthYear = date.Split(' ');
                string month = monthYear[0].Substring(0, 3);
                string year = monthYear[1];
                date = month + "-" + year;
            }            
           
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            if (string.IsNullOrEmpty(dept))
            {
                dept = System.Web.HttpContext.Current.Session["UserDepartment"].ToString();
                model.Nom_DeptFilter = dept;
            }
            repoResponse = nominationRepo.GetReportDetails(_loggedInUserID, dept, date);
            
            if (repoResponse.success)
            {
                partModel = repoResponse.Data;
                model.participatedCount = partModel;
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult LoadDashboardDetailsForFilter(string dept = "", string date = "")
        {
            repoResponse = new RepositoryResponse();
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
            nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            repoResponse = nominationRepo.GetReportDetails(_loggedInUserID, dept, date);
            if (repoResponse.success)
            {
                ParticipatedCount partModel = new ParticipatedCount();
                partModel = repoResponse.Data;
                //var _sa = new JsonSerializerSettings();
                //return Json(new { data = partModel });

                return Json(new { success = repoResponse.success, message = partModel });

            }
            else
            {
                return Json(new { success = repoResponse.success, message = repoResponse.message });
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reports(string month="", string year ="")
        {
            nominationRepo = new NominationRepo();
            repoResponse = new RepositoryResponse();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            repoResponse = nominationRepo.GetReportDetails(_loggedInUserID, month, year);
            if (repoResponse.success)
            {
                ParticipatedCount _model = new ParticipatedCount();
                _model = repoResponse.Data;
                return View(_model);
            }
            return Json(new { success = repoResponse.success, message = repoResponse.message });
        }

        public JsonResult LoadNewAltertForBellICon()
        {
            notifyRepo = new NotificationRepo();
            repoResponse = new RepositoryResponse();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            repoResponse = notifyRepo.GetNotificationsByEmployeeNumberTOP3(_loggedInUserID);

            var sa = new JsonSerializerSettings();
            return Json(repoResponse.Data, JsonRequestBehavior.AllowGet);

            //return Json(new { success = repoResponse.success, message = repoResponse.message, Data = repoResponse.Data });
        }

        public ActionResult LoadAllAlert()
        {
            notifyRepo = new NotificationRepo();
            repoResponse = new RepositoryResponse();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            repoResponse = notifyRepo.GetNotificationsByEmployeeNumber(_loggedInUserID);

            return Json(new { success = repoResponse.success, message = repoResponse.message, Data = repoResponse.Data });
        }
    }
}