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

        [Description("AdminHoD")]
        AdminHoD = 1008,

        [Description("AdminNominationHoD")]
        AdminNominationHoD = 1009,

        [Description("AdminEvaluationHoD")]
        AdminEvaluationHoD = 10010,
    }

    public enum SOMEmpRole
    {
        [Description("TQCHead")]
        TQCHead = 3001,

        [Description("Admin")]
        Admin = 3002,

        [Description("HoD")]
        HoD = 3003,

        [Description("Evaluation")]
        Evaluation = 3004,

        [Description("Nomination")]
        Nomination = 3005,

        [Description("AdminHoD")]
        AdminHoD = 3006,

        [Description("AdminEvaluation")]
        AdminEvaluation = 3007,

        [Description("AdminNomination")]
        AdminNomination = 3008,

        [Description("HoDEvaluation")]
        HoDEvaluation = 3009,

        [Description("HoDNomination")]
        HoDNomination = 3010,

        [Description("AdminHoDEvaluation")]
        AdminHoDEvaluation = 3011,

        [Description("AdminHoDNomination")]
        AdminHoDNomination = 3012,

        [Description("AdminHoDEvaluationNomination")]
        AdminHoDEvaluationNomination = 3013,

    }

    public enum NominationStatus
    {
        [Description("Employee Save")]
        Employee_Save = 2001,

        [Description("Employee Assign HoD")]
        Employee_Assign_HOD = 2002,

        [Description("HoD Assign EmployeeClarification")]
        HoD_Assign_EmployeeClarification = 2003,

        [Description("HoD Assign TQC")]
        HoD_Assign_TQC = 2004,

        [Description("TQC Assign Evaluator")]
        TQC_Assign_Evaluator = 2005,

        [Description("Evaluators Save")]
        Evaluators_Save = 2006,

        [Description("Evaluators Assign TQC")]
        Evaluators_Assign_TQC = 2007,

        [Description("HoD Reject")]
        HoD_Reject = 2008,

        [Description("TQC Reject")]
        TQC_Reject = 2009,

        [Description("Evaluator Reject")]
        Evaluator_Reject = 2010,

        [Description("Completed")]
        Completed = 2011,

        [Description("AdminReopen")]
        AdminReopen = 2012,

        [Description("Employee ReAssign HoD")]
        Employee_ReAssign_HoD = 2013,

        [Description("TQC Declare SOM")]
        TQC_Declare_SOM = 2014,
    }
}
