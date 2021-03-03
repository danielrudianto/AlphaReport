using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class StatusReportFormModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public string Status { get; set; }
        public Boolean Images { get; set; }
    }
}