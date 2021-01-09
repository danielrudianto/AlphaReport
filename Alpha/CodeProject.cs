//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CodeProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CodeProject()
        {
            this.CodeProjectDocument = new HashSet<CodeProjectDocument>();
            this.CodeProjectUser = new HashSet<CodeProjectUser>();
            this.CodeReport = new HashSet<CodeReport>();
            this.Project = new HashSet<Project>();
            this.RequestForInformation = new HashSet<RequestForInformation>();
            this.StatusReport = new HashSet<StatusReport>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ClientId { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public string Address { get; set; }
        public string DocumentName { get; set; }
        public byte IsCompleted { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodeProjectDocument> CodeProjectDocument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodeProjectUser> CodeProjectUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodeReport> CodeReport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestForInformation> RequestForInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StatusReport> StatusReport { get; set; }
    }
}
