﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alpha
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class alphaReportEntities : DbContext
    {
        public alphaReportEntities()
            : base("name=alphaReportEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientContact> ClientContact { get; set; }
        public virtual DbSet<CodeProject> CodeProject { get; set; }
        public virtual DbSet<CodeProjectDocument> CodeProjectDocument { get; set; }
        public virtual DbSet<CodeProjectUser> CodeProjectUser { get; set; }
        public virtual DbSet<CodeReport> CodeReport { get; set; }
        public virtual DbSet<CodeReportApproval> CodeReportApproval { get; set; }
        public virtual DbSet<DailyReportImage> DailyReportImage { get; set; }
        public virtual DbSet<DailyTask> DailyTask { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RequestForInformation> RequestForInformation { get; set; }
        public virtual DbSet<RequestForInformationAnswer> RequestForInformationAnswer { get; set; }
        public virtual DbSet<RequestForInformationDocument> RequestForInformationDocument { get; set; }
        public virtual DbSet<StatusReport> StatusReport { get; set; }
        public virtual DbSet<StatusReportImage> StatusReportImage { get; set; }
        public virtual DbSet<Tool> Tool { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserPosition> UserPosition { get; set; }
        public virtual DbSet<Weather> Weather { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
    }
}
