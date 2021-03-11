using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class StatusReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public string Status { get; set; }
        public List<StatusReportImageModel> Images { get; set; }
        public List<CodeReportApprovalPresentationModel> Approvals { get; set; }

        public static StatusReportPresentationModel ParseReport(CodeReport report)
        {
            var statusText = report.StatusReport.First();
            StatusReportPresentationModel response = new StatusReportPresentationModel();
            response.Id = report.Id;
            response.Type = 7;
            response.CreatedBy = report.User.FirstName + " " + report.User.LastName;
            response.CreatedDate = report.CreatedDate;
            response.CodeProjectId = report.CodeProjectId;
            response.Status = statusText.Status;
            response.Images = new List<StatusReportImageModel>();
            response.Approvals = new List<CodeReportApprovalPresentationModel>();

            var statusReportRef = report.StatusReport.First();
            List<StatusReportImage> images = new List<StatusReportImage>();
            images = statusReportRef.StatusReportImage.ToList();
            images.ForEach(image =>
            {
                response.Images.Add(new StatusReportImageModel(image));
            });

            List<CodeReportApproval> approvalsDb = new List<CodeReportApproval>();
            approvalsDb = report.CodeReportApproval.Where(x => x.IsDelete == 0).ToList();
            approvalsDb.ForEach(approvalDb =>
            {
                response.Approvals.Add(new CodeReportApprovalPresentationModel(approvalDb));
            });

            return response;
        }
    }
}