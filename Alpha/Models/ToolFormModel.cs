using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class ToolFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CodeReportId { get; set; }

        public static Tool MapDbObject(ToolFormModel value)
        {
            Tool tool = new Tool();
            tool.Id = value.Id;
            tool.Name = value.Name;
            tool.Description = value.Description;
            tool.Quantity = value.Quantity;
            tool.CodeReportId = value.CodeReportId;

            return tool;
        }
    }
}