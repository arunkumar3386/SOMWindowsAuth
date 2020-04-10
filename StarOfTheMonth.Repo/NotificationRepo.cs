using StarOfTheMonth.DataBase;
using StarOfTheMonth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarOfTheMonth.Repo
{
    public interface INotificationRepo : IDisposable
    {
        RepositoryResponse AddNotification(string sCategory, string sMessage, string sSourceUser, string sDestinationUser, string CreatedUser);

        RepositoryResponse GetNotificationsByEmployeeNumber(string userID);

        RepositoryResponse GetNotificationsByEmployeeNumberTOP3(string userID);
    }

    public class NotificationRepo : INotificationRepo
    {
        private SOMEntities objSOMEntities;
        private bool disposed = false;
        RepositoryResponse baseModel;
        string _message = string.Empty;

        public RepositoryResponse AddNotification(string sCategory, string sMessage, string sSourceUser, string sDestinationUser, string CreatedUser)
        {
            baseModel = new RepositoryResponse();
            try
            {
                using (objSOMEntities = new SOMEntities())
                {
                    Notification dbModel = new Notification();
                    dbModel.Category = sCategory;
                    dbModel.CreatedBy = CreatedUser;
                    dbModel.DestinationUser = sDestinationUser;
                    dbModel.SourceUser = sSourceUser;
                    dbModel.Message = sMessage;
                    dbModel.UserSeen = false;
                    dbModel.IsActive = true;
                    objSOMEntities.Notifications.Add(dbModel);
                    objSOMEntities.SaveChanges();
                    baseModel = new RepositoryResponse { success = true, message = "Notification Details added Successfully" };
                }
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = "Notification Details unable to add" };
            }

            return baseModel;
        }

        public RepositoryResponse GetNotificationsByEmployeeNumber(string userID)
        {
            try
            {
                List<Notification> lstNotifications = new List<Notification>();
                using (objSOMEntities = new SOMEntities())
                {
                    //if (IsSeen == false)
                    //{
                    //    lstNotifications = objSOMEntities.Notifications.Where(r => r.DestinationUser == userID.ToString() && r.IsActive == true
                    //         && r.UserSeen == IsSeen).OrderByDescending(r => r.ID).ToList();
                    //}
                    //else
                    //{
                        lstNotifications = objSOMEntities.Notifications.Where(r => r.DestinationUser == userID && r.IsActive == true)
                            .OrderByDescending(r=>r.ID).ToList();
                    //}

                }
                baseModel = new RepositoryResponse { success = true, message = "Notification Details added Successfully", Data = lstNotifications };
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = "Notification Details unable to add" };
            }
            return baseModel;
        }

        public RepositoryResponse GetNotificationsByEmployeeNumberTOP3(string userID)
        {
            try
            {
                List<Notification> lstNotifications = new List<Notification>();
                string mes = string.Empty;
                using (objSOMEntities = new SOMEntities())
                {  
                   lstNotifications = objSOMEntities.Notifications.Where(r => r.DestinationUser == userID && r.IsActive == true
                             && r.UserSeen == false).OrderByDescending(r => r.ID).Take(3).ToList();

                    string messageFormat = "<li> " +
                                          "<p> " +
                                          "##message##<a href='/Dashboard/Notification'> more details... </a> " +
                                          "<span class='timeline-icon'><i class='fa fa-bell' style='color:##colorName##'></i></span> " +
                                          "<span class='timeline-date'>##dateTime##</span> " +
                                          "</p> " +
                                          "</li> ";
                    int _count = 0;
                    foreach (var item in lstNotifications)
                    {
                        mes = item.Message;
                        if ((bool)item.UserSeen)
                        {
                            string mess = string.Empty;
                            mess = messageFormat.Replace("##message##", item.Message);
                            mess = mess.Replace("##colorName##", "red");
                            mess = mess.Replace("##dateTime##", item.CreatedDate);
                            _message += mess;
                            _count += 1;
                        }
                        else
                        {
                            string mess = string.Empty;
                            mess = messageFormat.Replace("##message##", item.Message);
                            mess = mess.Replace("##colorName##", "green");
                            mess = mess.Replace("##dateTime##", item.CreatedDate);
                            //mess = mess.Replace("##dateTime##", Helper.DateFormater(item.CreatedDate) + ", " + Helper.TimeFormater(item.CreatedTime));
                            _message += mess;
                        }
                    }

                }
                baseModel = new RepositoryResponse { success = true, message = "Notification Details added Successfully", Data = _message };
            }
            catch (Exception ex)
            {
                baseModel = new RepositoryResponse { success = false, message = "Notification Details unable to add",Data="" };
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
