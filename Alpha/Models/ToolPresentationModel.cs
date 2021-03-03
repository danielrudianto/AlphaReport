using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class ToolPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CodeReportId { get; set; }

        public ToolPresentationModel(Tool value)
        {
            Id = value.Id;
            Name = value.Name;
            Description = value.Description;
            Quantity = value.Quantity;
            CodeReportId = value.CodeReportId;
        }
    }
}