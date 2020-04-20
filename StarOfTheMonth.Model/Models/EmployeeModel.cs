using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StarOfTheMonth.Models
{
    public class EmployeeModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public SOMEmpRole Role { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }


    public class EmpMasterModel
    {
        public int EmpID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogedIn { get; set; }
        public string Position { get; set; }
        public string ImagePath { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Contact { get; set; }
        public bool IsActive { get; set; }
        public int PositionsId { get; set; }
        public int ReportingPersonId { get; set; }
        public DateTime PasswordChangedOn { get; set; }
        public int UserRole { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DOJ { get; set; }
        public string Department { get; set; }
        public string UserName { get; set; }
        public string Grade { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public int EmployeeSOMRole { get; set; }
        public string EmployeeSOMRoleAsString { get; set; }

        public string EMPMonth { get; set; }
        public string EMPYear { get; set; }
    }

    public class LoginModel
    {
        public long UserID { get; set; }
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string filter { get; set; }
        public IEnumerable<SelectListItem> filter1 { get; set; }
        //public List<SelectListItem> filter { get; set; }
    }
    //public class SelectListItem
    //{
    //    public bool Selected { get; set; }
    //    public string Text { get; set; }
    //    public string Value { get; set; }
    //}
}
