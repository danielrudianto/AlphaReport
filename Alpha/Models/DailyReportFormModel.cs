using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class DailyReportFormModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public List<DailyTaskFormModel> Tasks { get; set; }

    }

    public class DailyTaskFormModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double? Quantity { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
        public List<DailyChildrenTaskFormModel> Children { get; set; }
    }

    public class DailyChildrenTaskFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
    }
}