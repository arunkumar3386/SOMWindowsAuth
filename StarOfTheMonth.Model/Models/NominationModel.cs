using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace StarOfTheMonth.Models
{
    public class NominationModel
    {
        public long ID { get; set; }
        public string EmployeeNumber { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        //[Required]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> StartDate { get; set; }
        //[Required]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> EndDate { get; set; }

        [Required]
        public string sStartDate { get; set; }
        [Required]
        public string sEndDate { get; set; }
        [Required]
        public string ProjectTitle { get; set; }
        [Required]
        public string Idea { get; set; }
        [Required]
        public string PrimaryObjective { get; set; }
        [Required]
        public string BriefDescription { get; set; }
        [Required]
        public string Benefits { get; set; }
        //public string AreaOfInterest_1 { get; set; }
        //public string ImplementationStatus_1 { get; set; }
        //public string AreaOfInterest_2 { get; set; }
        //public string ImplementationStatus_2 { get; set; }
        //public string AreaOfInterest_3 { get; set; }
        //public string ImplementationStatus_3 { get; set; }
        public long HorizontalDeploymentID { get; set; }
        [Required]
        public string Acknowledgement { get; set; }
        public string SubmittedMonth { get; set; }
        public string SubmittedYear { get; set; }
        public NominationStatus Status { get; set; }
        public int Status_int { get; set; }
        public bool IsActive { get; set; }

        public string NominationSubmittedDate { get; set; }
        public string NominationSignature { get; set; }


        public string CommentsByDH { get; set; }
        public string DHSubmittedDate { get; set; }
        public string DHSignature { get; set; }

        public string CommentsForSOM { get; set; }
        public string SOMSubmittedDate { get; set; }
        public string SOMSignature { get; set; }

        public string Name { get; set; }
        public string Department { get; set; }
        public string StartDate_ForGrid { get; set; }
        public string EndDate_ForGrid { get; set; }
        public string CreatedDate_ForGrid { get; set; }
        public long SlNo { get; set; }

        public string lstHDIDs { get; set; }
        public string lstAreaOfInterest { get; set; }
        public string lstImplementationStatus { get; set; }

        public List<HorizontaldeploymentModel> lstHD { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public List<SelectListItem> Savings { get; set; }
        public int[] SavingsIds { get; set; }

        public string Cost { get; set; }
        public string Time { get; set; }
        public string Paper { get; set; }

        public string NominationID { get; set; }
        public string UploadedFileNames { get; set; }

        public List<AuditLogModel> lstAuditLogs { get; set; }

        //[Required(ErrorMessage = "Please select file.")]
        //[Display(Name = "Browse File")]
        //public HttpPostedFileBase[] files { get; set; }

        public string NominationMessage { get; set; }
        public string ReportingPersonID { get; set; }

        public List<fileProperty> uploadFilelst { get; set; }
        public bool IsEvalatorAssigned { get; set; }

        public string AuditLogComments { get; set; }

        public string currentHoldingPerson { get; set; }
        public string StatusText { get; set; }
        public DateTime CreatedDateForFilterAsDateTime { get; set; }

    }
    
    public class fileProperty
    {
        public string FileName { get; set; }
        public string NominationID { get; set; }
    }

    public class HorizontaldeploymentModel
    {
        public long ID { get; set; }
        public string NominationID { get; set; }
        public string EmpNumber { get; set; }
        public string AreaOfInterest { get; set; }
        public string ImplementationStatus { get; set; }
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class AutoCompleteBox
    {
        public string ID { get; set; }
        public string Value { get; set; }
    }

    public class RepositoryResponse
    {
        public dynamic Data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class DashboardModel
    {
        public string DeptFilter { get; set; }
        public IEnumerable<SelectListItem> DeptFilterlst { get; set; }

        public string From_Date { get; set; }
        public IEnumerable<SelectListItem> From_Nom_DateFilterlst { get; set; }

        public string To_Date { get; set; }
        public IEnumerable<SelectListItem> TO_Nom_DateFilterlst { get; set; }

        public string Eval_DeptFilter { get; set; }
        public IEnumerable<SelectListItem> Eval_DeptFilterlst { get; set; }

        public string Eval_dateFilter { get; set; }
        public IEnumerable<SelectListItem> Eval_DateFilterlst { get; set; }

        public ParticipatedCount participatedCount { get; set; }

        public EvaluationModel evaluationModel { get; set; }

        [Required]
        public string EvaluatorName { get; set; }
        [Required]
        public string EvaluatorNumber { get; set; }
        public long ID { get; set; }

    }

    public class ParticipatedCount
    {
        public int TotalEmpCount { get; set; }
        public int ParticipatedEmpCount { get; set; }
        public int WinnnerCount { get; set; }
    }
}
