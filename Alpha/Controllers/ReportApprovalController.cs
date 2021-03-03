using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Alpha.Models
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class ReportApprovalController : ApiController
    {
        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        public List<CodeReportApprovalPresentationModel> Get(int CodeReportId)
        {
            List<CodeReportApprovalPresentationModel> Response = new List<CodeReportApprovalPresentationModel>();
            alphaReportEntities dbContext = new alphaReportEntities();
            List<CodeReportApproval> Approvals = new List<CodeReportApproval>();
            Approvals = dbContext.CodeReportApproval.Where(x => x.CodeReportId == CodeReportId).ToList();
            Approvals.ForEach(x =>
            {
                Response.Add(new CodeReportApprovalPresentationModel(x));
            });

            return Response;
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("Api/ReportApproval/ApproveReport")]
        public int ApproveReport(ReportApprovalFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == value.CodeReportId).First();
            if(codeReport == null)
            {
                return 0;
            } else
            {
                var item = ReportApprovalFormModel.mapDbObject(value);
                dbContext.CodeReportApproval.Add(item);
                var result = dbContext.SaveChanges();
                if(result == 1)
                {
                    item.User = dbContext.User.Where(x => x.Id == value.UserId).First();
                    NotificationHub.UpdateApprovals(item);
                }

                return result;
            }
        }
    }
}