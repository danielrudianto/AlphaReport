using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class WorkerReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public List<WorkerPresentationModel> Workers { get; set; }
        public List<CodeReportApprovalPresentationModel> Approvals { get; set; }

        public static WorkerReportPresentationModel ParseReport(CodeReport report)
        {
            WorkerReportPresentationModel response = new WorkerReportPresentationModel();
            response.Id = report.Id;
            response.Type = report.Type;
            response.CreatedBy = report.User.FirstName + " " + report.User.LastName;
            response.CreatedDate = report.CreatedDate;
            response.CodeProjectId = report.CodeProjectId;
            response.Workers = new List<WorkerPresentationModel>();
            response.Approvals = new List<CodeReportApprovalPresentationModel>();

            List<Worker> workersDb = new List<Worker>();
            workersDb = report.Worker.ToList();
            workersDb.ForEach(workerDb =>
            {
                response.Workers.Add(new WorkerPresentationModel(workerDb));
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