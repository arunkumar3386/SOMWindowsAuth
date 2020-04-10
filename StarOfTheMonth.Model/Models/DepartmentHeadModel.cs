namespace StarOfTheMonth.Models
{
    public class DepartmentHeadModel
    {
        public long ID { get; set; }
        public string NominationID { get; set; }
        public string EmployeeID { get; set; }
        public string CommentsByDeptHead { get; set; }
        public NominationStatus Status { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}
