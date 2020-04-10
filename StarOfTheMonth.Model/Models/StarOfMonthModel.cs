using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StarOfTheMonth.Models
{
    public class StarOfMonthModel
    {
        public int TransID { get; set; }
        public string Month { get; set; }
        public string Description { get; set; }
        public string EmpId { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string NominationID { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
        public string Year { get; set; }

        public string DeptFilter { get; set; }
        public IEnumerable<SelectListItem> DeptFilterlst { get; set; }

        public string dateFilter { get; set; }
        public IEnumerable<SelectListItem> DateFilterlst { get; set; }

        [Required]
        public string MonthFilter { get; set; }
        public IEnumerable<SelectListItem> MonthFilterlst { get; set; }

        [Required]
        public string YearFilter { get; set; }
        public IEnumerable<SelectListItem> YearFilterlst { get; set; }

        [Required]
        public string NominationIDs { get; set; }
        public IEnumerable<SelectListItem> NominationIDlst { get; set; }

        //[Required]
        public string EmployeeDetails { get; set; }

        [Required]
        public string SOMComments { get; set; }

        public string SelectedNominationID { get; set; }

    }
}
