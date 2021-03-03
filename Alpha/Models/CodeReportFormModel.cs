using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Alpha.Models
{
    public class CodeReportFormModel
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime Date { get; set; }
        public int CodeProjectId { get; set; }
        public int Type { get; set; }
        // Report type //
        //1. Worker
        //2. Tools
        //3. Material
        //4. Weather
        //5. RFI
        //6. Daily 
        //7. Progress
        public byte IsConfirm { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }

        public CodeReportFormModel()
        {
        }

        public static CodeReport MapDbObject(CodeReportFormModel value)
        {
            CodeReport codeReport = new CodeReport();

            codeReport.Id = value.Id;
            codeReport.CreatedBy = value.CreatedBy;
            codeReport.CreatedDate = DateTime.Now;
            codeReport.Date = DateTime.Today;
            codeReport.CodeProjectId = value.CodeProjectId;
            codeReport.Type = value.Type;
            codeReport.IsDelete = 0;
            codeReport.DeletedBy = null;
            codeReport.DeletedDate = null;

            return codeReport;
        }
    }
}