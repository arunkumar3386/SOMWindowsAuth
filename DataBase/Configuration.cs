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
    
    public partial class Configuration
    {
        public long ID { get; set; }
        public string Module { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDisplayUI { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
