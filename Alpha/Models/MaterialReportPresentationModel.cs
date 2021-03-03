using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class MaterialReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public List<MaterialPresentationModel> Materials { get; set; }
        public List<CodeReportApprovalPresentationModel> Approvals { get; set; }

        public static MaterialReportPresentationModel ParseReport(CodeReport report)
        {
            MaterialReportPresentationModel response = new MaterialReportPresentationModel
            {
                Id = report.Id,
                Type = report.Type,
                CreatedBy = report.User.FirstName + " " + report.User.LastName,
                CreatedDate = report.CreatedDate,
                CodeProjectId = report.CodeProjectId,
                Materials = new List<MaterialPresentationModel>(),
                Approvals = new List<CodeReportApprovalPresentationModel>()
            };

            List<Material> MaterialsDb = new List<Material>();
            MaterialsDb = report.Material.ToList();
            MaterialsDb.ForEach(materialDb =>
            {
                response.Materials.Add(new MaterialPresentationModel(materialDb));
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