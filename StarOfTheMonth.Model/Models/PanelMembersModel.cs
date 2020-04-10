using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StarOfTheMonth.Models
{
    public class PanelMembersModel
    {

        public long ID { get; set; }
        [Required]
        public string EvaluatorID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public bool IsActive { get; set; }


        public long SlNo { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string DeptFilter { get; set; }
        public IEnumerable<SelectListItem> DeptFilterlst { get; set; }

        public string DateFilter { get; set; }
        public IEnumerable<SelectListItem> DateFilterlst { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        
        public string EmpName { get; set; }
        public string EmpNumber { get; set; }
        [Required]
        public string MonthFilter { get; set; }
        public IEnumerable<SelectListItem> MonthFilterlst { get; set; }
        [Required]
        public string YearFilter { get; set; }
        public IEnumerable<SelectListItem> YearFilterlst { get; set; }
    }
}
