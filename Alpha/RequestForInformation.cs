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
    
    public partial class RequestForInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestForInformation()
        {
            this.RequestForInformationAnswer = new HashSet<RequestForInformationAnswer>();
        }
    
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int CodeProjectId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }
    
        public virtual CodeProject CodeProject { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestForInformationAnswer> RequestForInformationAnswer { get; set; }
    }
}