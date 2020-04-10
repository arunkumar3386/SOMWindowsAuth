using Newtonsoft.Json;
//using StarOfTheMonth.ActionFilter;
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
    public class TQCHeadController : Controller
    {
        private ITQCHeadRepo _TQCHeadRepo;
        private ILoginRepo _loginRepo;
        RepositoryResponse _repoResponse;
        string _message = string.Empty;

        // GET: TQCHead
        public ActionResult Index()
        {
            TQCHeadModel model = new TQCHeadModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadAllTQCHeadDatas(string dept = "", string date = "")
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
            _TQCHeadRepo = new TQCHeadRepo();
            _repoResponse = _TQCHeadRepo.GetAllTQCHeadDatas(dept, date);
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
        public ActionResult LoadTQCHeadDetailsByID(string ID)
        {
            long _id = long.Parse(ID);
            _TQCHeadRepo = new TQCHeadRepo();
            _repoResponse = new RepositoryResponse();
            if (_id > 0)
            {
                _repoResponse = _TQCHeadRepo.GetTQCHeadDetailsByID(_id);
            }

            if (_repoResponse.success == true)
            {
                TQCHeadModel model = new TQCHeadModel();
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
        public ActionResult SaveTQCHeadDetails(TQCHeadModel model)
        {

            if (ModelState.IsValid)
            {
                _TQCHeadRepo = new TQCHeadRepo();
                _repoResponse = new RepositoryResponse();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _TQCHeadRepo.AddOrEditTQCHeadDetails(model, _loggedInUserID);
                //if (_repoResponse.success)
                //{
                //    return View();
                //}
                return Json(new { success = _repoResponse.success, message = _repoResponse.message });
            }
            else
            {
                List<string> fieldOrder = new List<string>(new string[] {
                                 "Name","StartDate","EndDate"  })
                   .Select(f => f.ToLower()).ToList();

                var _message1 = ModelState
                    .Select(m => new { Order = fieldOrder.IndexOf(m.Key.ToLower()), Error = m.Value })
                    .OrderBy(m => m.Order)
                    .SelectMany(m => m.Error.Errors.Select(e => e.ErrorMessage)).ToList();

                _message = string.Join("<br/>", _message1);

                return Json(new { success = false, message = _message });
            }

         
        }

    }
}