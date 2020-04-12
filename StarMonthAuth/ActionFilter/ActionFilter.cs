using StarOfTheMonth.Models;
using StarOfTheMonth.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace StarMonthAuth.ActionFilter
{
    public class CheckSessionIsAvailable : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                var loggedInuser = "";
                var prinicpal = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
                if (prinicpal != null && prinicpal.Claims != null)
                {
                    if (prinicpal.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name).Any())
                    {
                        loggedInuser = prinicpal.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name).Select(c => c.Value).FirstOrDefault();
                    }
                }
                HttpContext.Current.Session.Add("loggedInuser", loggedInuser);

                loggedInuser = loggedInuser.Replace("\\", "|");

                if (loggedInuser.Contains("|"))
                {
                    loggedInuser = loggedInuser.Split('|')[1];

                }

                loggedInuser = "Sanjay"; //Nomination User -- DH --RThangaraj
                //loggedInuser = "muthu"; //Nomination User DH -- RThangaraj
                //loggedInuser = "kamudhan";//Nomination User 
                //loggedInuser = "r.thangaraj"; //DH user
                //loggedInuser = "r.thangaraj"; //DH user

                //loggedInuser = "Chandrasekar"; // Evaluation user
                //loggedInuser = "S.Karthik"; // Evaluation user

                // loggedInuser = "Jerome"; //TQC Head
                //loggedInuser = "KS.Suseel"; //Admin

                ILoginRepo loginRepo = new LoginRepo();
                RepositoryResponse model = loginRepo.GetLoginUserDetails(loggedInuser);

                if (model.success)
                {
                    EmpMasterModel _orGModel = model.Data;
                    if (_orGModel != null)
                    {
                        RepositoryResponse _model = loginRepo.GetPageAccessListByUserGrade(_orGModel.Grade, loggedInuser);
                        if (_model != null)
                        {
                            HttpContext.Current.Session.Add("pageAccessList", _model.Data);
                            //claims.Add(new Claim(ClaimTypes.Actor, _model.Data));
                        }
                    }
                    HttpContext.Current.Session.Add("UserName", _orGModel.UserName);
                    HttpContext.Current.Session.Add("UserFullName", _orGModel.EmployeeName);
                    HttpContext.Current.Session.Add("UserID", _orGModel.EmployeeNumber);
                    HttpContext.Current.Session.Add("UserDepartment", _orGModel.Department);
                    HttpContext.Current.Session.Add("UserGrade", _orGModel.Grade.ToString());

                    //claims.Add(new Claim(ClaimTypes.GivenName, _orGModel.UserName));
                    //claims.Add(new Claim(ClaimTypes.SerialNumber, _orGModel.EmployeeNumber));
                    //claims.Add(new Claim(ClaimTypes.Role, _orGModel.Department));
                    //claims.Add(new Claim(ClaimTypes.Surname, _orGModel.Grade));

                    //if (_orGModel.Grade == "A1")
                    //{
                    //    HttpContext.Current.Session.Add("UserGrade", _orGModel.Grade.ToString());
                    //    HttpContext.Current.Session.Add("UserRole", "Nomination");
                    //}
                    //else if (_orGModel.Grade == "DH")
                    //{
                    //    HttpContext.Current.Session.Add("UserGrade", _orGModel.Grade.ToString());
                    //    HttpContext.Current.Session.Add("UserRole", "DepartmentHead");
                    //}
                    //else if (_orGModel.Grade == "C1")
                    //{
                    //    HttpContext.Current.Session.Add("UserGrade", _orGModel.Grade.ToString());
                    //    HttpContext.Current.Session.Add("UserRole", "Evaluation");
                    //}
                    int empRole = 0;
                    RepositoryResponse _model1 = loginRepo.GetUserDetailsByUserID(_orGModel.EmployeeNumber);
                    if (_model1 != null && _model1.Data != null)
                    {
                        EmpMasterModel data = _model1.Data;
                        empRole = data.EmployeeSOMRole;
                        HttpContext.Current.Session.Add("EmpSOMRole", empRole.ToString());

                        //claims.Add(new Claim(ClaimTypes.StreetAddress, empRole.ToString()));
                    }

                    //Get Notification count
                    int count = loginRepo.GetCountForUser(_orGModel.EmployeeNumber, empRole);

                    HttpContext.Current.Session.Add("NotifyCount", count);
                    //claims.Add(new Claim(ClaimTypes.HomePhone, count.ToString()));

                    //if (empRole == (int)EmployeeRole.Nomination)
                    //{
                    //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Nomination" }));
                    //}
                    //else
                    //{
                    //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Dashboard" }));
                    //}

                    //Get Star of the month details
                    IStarOfMonthRepo starOfTheMonth = new StarOfMonthRepo();
                    List<EmpMasterModel> _data = starOfTheMonth.GetLastThreeStarOftheMonthEmpDetails();
                    if (_data == null)
                    {
                        HttpContext.Current.Session.Add("SOM_Month_Count", "0");
                    }
                    else
                    {
                        HttpContext.Current.Session.Add("SOM_Month_Count", _data.Count);
                    }

                    for (int i = 0; i < _data.Count; i++)
                    {
                        HttpContext.Current.Session.Add("SOM_Month_" + i, _data[i].EMPMonth);
                        HttpContext.Current.Session.Add("SOM_Image_" + i, _data[i].ImagePath);
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "NoAccess" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}