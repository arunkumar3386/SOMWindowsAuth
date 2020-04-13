using System.ComponentModel;

namespace StarOfTheMonth.Models
{
    public enum EmployeeRole
    {
        [Description("Nomination")]
        Nomination = 1001,

        [Description("DepartmentHead")]
        DepartmentHead = 1002,

        [Description("TQCHead")]
        TQCHead = 1003,

        [Description("Evaluation")]
        Evaluation = 1004,

        [Description("Admin")]
        Admin = 1005,

        [Description("AdminNomination")]
        AdminNomination = 1006,

        [Description("AdminEvaluation")]
        AdminEvaluation = 1007,

        [Description("AdminDH")]
        AdminDH = 1008,
    }

    public enum NominationStatus
    {
        [Description("Employee Save")]
        Employee_Save = 2001,

        [Description("Employee Assign HoD")]
        Employee_Assign_DH = 2002,

        [Description("DH Assign EmployeeClarification")]
        DH_Assign_EmployeeClarification = 2003,

        [Description("DH Assign TQC")]
        DH_Assign_TQC = 2004,

        [Description("TQC Assign Evaluator")]
        TQC_Assign_Evaluator = 2005,

        [Description("Evaluators Save")]
        Evaluators_Save = 2006,

        [Description("Evaluators Assign TQC")]
        Evaluators_Assign_TQC = 2007,

        [Description("DH Reject")]
        DH_Reject = 2008,

        [Description("TQC Reject")]
        TQC_Reject = 2009,

        [Description("Evaluator Reject")]
        Evaluator_Reject = 2010,

        [Description("Completed")]
        Completed = 2011,

        [Description("AdminReopen")]
        AdminReopen = 2012,

        [Description("Employee ReAssign DH")]
        Employee_ReAssign_DH = 2013,

        [Description("TQC Declare SOM")]
        TQC_Declare_SOM = 2014,
    }
}
