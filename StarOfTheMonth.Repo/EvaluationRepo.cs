using Helper;
using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarOfTheMonth.Repo
{
    public interface IEvaluationRepo : IDisposable
    {
        RepositoryResponse LoadAllEvaluationData(string _loggedInUserID,string dept = "", string fromDate = "", string toDate = "");
        RepositoryResponse AddorUpdateEvaluationData(EvaluationModel model, string _loggedInUserID, bool isSubmit);
        RepositoryResponse LoadEvaluationDataByID(long ID);
        RepositoryResponse LoadEmpNominationDetails(long ID, string nominationID, string empNum);
        RepositoryResponse LoadAllEvaluationData_Alltime(string _loggedInUserID, string dept = "", string fromDate = "", string toDate = "");
    }

    public class EvaluationRepo : IEvaluationRepo
    {
        private SOMEntities objSOMEntities;
        private IntranetPortalEntities objIPEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;
        INominationRepo nomRepo;
        IStarOfMonthRepo starRepo;
        IConfigurationRepo configRepo;

        public RepositoryResponse LoadAllEvaluationData(string _loggedInUserID,string dept, string fromDate = "", string toDate = "")
        {
            baseModel = new RepositoryResponse();
            try
            {
                string F_subMonth = string.Empty;
                string F_subYear = string.Empty;

                string TO_subMonth = string.Empty;
                string TO_subYear = string.Empty;

                if (!string.IsNullOrEmpty(toDate))
                {
                    string[] sDate = toDate.Split('-');
                    TO_subMonth = sDate[0];
                    TO_subYear = sDate[1];
                }

                if (!string.IsNullOrEmpty(fromDate))
                {
                    string[] sDate = fromDate.Split('-');
                    F_subMonth = sDate[0];
                    F_subYear = sDate[1];
                }

                List<EvaluationModel> lstEvaluation = new List<EvaluationModel>();
                //using (objSOMEntities = new SOMEntities())
                //{
                //    string month = Assistant.GetMonthFromCurrentDate();
                //    string year = Assistant.GetYearFromCurrentDate();
                //    var currMonth = objSOMEntities.EvaluatorAvailabilities.Where(r => r.EvaluatorID == _loggedInUserID
                //                        && r.Month == month
                //                        && r.Year == year).FirstOrDefault();
                //    if (currMonth == null)
                //    {
                //        return baseModel = new RepositoryResponse { success = true, message = "", Data = lstEvaluation }; ;
                //    }
                //}
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {

                    var _NomDetails = (from em in objIPEntities.EmpMasters.AsEnumerable()
                                       join eva in objSOMEntities.Evaluations.AsEnumerable() on em.EmployeeNumber equals eva.EmployeeNumber.ToString()
                                       join nom in objSOMEntities.Nominations.AsEnumerable() on eva.NominationID equals nom.NominationId
                                       where eva.EvaluatorID == _loggedInUserID && nom.IsActive == true
                                       && (eva.Status == (int)NominationStatus.TQC_Assign_Evaluator || eva.Status == (int)NominationStatus.Evaluators_Save)
                                       select new { em, eva, nom }).OrderByDescending(r => r.nom.ID).ToList();
                    int i = 1;
                    foreach (var item in _NomDetails)
                    {
                        lstEvaluation.Add(ConvertEvaluation_DB2Model(item.em, item.eva, item.nom, i));
                    }

                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        lstEvaluation = lstEvaluation.Where(r => r.Department == dept).ToList();
                    }
                    if (!string.IsNullOrEmpty(F_subMonth) && !string.IsNullOrEmpty(F_subYear) && !string.IsNullOrEmpty(TO_subMonth) && !string.IsNullOrEmpty(TO_subYear))
                    {
                        string sDate = "01" + F_subMonth + "" + F_subYear;
                        string eDate = "30" + TO_subMonth + "" + TO_subYear;

                        DateTime sdt = Assistant.SOMDbToDateTimeForFilter(sDate, true);
                        DateTime edt = Assistant.SOMDbToDateTimeForFilter(eDate, false);

                        lstEvaluation = lstEvaluation.Where(r => r.IsActive == true
                                        && (r.CreatedDateForFilterAsDateTime.Date >= sdt.Date
                                        && r.CreatedDateForFilterAsDateTime.Date <= edt.Date)).ToList();
                    }


                    baseModel = new RepositoryResponse { success = true, message = "Get Evaluation details Successfully", Data = lstEvaluation.OrderByDescending(r => r.TotalScore) };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            
            return baseModel;
        }

        public RepositoryResponse AddorUpdateEvaluationData(EvaluationModel model, string _loggedInUserID, bool isSubmit)
        {
            baseModel = new RepositoryResponse();

            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    Evaluation db = objSOMEntities.Evaluations.Where(r => r.ID == model.ID).FirstOrDefault();
                    if (db == null)
                    {
                        objSOMEntities.Evaluations.Add(ConvertEvaluation_Model2DB(model, _loggedInUserID, isSubmit));
                        objSOMEntities.SaveChanges();
                        if (isSubmit)
                        {
                            baseModel = new RepositoryResponse { success = true, message = "Evaluations Scroe Details Submitted to TQC Successfully" };
                        }
                        else
                        {
                            baseModel = new RepositoryResponse { success = true, message = "Evaluations Scroe Details added Successfully" };
                        }
                    }
                    else
                    {
                        db.TotalScore = int.Parse(model.TotalScore);
                        db.AheadOfPlan = model.AheadOfPlan;
                        db.AsPerPlan = model.AsPerPlan;
                        db.BasedOnInstruction = model.BasedOnInstruction;

                        db.BreakthroughImprovement = model.BreakthroughImprovement;
                        db.CoordiantionWithInTheDept = model.CoordiantionWithInTheDept;
                        db.CoordinationWithAnotherFunction = model.CoordinationWithAnotherFunction;

                        db.CoordinationWithMultipleFunctions = model.CoordinationWithMultipleFunctions;
                        db.Delayed = model.Delayed;
                        //db.EmployeeID = model.EmployeeID;
                        db.NominationID = model.NominationID;

                        //db.EvaluatorID = model.EvaluatorID;
                        db.FollowedUp = model.FollowedUp;
                        //db.ID = model.ID;

                        db.Implemented = model.Implemented;
                        db.ImplementedInAllApplicableAreas = model.ImplementedInAllApplicableAreas;
                        db.ImplementedPartially = model.ImplementedPartially;
                        db.ImprovementFromCurrentSituation = model.ImprovementFromCurrentSituation;

                        db.IsActive = true;
                        db.Participated = model.Participated;
                        db.PreventionOfaFailure = model.PreventionOfaFailure;

                        db.ProactiveIdeaGeneratedBySelf = model.ProactiveIdeaGeneratedBySelf;
                        db.ReactiveIdeaDrivenBySituation = model.ReactiveIdeaDrivenBySituation;
                        db.ScopeIdentified = model.ScopeIdentified;

                        if (isSubmit)
                        {
                            db.Status = (int)NominationStatus.Evaluators_Assign_TQC;
                        }
                        else
                        {
                            db.Status = (int)NominationStatus.Evaluators_Save;
                        }
                        db.ModifiedBy = _loggedInUserID;
                        objSOMEntities.SaveChanges();

                        if (isSubmit)
                        {
                            configRepo = new ConfigurationRepo();
                            bool result = configRepo.SendEmailUsingSOM_Evaluator_Assign_TQC(db.NominationID, _loggedInUserID);
                            if (result)
                            {
                                baseModel = new RepositoryResponse { success = true, message = "Evaluations Scroe Details Submitted to TQC Successfully and Send Mail to TQC" };
                            }
                            else
                            {
                                baseModel = new RepositoryResponse { success = true, message = "Evaluations Scroe Details Submitted to TQC Successfully. Unable to Send Mail to TQC" };
                            }
                            
                        }
                        else
                        {
                            baseModel = new RepositoryResponse { success = true, message = "Evaluations Scroe Details updated Successfully" };
                        }
                    }
                }
                using (objSOMEntities = new SOMEntities())
                {
                    if (isSubmit)
                    {
                        var nomDet = objSOMEntities.Nominations.Where(r => r.NominationId == model.NominationID).FirstOrDefault();
                        nomDet.Status = (int)NominationStatus.Evaluators_Assign_TQC;
                        nomDet.ModifiedBy = _loggedInUserID;
                        objSOMEntities.SaveChanges();
                    }
                }
                if (isSubmit)
                {
                    nomRepo = new NominationRepo();
                    //Add entry into Audit Log
                    AuditLogModel objAuditLog = new AuditLogModel();
                    objAuditLog.CurrentStatus = NominationStatus.Evaluators_Assign_TQC;
                    objAuditLog.EmployeeNumber = model.EmployeeNumber;
                    objAuditLog.IsNewAlert = true;
                    objAuditLog.IsNotification = true;
                    objAuditLog.NominationID = model.NominationID;
                    objAuditLog.CreatedBy = _loggedInUserID;
                    objAuditLog.EvaluatorID = _loggedInUserID;
                    nomRepo.AddEntryIntoAuditLog(objAuditLog);

                    Nomination _nomModel;
                    using (objSOMEntities = new SOMEntities())
                    {
                        _nomModel = objSOMEntities.Nominations.Where(r => r.NominationId == model.NominationID).FirstOrDefault();
                    }
                    // Add entry into SOM
                    StarOfMonthModel SOMmodel = new StarOfMonthModel();
                    SOMmodel.Month = _nomModel.SubmittedMonth;
                    SOMmodel.Year = _nomModel.SubmittedYear;
                    SOMmodel.Description = _nomModel.Idea;
                    SOMmodel.EmpId = model.EmployeeNumber;
                    SOMmodel.NominationID = model.NominationID;
                    SOMmodel.IsDisplay = true;
                    SOMmodel.IsApproved = true;
                    SOMmodel.ApprovedBy = int.Parse(_loggedInUserID);
                    SOMmodel.CreatedBy = int.Parse(_loggedInUserID);
                    SOMmodel.CreatedDate = DateTime.Now;
                    SOMmodel.ModifiedBy = int.Parse(_loggedInUserID);
                    SOMmodel.ModifiedDate = DateTime.Now;
                    starRepo = new StarOfMonthRepo();
                    //starRepo.AddOrEditStarOfMonth(SOMmodel, _loggedInUserID);
                }

                if (isSubmit) //Send Nomination Submit Details to DH
                {
                    using (objSOMEntities = new SOMEntities())
                    {

                    }
                   
                }
                
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;


        }

        public RepositoryResponse LoadEvaluationDataByID(long ID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    EmpMaster a = new EmpMaster();
                    Nomination c = new Nomination();
                    var _data = objSOMEntities.Evaluations.Where(r => r.ID == ID && r.IsActive == true).FirstOrDefault();
                    EvaluationModel model = ConvertEvaluation_DB2Model(a,_data,c,1);
                    baseModel = new RepositoryResponse { success = true, message = "Get Evaluation details Successfully", Data = model };
                } 
            }
            catch (Exception ex)
            {

                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        public RepositoryResponse LoadEmpNominationDetails(long ID, string nominationID, string empNum)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {
                    EmpMaster a = new EmpMaster();
                    Nomination c = new Nomination();

                    var _NomDetails = (from em in objIPEntities.EmpMasters.AsEnumerable()
                                       join nom in objSOMEntities.Nominations.AsEnumerable() on em.EmployeeNumber equals nom.EmployeeNumber.ToString()
                                       where nom.IsActive == true && nom.NominationId == nominationID && em.EmployeeNumber == empNum
                                       select new { em, nom }).OrderByDescending(r => r.nom.ID).FirstOrDefault();

                    var _data = objSOMEntities.Evaluations.Where(r => r.ID == ID && r.IsActive == true).FirstOrDefault();
                    EvaluationModel model = ConvertEvaluation_DB2Model(_NomDetails.em, _data, _NomDetails.nom, 1);
                    baseModel = new RepositoryResponse { success = true, message = "Get Evaluation details Successfully", Data = model };
                }
            }
            catch (Exception ex)
            {

                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }
            return baseModel;
        }

        private EvaluationModel ConvertEvaluation_DB2Model(EmpMaster a, Evaluation b , Nomination c,  int i)
        {           
            EvaluationModel model = new EvaluationModel();
            model.SlNo = i;
            if (a != null)
            {
                model.Name = a.EmployeeName;
                model.EmployeeNumber = a.EmployeeNumber;
                model.Department = a.Department;
            }
            if (b!=null)
            {
                model.AheadOfPlan = b.AheadOfPlan;
                model.AsPerPlan = b.AsPerPlan;
                model.BasedOnInstruction = b.BasedOnInstruction;

                model.BreakthroughImprovement = b.BreakthroughImprovement;
                model.CoordiantionWithInTheDept =b.CoordiantionWithInTheDept;
                model.CoordinationWithAnotherFunction = b.CoordinationWithAnotherFunction;

                model.CoordinationWithMultipleFunctions = b.CoordinationWithMultipleFunctions;
                model.Delayed = b.Delayed;

                model.EvaluatorID = b.EvaluatorID;
                model.FollowedUp = b.FollowedUp;
                model.ID = b.ID;

                model.Implemented = b.Implemented;
                model.ImplementedInAllApplicableAreas = b.ImplementedInAllApplicableAreas;
                model.ImplementedPartially = b.ImplementedPartially;
                model.ImprovementFromCurrentSituation = b.ImprovementFromCurrentSituation;

                model.IsActive = (bool)b.IsActive;
                model.Participated = b.Participated;
                model.PreventionOfaFailure = b.PreventionOfaFailure;

                model.ProactiveIdeaGeneratedBySelf = b.ProactiveIdeaGeneratedBySelf;
                model.ReactiveIdeaDrivenBySituation = b.ReactiveIdeaDrivenBySituation;
                model.ScopeIdentified = b.ScopeIdentified;

                model.Status = (NominationStatus)b.Status;
                model.TotalScore = b.TotalScore.ToString();
            }
            if (c!=null)
            {
                model.NominationIdentity = c.ID;
                model.NominationID = c.NominationId;
                model.SummaryOfAchievement = c.Idea;
                model.SubmittedMonth = c.SubmittedMonth;
                model.SubmittedYear = c.SubmittedYear;
                if (!string.IsNullOrEmpty(c.CreatedDate))
                {
                    model.CreatedDateForFilterAsDateTime = Assistant.SOMDbToDateTimePicker(c.CreatedDate.Substring(0, 8));
                }
                
            }
            return model;
        }

        private Evaluation ConvertEvaluation_Model2DB(EvaluationModel model, string _loggedInUserID, bool isSubmit)
        {
            Evaluation db = new Evaluation();
            if (model != null)
            {
                db.AheadOfPlan = model.AheadOfPlan;
                db.AsPerPlan = model.AsPerPlan;
                db.BasedOnInstruction = model.BasedOnInstruction;

                db.BreakthroughImprovement = model.BreakthroughImprovement;
                db.CoordiantionWithInTheDept = model.CoordiantionWithInTheDept;
                db.CoordinationWithAnotherFunction = model.CoordinationWithAnotherFunction;

                db.CoordinationWithMultipleFunctions = model.CoordinationWithMultipleFunctions;
                db.Delayed = model.Delayed;
                db.EmployeeNumber = model.EmployeeNumber;

                db.NominationID = model.NominationID;
                db.EvaluatorID = model.EvaluatorID;
                db.FollowedUp = model.FollowedUp;
                db.ID = model.ID;

                db.Implemented = model.Implemented;
                db.ImplementedInAllApplicableAreas = model.ImplementedInAllApplicableAreas;
                db.ImplementedPartially = model.ImplementedPartially;
                db.ImprovementFromCurrentSituation = model.ImprovementFromCurrentSituation;

                db.IsActive = true;
                db.Participated = model.Participated;
                db.PreventionOfaFailure = model.PreventionOfaFailure;

                db.ProactiveIdeaGeneratedBySelf = model.ProactiveIdeaGeneratedBySelf;
                db.ReactiveIdeaDrivenBySituation = model.ReactiveIdeaDrivenBySituation;
                db.ScopeIdentified = model.ScopeIdentified;

                if (isSubmit)
                {
                    db.Status = (int)NominationStatus.Evaluators_Assign_TQC;
                }
                else
                {
                    db.Status = (int)NominationStatus.Evaluators_Save;
                }
                db.TotalScore = int.Parse(model.TotalScore);
                db.CreatedBy = _loggedInUserID;;
            }
            return db;
        }

        public RepositoryResponse LoadAllEvaluationData_Alltime(string _loggedInUserID, string dept, string fromDate = "", string toDate = "")
        {
            baseModel = new RepositoryResponse();
            try
            {
                string F_subMonth = string.Empty;
                string F_subYear = string.Empty;

                string TO_subMonth = string.Empty;
                string TO_subYear = string.Empty;

                if (!string.IsNullOrEmpty(toDate))
                {
                    string[] sDate = toDate.Split('-');
                    TO_subMonth = sDate[0];
                    TO_subYear = sDate[1];
                }

                if (!string.IsNullOrEmpty(fromDate))
                {
                    string[] sDate = fromDate.Split('-');
                    F_subMonth = sDate[0];
                    F_subYear = sDate[1];
                }

                List<EvaluationModel> lstEvaluation = new List<EvaluationModel>();
                using (objSOMEntities = new SOMEntities())
                using (objIPEntities = new IntranetPortalEntities())
                {

                    var _NomDetails = (from em in objIPEntities.EmpMasters.AsEnumerable()
                                       join eva in objSOMEntities.Evaluations.AsEnumerable() on em.EmployeeNumber equals eva.EmployeeNumber.ToString()
                                       join nom in objSOMEntities.Nominations.AsEnumerable() on eva.NominationID equals nom.NominationId
                                       where 
                                       //eva.EvaluatorID == _loggedInUserID && 
                                       nom.IsActive == true
                                       //&& (eva.Status == (int)NominationStatus.TQC_Assign_Evaluator || eva.Status == (int)NominationStatus.Evaluators_Save
                                       //|| eva.Status == (int)NominationStatus.Evaluators_Assign_TQC || eva.Status == (int)NominationStatus.TQC_Declare_SOM
                                       //|| eva.Status == (int)NominationStatus || eva.Status == (int)NominationStatus.Evaluators_Save)
                                       select new { em, eva, nom }).OrderByDescending(r => r.nom.ID).ToList();
                    int i = 1;
                    foreach (var item in _NomDetails)
                    {
                        //Evaluation objEvaluation = objSOMEntities.Evaluations.Where(
                        //                r => r.CreatedBy == _loggedInUserID
                        //                && r.NominationID.Value == item.nom.ID
                        //                && r.IsActive == true).FirstOrDefault();

                        //Evaluation objEvaluation = objSOMEntities.Evaluations.Where(
                        //                r=> r.NominationID == item.nom.NominationId
                        //                && r.IsActive == true).FirstOrDefault();

                        lstEvaluation.Add(ConvertEvaluation_DB2Model(item.em, item.eva, item.nom, i));
                    }

                    if (!string.IsNullOrEmpty(dept) && dept != "--ALL--")
                    {
                        lstEvaluation = lstEvaluation.Where(r => r.Department == dept).ToList();
                    }
                    if (!string.IsNullOrEmpty(F_subMonth) && !string.IsNullOrEmpty(F_subYear) && !string.IsNullOrEmpty(TO_subMonth) && !string.IsNullOrEmpty(TO_subYear))
                    {
                        string sDate = "01" + F_subMonth + "" + F_subYear;
                        string eDate = "30" + TO_subMonth + "" + TO_subYear;

                        DateTime sdt = Assistant.SOMDbToDateTimeForFilter(sDate, true);
                        DateTime edt = Assistant.SOMDbToDateTimeForFilter(eDate, false);

                        lstEvaluation = lstEvaluation.Where(r => r.IsActive == true
                                        && (r.CreatedDateForFilterAsDateTime.Date >= sdt.Date
                                        && r.CreatedDateForFilterAsDateTime.Date <= edt.Date)).ToList();
                    }

                    baseModel = new RepositoryResponse { success = true, message = "Get Evaluation details Successfully", Data = lstEvaluation.OrderByDescending(r => r.TotalScore) };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
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
