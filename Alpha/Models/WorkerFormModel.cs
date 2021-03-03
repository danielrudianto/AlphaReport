using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class WorkerFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CodeReportId { get; set; }

        public static Worker MapDbObject(WorkerFormModel value)
        {
            Worker worker = new Worker();
            worker.Id = value.Id;
            worker.Name = value.Name;
            worker.CodeReportId = value.CodeReportId;
            worker.Quantity = value.Quantity;

            return worker;
        }
    }
}