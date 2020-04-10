using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarOfTheMonth.Models
{
    public class TQCHeadModel
    {
        public long? ID { get; set; }
        public long? EmployeeID { get; set; }

        [Required]
        public string EmpNumber { get; set; }

        [Required]
        public string EmpName { get; set; }
        
        [Required]
        public Nullable<System.DateTime> StartDate { get; set; }

        //[Required]
        public Nullable<System.DateTime> EndDate { get; set; }
        
        public bool? IsActive { get; set; }

        public long SlNo { get; set; }
        public string StartDate_Grid { get; set; }
        public string EndDate_Grid { get; set; }

        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}
