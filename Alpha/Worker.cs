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
    
    public partial class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CodeReportId { get; set; }
        public int Quantity { get; set; }
    
        public virtual CodeReport CodeReport { get; set; }
    }
}
