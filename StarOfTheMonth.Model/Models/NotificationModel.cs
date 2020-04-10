using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarOfTheMonth.Model.Models
{
    public class NotificationModel
    {
        public long ID { get; set; }
        public string Category { get; set; }
        public string SourceUser { get; set; }
        public string DestinationUser { get; set; }
        public string Message { get; set; }
        public bool UserSeen { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
