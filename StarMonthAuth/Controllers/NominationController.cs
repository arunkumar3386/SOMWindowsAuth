using Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

using Newtonsoft.Json;

using StarOfTheMonth.Models;
using StarOfTheMonth.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;


namespace StarMonthAuth.Controllers
{
    public class NominationController : Controller
    {
        private INominationRepo _nominationRepo;
        private ILoginRepo _loginRepo;
        RepositoryResponse _repoResponse;
        string _message = string.Empty;
        //string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
        //long _loggedInUserID = 750002;


        public ActionResult Index()
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            string _dept = System.Web.HttpContext.Current.Session["UserDepartment"].ToString();
            //Assistant.ConvertHTML2PDF("", "", "");

            DashboardModel model = new DashboardModel();
            _loginRepo = new LoginRepo();

            List<SelectListItem> deptFilter = new List<SelectListItem>();

            if (_empSOMRole == (int)EmployeeRole.DepartmentHead)
            {
                deptFilter = _loginRepo.GetDepartmentDetails(_dept, _dept);
                model.Nom_dateFilter = DateTime.Now.ToString("MMMM yyyy");
            }
            else
            {
                deptFilter = _loginRepo.GetDepartmentDetails();
            }
            model.Nom_DeptFilterlst = deptFilter;

            return View(model);
        }

        [HttpPost]
        public JsonResult LoadNominationDetailsForGrid(string dept = "", string date = "", string toDate = "")
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
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.LoadNomination(_loggedInUserID, dept, date, toDate);
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
        public JsonResult LoadNominationRejectDetailsForGrid(string dept = "", string date = "")
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
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.LoadNomination(_loggedInUserID, dept, date, "", true);
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

        public ActionResult NominationDataExport()
        {
            _repoResponse = new RepositoryResponse();
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.LoadNomination(_loggedInUserID, "");
            if (_repoResponse.success)
            {
                DataSet ds = Assistant.ToDataSet(_repoResponse.Data);
                string path = HostingEnvironment.MapPath("~/bin/Pdf/");
                string fName = "SOM_NominationDetails_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (Assistant.DatasetToExcelExport(ds, path, fName, 5, "NominationDetails")) // 4th parameter is field Count
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path + "" + fName);

                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fName);
                }
            }
            return Json(new { success = _repoResponse.success, message = _repoResponse.message });
        }

        // GET: Nomination
        //[HttpGet]
        public ActionResult Nomination(long ID = 0)
        {
            _repoResponse = new RepositoryResponse();
            NominationModel model = new NominationModel();

            if (ID > 0)
            {
                string subPath = "~/Uploads/nominationID/";
                string fpath = string.Empty;
                bool exists = System.IO.Directory.Exists(subPath);
                if (exists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                }
                fpath = Server.MapPath(subPath);

                _nominationRepo = new NominationRepo();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _nominationRepo.GetNominationByID(_loggedInUserID, ID, fpath);
                if (_repoResponse.success)
                {
                    model = _repoResponse.Data;

                    List<SelectListItem> lstItem = new List<SelectListItem>();

                    SelectListItem s1 = new SelectListItem();
                    s1.Text = "Cost(in Lakhs)";
                    s1.Value = "1";
                    lstItem.Add(s1);

                    SelectListItem s2 = new SelectListItem();
                    s2.Text = "Time(in hours)";
                    s2.Value = "2";
                    lstItem.Add(s2);

                    SelectListItem s3 = new SelectListItem();
                    s3.Text = "Paper(in Sheets)";
                    s3.Value = "3";
                    lstItem.Add(s3);
                    model.Savings = lstItem;

                    return View(model);
                }
            }
            else
            {
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                if (!string.IsNullOrEmpty(_loggedInUserID))
                {
                    _nominationRepo = new NominationRepo();
                    _repoResponse = new RepositoryResponse();
                    _repoResponse = _nominationRepo.NominationRestrictionCount(_loggedInUserID);

                    if (!_repoResponse.success)
                    {
                        model.NominationMessage = _repoResponse.message;
                    }
                }


                model.Name = System.Web.HttpContext.Current.Session["UserFullName"].ToString();
                model.EmployeeNumber = System.Web.HttpContext.Current.Session["UserID"].ToString();
                model.Department = System.Web.HttpContext.Current.Session["UserDepartment"].ToString();

                List<SelectListItem> lstItem = new List<SelectListItem>();

                SelectListItem s1 = new SelectListItem();
                s1.Text = "Cost(in Lakhs)";
                s1.Value = "1";
                lstItem.Add(s1);

                SelectListItem s2 = new SelectListItem();
                s2.Text = "Time(in hours)";
                s2.Value = "2";
                lstItem.Add(s2);

                SelectListItem s3 = new SelectListItem();
                s3.Text = "Paper(in Sheets)";
                s3.Value = "3";
                lstItem.Add(s3);
                model.Savings = lstItem;

                return View(model);
            }
            return Json(new { success = _repoResponse.success.ToString(), message = _repoResponse.message });
        }



        [HttpPost]
        public ActionResult Save(NominationModel model, HttpPostedFileBase[] files)
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = new RepositoryResponse();
            _nominationRepo = new NominationRepo();

            if (string.IsNullOrEmpty(model.NominationID))
            {
                _repoResponse = _nominationRepo.NominationRestrictionCount(_loggedInUserID);
                if (!_repoResponse.success)
                {
                    return Json(new { success = _repoResponse.success, message = _repoResponse.message });
                }
            }

            if (ModelState.IsValid)
            {
                _loginRepo = new LoginRepo();
                //FileUpload(files);

                _repoResponse = _nominationRepo.AddOrEditNominationDetails(_loggedInUserID, model);

                if (_repoResponse.success)
                {
                    _loginRepo = new LoginRepo();
                    int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                    int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
                    HttpContext.Session["NotifyCount"] = count;

                    return Json(new { success = true, message = _repoResponse.message, Data = _repoResponse.Data });
                }
                else
                {
                    return Json(new { success = false, message = _repoResponse.message, Data = _repoResponse.Data });
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

        public ActionResult GeneratePDF(string ID)
        {
            //ID = "1";
            _nominationRepo = new NominationRepo();
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                string subPath = "~/HTMLFiles/";
                string path = Server.MapPath(subPath) + "NominationHTMLFormat.html";
                string PDFBody = System.IO.File.ReadAllText(path);
                //string PDFBody = System.IO.File.ReadAllText(@"D:\Personal Work\Project GitHub\StarOfTheMonth\StarOfTheMonth\HTMLFiles\NominationHTMLFormat.html");
                string data = _nominationRepo.GetNomineeContentsForPDFExport(PDFBody, ID);
                StringReader sr = new StringReader(data);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "NominationForm_" + ID + ".pdf");
            }


            //// instantiate the html to pdf converter
            //HtmlToPdf converter = new HtmlToPdf();

            //// convert the url to pdf
            //PdfDocument doc = converter.ConvertUrl("http://localhost:50967/Nom?id=1");

            //// save pdf document
            //doc.Save(@"D:\Personal Work\Documents\PDF Export\file.pdf");

            //// close pdf document
            //doc.Close();

            ////References : http://www.intstrings.com/ramivemula/articles/export-asp-net-mvc-view-to-pdf-in-3-quick-steps/
            ////return new Rotativa.ActionAsPdf("/Nomination/Nomination?id=1");
            //return new Rotativa.ActionAsPdf("Nom?id=1");
            ////return new Rotativa.ActionAsPdf("http://localhost:50967/Nomination/Nomination?id=1");

            ////// instantiate the html to pdf converter
            ////HtmlToPdf converter = new HtmlToPdf();

            ////// convert the url to pdf
            ////PdfDocument doc = converter.ConvertUrl(url);

            ////// save pdf document
            ////doc.Save(file);

            ////// close pdf document
            ////doc.Close();

            ////// instantiate the html to pdf converter
            ////HtmlToPdf converter = new HtmlToPdf();

            ////// convert the url to pdf
            ////PdfDocument doc = converter.ConvertUrl(url);

            ////// save pdf document
            ////doc.Save(file);

            ////// close pdf document
            ////doc.Close();
        }

        [HttpPost]
        public ActionResult Submit(NominationModel model, HttpPostedFileBase[] files)
        {
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = new RepositoryResponse();
            _nominationRepo = new NominationRepo();

            if (string.IsNullOrEmpty(model.NominationID))
            {
                _repoResponse = _nominationRepo.NominationRestrictionCount(_loggedInUserID);
                if (!_repoResponse.success)
                {
                    return Json(new { success = _repoResponse.success, message = _repoResponse.message });
                }
            }

            if (ModelState.IsValid)
            {
                //FileUpload(files);
                _repoResponse = _nominationRepo.AddOrEditNominationDetails(_loggedInUserID, model, true);

                _loginRepo = new LoginRepo();
                int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
                HttpContext.Session["NotifyCount"] = count;

                return Json(new { success = _repoResponse.success, message = _repoResponse.message, Data = _repoResponse.Data });
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
        public JsonResult GetEmployeeDataByName(string Prefix)
        {
            _loginRepo = new LoginRepo();
            List<AutoCompleteBox> lstAutoCompleteBox = _loginRepo.GetEmpDetailsByEmpName(true);

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
        public JsonResult GetEmployeeDataByEmpNumber(string Prefix)
        {
            _loginRepo = new LoginRepo();
            List<AutoCompleteBox> lstAutoCompleteBox = _loginRepo.GetEmpDetailsByEmpName(false);

            var _empData = (from N in lstAutoCompleteBox
                            where N.ID.ToLower().Contains(Prefix.ToLower())
                            select new
                            {
                                label = N.ID,
                                val = N.Value
                            }).Distinct();
            return Json(_empData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Approve(NominationModel model)
        {
            _repoResponse = new RepositoryResponse();
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.NominationFormDHOperations(_loggedInUserID, model, (int)NominationStatus.DH_Assign_TQC);

            _loginRepo = new LoginRepo();
            int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
            HttpContext.Session["NotifyCount"] = count;

            return Json(new { success = _repoResponse.success, message = _repoResponse.message });
        }

        [HttpPost]
        public ActionResult ReAssign(NominationModel model)
        {
            _repoResponse = new RepositoryResponse();
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.NominationFormDHOperations(_loggedInUserID, model, (int)NominationStatus.DH_Assign_EmployeeClarification);

            _loginRepo = new LoginRepo();
            int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
            HttpContext.Session["NotifyCount"] = count;

            return Json(new { success = _repoResponse.success, message = _repoResponse.message });
        }


        [HttpPost]
        public ActionResult Reject(NominationModel model)
        {
            _repoResponse = new RepositoryResponse();
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.NominationFormDHOperations(_loggedInUserID, model, (int)NominationStatus.DH_Reject);

            _loginRepo = new LoginRepo();
            int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
            int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
            HttpContext.Session["NotifyCount"] = count;

            return Json(new { success = _repoResponse.success, message = _repoResponse.message });
        }


        public ActionResult PendingNomination()
        {
            DashboardModel model = new DashboardModel();
            _loginRepo = new LoginRepo();
            _repoResponse = new RepositoryResponse();
            var deptFilter = _loginRepo.GetDepartmentDetails();
            model.Nom_DeptFilterlst = deptFilter;
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadNominationPendingDetailsForGrid(string dept = "", string date = "")
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
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.LoadNominationForDH(_loggedInUserID, dept, date, "pending");
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

        public ActionResult ApproveNomination()
        {
            DashboardModel model = new DashboardModel();
            _loginRepo = new LoginRepo();
            _repoResponse = new RepositoryResponse();
            var deptFilter = _loginRepo.GetDepartmentDetails();
            model.Nom_DeptFilterlst = deptFilter;
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadNominationApprovedDetailsForGrid(string dept = "", string date = "")
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
            _nominationRepo = new NominationRepo();
            string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            _repoResponse = _nominationRepo.LoadNominationForDH(_loggedInUserID, dept, date, "approved");
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

        //private void FileUpload(HttpPostedFileBase[] files)
        //{

        //    if (files != null)
        //    {
        //        //iterating through multiple file collection   
        //        foreach (HttpPostedFileBase file in files)
        //        {
        //            //Checking file is available to save.  
        //            if (file != null)
        //            {
        //                var InputFileName = Path.GetFileName(file.FileName);
        //                var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
        //                //Save file to server folder  
        //                file.SaveAs(ServerSavePath);
        //                //assigning file uploaded status to ViewBag for showing message to user.  
        //                ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
        //            }

        //        }
        //    }

        //}

        //[HttpPost]
        //public async Task<ActionResult> FileUpload(IList<IFormFile> files)
        //{
        //    return Json(new { success = true, message = "" });
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult FileUpload()
        {

            string nominationID = Request["NominationID"];
            string fileNamesForDb = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                string fname;
                // Checking for Internet Explorer      
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }

                if (string.IsNullOrEmpty(fileNamesForDb))
                {
                    fileNamesForDb = fname;
                }
                else
                {
                    fileNamesForDb = fileNamesForDb + ";" + fname;
                }

                string subPath = "~/Uploads/" + nominationID + "/";

                bool exists = System.IO.Directory.Exists(subPath);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                }
                fname = Path.Combine(Server.MapPath(subPath), fname);
                file.SaveAs(fname);
            }
            if (!string.IsNullOrEmpty(fileNamesForDb))
            {
                _nominationRepo = new NominationRepo();
                _nominationRepo.AddFileNameToNomination(nominationID, fileNamesForDb);
            }
            return Json(new { success = true, message = "Your files uploaded successfully" });
        }

        public FileResult DownloadFile(string nomID, string fname)
        {
            string nominationID = nomID;
            string subPath = "~/Uploads/" + nominationID + "/";
            string path = Server.MapPath(subPath);
            string _fname = Path.Combine(Server.MapPath(subPath), fname);

            byte[] fileBytes = System.IO.File.ReadAllBytes(_fname);
            string fileName = fname;
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = fname;
            return response;

        }

        [HttpPost]
        public JsonResult GetEvaluatorDataByName(string Prefix)
        {
            _nominationRepo = new NominationRepo();
            List<AutoCompleteBox> lstAutoCompleteBox = _nominationRepo.GetEvaltorDetailsForAutoCompleteBox(true);

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
        public JsonResult GetEvaluatorDataByID(string Prefix)
        {
            _nominationRepo = new NominationRepo();
            List<AutoCompleteBox> lstAutoCompleteBox = _nominationRepo.GetEvaltorDetailsForAutoCompleteBox();

            var _empData = (from N in lstAutoCompleteBox
                            where N.ID.ToLower().Contains(Prefix.ToLower())
                            select new
                            {
                                label = N.ID,
                                val = N.Value
                            }).Distinct();
            return Json(_empData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult assignEvaltorForNomination(DashboardModel model)
        {
            if (ModelState.IsValid)
            {
                _nominationRepo = new NominationRepo();
                _repoResponse = new RepositoryResponse();
                string _loggedInUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
                _repoResponse = _nominationRepo.AssignEvalatorToNominationByTQCHead(model, _loggedInUserID);

                _loginRepo = new LoginRepo();
                int _empSOMRole = int.Parse(System.Web.HttpContext.Current.Session["EmpSOMRole"].ToString());
                int count = _loginRepo.GetCountForUser(_loggedInUserID, _empSOMRole);
                HttpContext.Session["NotifyCount"] = count;

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



    }
}