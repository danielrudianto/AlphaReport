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
    
    public partial class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<double> BudgetPrice { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Done { get; set; }
        public byte IsDelete { get; set; }
        public int CodeProjectId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<double> EstimatedDuration { get; set; }
        public Nullable<int> Timeline { get; set; }
        public Nullable<double> Price { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
    
        public virtual CodeProject CodeProject { get; set; }
    }
}
