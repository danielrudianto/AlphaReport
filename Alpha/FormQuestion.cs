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
    
    public partial class FormQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormQuestion()
        {
            this.AnswerForm = new HashSet<AnswerForm>();
        }
    
        public int Id { get; set; }
        public string Question { get; set; }
        public int CodeFormId { get; set; }
        public byte IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerForm> AnswerForm { get; set; }
        public virtual CodeForm CodeForm { get; set; }
    }
}
