using Helper;
using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace StarOfTheMonth.Repo
{
    public interface ITQCHeadRepo : IDisposable
    {
        RepositoryResponse GetAllTQCHeadDatas(string dept = "", string date = "");
        RepositoryResponse GetTQCHeadDetailsByID(long ID);
        RepositoryResponse AddOrEditTQCHeadDetails(TQCHeadModel model, string _loggedInUserID);
    }

    public class TQCHeadRepo : ITQCHeadRepo
    {
        private SOMEntities objSOMEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;

        public RepositoryResponse GetAllTQCHeadDatas(string dept, string date)
        {
            baseModel = new RepositoryResponse();
            try
            {
                string subMonth = string.Empty;
                string subYear = string.Empty;

                if (!string.IsNullOrEmpty(date))
                {
                    string[] sDate = date.Split('-');
                    subMonth = sDate[0];
                    subYear = sDate[1];
                }
                List<TQCHeadModel> _lstTQCHead = new List<TQCHeadModel>();
                using (objSOMEntities = new SOMEntities())
                {
                    var lstTQCHead = objSOMEntities.TQCHeads.OrderBy(r => r.IsActive).ToList();

                    int i = 1;
                    foreach (var item in lstTQCHead)
                    {

                        TQCHeadModel _TQCHead = new TQCHeadModel();
                        _TQCHead.ID = item.ID;
                        _TQCHead.SlNo = i;
                        _TQCHead.EmployeeID = long.Parse(item.EmployeeNumber);
                        _TQCHead.EmpNumber = item.EmployeeNumber;
                        _TQCHead.EmpName = item.Name;
                        _TQCHead.StartDate_Grid = Assistant.SOMDbToUIDateConversion(item.StartDate); 
                        _TQCHead.EndDate_Grid = Assistant.SOMDbToUIDateConversion(item.EndDate); 
                        _TQCHead.IsActive = (bool)item.IsActive;
                        _lstTQCHead.Add(_TQCHead);
                        i++;
                    }

                    baseModel = new RepositoryResponse { success = true, message = "Get TQC Head details Successfully", Data = _lstTQCHead };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }

        public RepositoryResponse GetTQCHeadDetailsByID(long ID)
        {
            baseModel = new RepositoryResponse();
            try
            {
                TQCHeadModel model = new TQCHeadModel();
                using (objSOMEntities = new SOMEntities())
                {
                    var data = objSOMEntities.TQCHeads.Where(r => r.ID == ID).FirstOrDefault();

                    if (data != null)
                    {

                        model.ID = data.ID;
                        model.EmployeeID = long.Parse(data.EmployeeNumber);
                        model.EmpName = data.Name;                     
                        model.StartDate_Grid = Assistant.SOMDbToUIDateConversionForPopup(data.StartDate);                       
                        model.EndDate_Grid = Assistant.SOMDbToUIDateConversionForPopup(data.EndDate); ;
                        //model.IsActive = (bool)data.IsActive;

                        baseModel = new RepositoryResponse { success = true, message = "Get TQC Head details Successfully", Data = model };
                    }
                    else
                    {
                        baseModel = new RepositoryResponse { success = false, message = "Unable to get TQC Head details", Data = model };
                    }

                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = ex.ToString() };
            }

            return baseModel;
        }

        public RepositoryResponse AddOrEditTQCHeadDetails(TQCHeadModel model, string _loggedInUserID)
        {
            baseModel = new RepositoryResponse();
            TQCHead dbModel;
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    DateTime now = DateTime.Now;
                    dbModel = new TQCHead();
                    //var usr = objSOMEntities.EmpMasters.Where(r => r.EmpID == model.EmployeeID).FirstOrDefault();
                    dbModel = objSOMEntities.TQCHeads.Where(r => r.ID == model.ID).FirstOrDefault();
                    if (dbModel != null)
                    {
                        dbModel.StartDate = Assistant.SOMDateConversionFrom_UIToDb((DateTime)model.StartDate);
                        dbModel.EndDate = "01019999";
                        dbModel.EmployeeNumber = model.EmpNumber;
                        dbModel.Name = model.EmpName;
                        dbModel.IsActive = true;
                        dbModel.ModifiedBy = _loggedInUserID;
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "TQC Head Details updated Successfully" };
                    }
                    else
                    {
                        dbModel = new TQCHead();
                        dbModel.EmployeeNumber = model.EmpNumber;
                        dbModel.Name = model.EmpName;
                        dbModel.StartDate = Assistant.SOMDateConversionFrom_UIToDb((DateTime)model.StartDate);
                        dbModel.EndDate = "01019999";
                        dbModel.IsActive = true;
                        dbModel.CreatedBy = _loggedInUserID;
                        objSOMEntities.TQCHeads.Add(dbModel);
                        objSOMEntities.SaveChanges();
                        baseModel = new RepositoryResponse { success = true, message = "TQC Head Details added Successfully" };
                    }
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
