﻿using StarOfTheMonth.Models;
using StarOfTheMonth.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMonthAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Access(string returnUrl = "")
        {
            if (Request.IsAuthenticated)
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
                Session["User"] = loggedInuser;

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

                //loggedInuser = "Jerome"; //TQC Head
                //loggedInuser = "KS.Suseel"; //Admin

                //loggedInuser = "d.kalpanadevi";//Nomination User
                //loggedInuser = "N.Vasudevan";//DH
                //loggedInuser = "d.kalpanadevi";
               // loggedInuser = "Shakir";

                ILoginRepo loginRepo = new LoginRepo();
                RepositoryResponse model = loginRepo.GetLoginUserDetails(loggedInuser);

                if (model.success)
                {
                    EmpMasterModel _orGModel = model.Data;
                    if (_orGModel != null)
                    {
                        //RepositoryResponse _model = loginRepo.GetPageAccessListByUserGrade(_orGModel.Grade, loggedInuser);
                        //if (_model != null)
                        //{
                        //    Session.Add("pageAccessList", _model.Data);
                        //}
                        if (string.IsNullOrEmpty(_orGModel.ImagePath))
                        {
                            Session.Add("UserImage", "/Images/UserImages/user-1.jpg");
                        }
                        else
                        {
                            Session.Add("UserImage", _orGModel.ImagePath);
                        }
                        
                        //Session.Add("UserImage", "/Images/UserImages/user-1.jpg");
                        Session.Add("UserName", _orGModel.UserName);
                        Session.Add("UserFullName", _orGModel.EmployeeName);
                        Session.Add("UserID", _orGModel.EmployeeNumber);
                        Session.Add("UserDepartment", _orGModel.Department);
                        Session.Add("UserGrade", _orGModel.Grade.ToString());
                    }

                    int empRole = 0;
                    RepositoryResponse _model1 = loginRepo.GetUserDetailsByUserID(_orGModel.EmployeeNumber);
                    if (_model1 != null && _model1.Data != null)
                    {
                        EmpMasterModel data = _model1.Data;
                        empRole = data.EmployeeSOMRole;
                        Session.Add("EmpSOMRole", empRole.ToString());
                        Session.Add("EmpSOMRoleText", data.EmployeeSOMRoleAsString);

                        string menuNames = loginRepo.getMenuForUser(_orGModel.EmployeeNumber, empRole);
                        Session.Add("pageAccessList", menuNames);
                    }

                    //Get Notification count
                    int count = loginRepo.getActionCounts(_orGModel.EmployeeNumber, empRole);

                    Session.Add("NotifyCount", count);

                    //Get Star of the month details
                    IStarOfMonthRepo starOfTheMonth = new StarOfMonthRepo();
                    List<EmpMasterModel> _data = starOfTheMonth.GetLastThreeStarOftheMonthEmpDetails();
                    if (_data == null)
                    {
                        Session.Add("SOM_Month_Count", "0");
                    }
                    else
                    {
                        Session.Add("SOM_Month_Count", _data.Count);
                    }

                    for (int i = 0; i < _data.Count; i++)
                    {
                        Session.Add("SOM_Month_" + i, _data[i].EMPMonth);
                        Session.Add("SOM_Image_" + i, _data[i].ImagePath);
                    }

                    if (empRole == (int)SOMEmpRole.Nomination)
                    {
                        return RedirectToAction("Index", "Nomination");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult NoAccess()
        {
            return View();
        }
    }
}