namespace StarOfTheMonth.Models
{
    public class AuditLogModel
    {
        public long ID { get; set; }
        public string NominationID { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentHeadID { get; set; }
        public string TQCHeadID { get; set; }
        public string EvaluatorID { get; set; }
        public bool IsNewAlert { get; set; }
        public bool IsNotification { get; set; }
        public NominationStatus CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Comments { get; set; }

        public int SlNo { get; set; }
        public string CreatedDate { get; set; }
        public string AdminID { get; set; }
        public string InfoMessage { get; set; }
        public string DepartmentHeadName { get; set; }
        public string TQCHeadName { get; set; }
        public string EvaluatorName { get; set; }
        public string AdminNameName { get; set; }
    }
}
