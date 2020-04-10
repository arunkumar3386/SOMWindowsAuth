//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarOfTheMonth.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evaluation
    {
        public long ID { get; set; }
        public string NominationID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EvaluatorID { get; set; }
        public Nullable<int> ReactiveIdeaDrivenBySituation { get; set; }
        public Nullable<int> BasedOnInstruction { get; set; }
        public Nullable<int> ProactiveIdeaGeneratedBySelf { get; set; }
        public Nullable<int> Delayed { get; set; }
        public Nullable<int> AsPerPlan { get; set; }
        public Nullable<int> AheadOfPlan { get; set; }
        public Nullable<int> FollowedUp { get; set; }
        public Nullable<int> Participated { get; set; }
        public Nullable<int> Implemented { get; set; }
        public Nullable<int> CoordiantionWithInTheDept { get; set; }
        public Nullable<int> CoordinationWithAnotherFunction { get; set; }
        public Nullable<int> CoordinationWithMultipleFunctions { get; set; }
        public Nullable<int> PreventionOfaFailure { get; set; }
        public Nullable<int> ImprovementFromCurrentSituation { get; set; }
        public Nullable<int> BreakthroughImprovement { get; set; }
        public Nullable<int> ScopeIdentified { get; set; }
        public Nullable<int> ImplementedPartially { get; set; }
        public Nullable<int> ImplementedInAllApplicableAreas { get; set; }
        public Nullable<int> TotalScore { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
