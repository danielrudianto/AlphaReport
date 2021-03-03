using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class WorkerPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CodeReportId { get; set; }

        public WorkerPresentationModel(Worker value)
        {
           Id = value.Id;
           Name = value.Name;
           CodeReportId = value.CodeReportId;
           Quantity = value.Quantity;
        }
    }
}