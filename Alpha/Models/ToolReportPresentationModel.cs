using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class ToolReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public List<ToolPresentationModel> Tools { get; set; }
        public List<CodeReportApprovalPresentationModel> Approvals { get; set; }

        public static ToolReportPresentationModel ParseReport(CodeReport report)
        {
            ToolReportPresentationModel response = new ToolReportPresentationModel();
            response.Id = report.Id;
            response.Type = report.Type;
            response.CreatedBy = report.User.FirstName + " " + report.User.LastName;
            response.CreatedDate = report.CreatedDate;
            response.CodeProjectId = report.CodeProjectId;
            response.Tools = new List<ToolPresentationModel>();
            response.Approvals = new List<CodeReportApprovalPresentationModel>();

            List<Tool> ToolsDb = new List<Tool>();
            ToolsDb = report.Tool.ToList();
            ToolsDb.ForEach(toolDb =>
            {
                response.Tools.Add(new ToolPresentationModel(toolDb));
            });

            List<CodeReportApproval> approvalsDb = new List<CodeReportApproval>();
            approvalsDb = report.CodeReportApproval.ToList();
            approvalsDb.ForEach(approvalDb =>
            {
                response.Approvals.Add(new CodeReportApprovalPresentationModel(approvalDb));
            });

            return response;
        }
    }
}