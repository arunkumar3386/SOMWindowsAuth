using Newtonsoft.Json;
using StarMonthAuth.ActionFilter;
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
    [CheckSessionIsAvailable]
    public class ConfigurationController : Controller
    {
        private IConfigurationRepo _ConfigurationRepo;
        private ILoginRepo _loginRepo;
        RepositoryResponse _repoResponse;

        // GET: TQCHead
        public ActionResult Index()
        {
            ConfigurationModel model = new ConfigurationModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadConfigurationDetailsForGrid()
        {
            _repoResponse = new RepositoryResponse();
          
            _ConfigurationRepo = new ConfigurationRepo();
            _repoResponse = _ConfigurationRepo.LoadConfigurationGridData();
            if (_repoResponse.success)
            {
                var _sa = new JsonSerializerSettings();
                return Json(_repoResponse.Data);
            }
            else
            {
                return Json(new { success = _repoResponse.success, message = _repoResponse.message });
            }
        }

        [HttpPost]
        public ActionResult ConfigurationPopupValues(string ID)
        {
            long _id = long.Parse(ID);
            _ConfigurationRepo = new ConfigurationRepo();
            _repoResponse = new RepositoryResponse();
            if (_id > 0)
            {
                _repoResponse = _ConfigurationRepo.LoadConfigurationDetailsByID(_id);
            }

            if (_repoResponse.success == true)
            {
                ConfigurationModel model = new ConfigurationModel();
                model = _repoResponse.Data;
                return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
            }
            else
            {
                EvaluationModel model = new EvaluationModel();
                return Json(new { success = _repoResponse.success, message = _repoResponse.message, data = model });
            }
        }

        [HttpPost]
        public ActionResult ConfigurationPopupSave(ConfigurationModel model)
        {
            if (ModelState.IsValid)
            {
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _ConfigurationRepo = new ConfigurationRepo();
                _repoResponse = new RepositoryResponse();
                _repoResponse = _ConfigurationRepo.AddOrEditConfigurationDetails(model, _loggedInUserID);

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

                string _message = string.Join("<br/>", _message1);

                return Json(new { success = false, message = _message });
            }
      
        }

    }
}