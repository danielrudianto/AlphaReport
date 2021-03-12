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
        [HttpGet]
        [Route("Api/ReportApproval/GetApprovals/{CodeReportId}")]
        public List<CodeReportApprovalPresentationModel> GetApprovals(int CodeReportId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();

            List<CodeReportApproval> approvals = new List<CodeReportApproval>();
            List<CodeReportApprovalPresentationModel> response = new List<CodeReportApprovalPresentationModel>();
            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == CodeReportId).FirstOrDefault();
            if(codeReport != null)
            {
                approvals = codeReport.CodeReportApproval.ToList();
                approvals.ForEach(x =>
                {
                    response.Add(new CodeReportApprovalPresentationModel(x));
                });
            }

            return response;
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
                var validateProject = dbContext.CodeProject.Where(x => x.Id == codeReport.CodeProjectId && x.IsCompleted == 0 && x.IsDelete == 0).Any();
                if (validateProject)
                {
                    var item = ReportApprovalFormModel.mapDbObject(value);
                    item.User = dbContext.User.Where(x => x.Id == value.UserId).First();
                    var existingApprovals = dbContext.CodeReportApproval.Where(x => x.CodeReportId == item.CodeReportId && x.Approval != 0 && x.CreatedBy == value.UserId).Any();
                    switch (item.Approval)
                    {
                        case 1:
                            if(!existingApprovals)
                            {
                                dbContext.CodeReportApproval.Add(item);
                                int result1 = dbContext.SaveChanges();
                                if(result1 == 1)
                                {
                                    NotificationHub.UpdateApprovals(item);
                                }
                            } else
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (!existingApprovals)
                            {
                                dbContext.CodeReportApproval.Add(item);
                                int result2 = dbContext.SaveChanges();
                                if (result2 == 1)
                                {
                                    NotificationHub.UpdateApprovals(item);
                                }
                            }
                            else
                            {
                                return 0;
                            }
                            break;
                        case 0:
                            dbContext.CodeReportApproval.Add(item);
                            int result3 = dbContext.SaveChanges();
                            if(result3 == 1)
                            {
                                NotificationHub.UpdateApprovals(item);
                            }
                            
                            break;
                    }
                    return 1;
                } else
                {
                    return 0;
                }
            }
        }
    }
}