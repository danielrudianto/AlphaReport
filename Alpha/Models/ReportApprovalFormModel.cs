using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class ReportApprovalFormModel
    {
        public int CodeReportId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public byte Approval { get; set; }
        public string Comment { get; set; }

        public static CodeReportApproval mapDbObject(ReportApprovalFormModel value)
        {
            CodeReportApproval codeReportApproval = new CodeReportApproval();
            codeReportApproval.CodeReportId = value.CodeReportId;
            codeReportApproval.Approval = value.Approval;
            codeReportApproval.Comment = (value.Comment == "") ? null : value.Comment;
            codeReportApproval.CreatedBy = value.UserId;
            codeReportApproval.CreatedDate = DateTime.Now;

            return codeReportApproval;
        }
    }
}