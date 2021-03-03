using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class MaterialPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public int Status { get; set; }
        public string Unit { get; set; }
        
        public MaterialPresentationModel(Material value)
        {
            Name = value.Name;
            Description = value.Description;
            Unit = value.Unit;
            Id = value.Id;
            Quantity = value.Quantity;
            Status = value.Status;
        }
    }
}