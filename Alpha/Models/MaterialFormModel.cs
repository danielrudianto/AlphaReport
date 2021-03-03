using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class MaterialFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int Status { get; set; }
        public int CodeReportId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }

        public static Material MapDbObject(MaterialFormModel value)
        {
            Material material = new Material();
            material.Id = value.Id;
            material.Name = value.Name;
            material.Description = value.Description;
            material.Quantity = value.Quantity;
            material.Status = value.Status;
            material.CodeReportId = value.CodeReportId;
            material.Unit = value.Unit;

            return material;
        }
    }
}