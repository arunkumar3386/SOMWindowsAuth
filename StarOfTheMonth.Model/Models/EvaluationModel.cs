using System.ComponentModel.DataAnnotations;

namespace StarOfTheMonth.Models
{
    public class EvaluationModel
    {   
        public long ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EvaluatorID { get; set; }

        //[Range(18, 56, ErrorMessage = "Age Must be between 18 to 56")]
        [Range(0, 3)]
        public int? ReactiveIdeaDrivenBySituation { get; set; }
        [Range(4, 7)]
        public int? BasedOnInstruction { get; set; }
        [Range(8, 10)]
        public int? ProactiveIdeaGeneratedBySelf { get; set; }

        [Range(0, 3)]
        public int? Delayed { get; set; }
        [Range(4, 7)]
        public int? AsPerPlan { get; set; }
        [Range(8, 10)]
        public int? AheadOfPlan { get; set; }

        [Range(0, 3)]
        public int? FollowedUp { get; set; }
        [Range(4, 7)]
        public int? Participated { get; set; }
        [Range(8, 10)]
        public int? Implemented { get; set; }

        [Range(0, 3)]
        public int? CoordiantionWithInTheDept { get; set; }
        [Range(4, 7)]
        public int? CoordinationWithAnotherFunction { get; set; }
        [Range(8, 10)]
        public int? CoordinationWithMultipleFunctions { get; set; }

        [Range(0, 3)]
        public int? PreventionOfaFailure { get; set; }
        [Range(4, 7)]
        public int? ImprovementFromCurrentSituation { get; set; }
        [Range(8, 10)]
        public int? BreakthroughImprovement { get; set; }

        [Range(0, 3)]
        public int? ScopeIdentified { get; set; }
        [Range(4, 7)]
        public int? ImplementedPartially { get; set; }
        [Range(8, 10)]
        public int? ImplementedInAllApplicableAreas { get; set; }


        public string TotalScore { get; set; }
        public NominationStatus Status { get; set; }
        public bool IsActive { get; set; }

        public long SlNo { get; set; }
        public long NominationIdentity { get; set; }
        public string NominationID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string SubmittedMonth { get; set; }
        public string SubmittedYear { get; set; }
        public string SummaryOfAchievement { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
