﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SOMEntities : DbContext
    {
        public SOMEntities()
            : base("name=SOMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<DepartmentHead> DepartmentHeads { get; set; }
        public virtual DbSet<EnumAbbreviation> EnumAbbreviations { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<EvaluatorAvailability> EvaluatorAvailabilities { get; set; }
        public virtual DbSet<HorizontalDeployment> HorizontalDeployments { get; set; }
        public virtual DbSet<Nomination> Nominations { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<StarOfTheMonth> StarOfTheMonths { get; set; }
        public virtual DbSet<TQCHead> TQCHeads { get; set; }
    }
}