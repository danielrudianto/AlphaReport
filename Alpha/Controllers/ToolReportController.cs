using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class ToolReportController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public int? Post(ToolReportFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();

            int totalQuantity = 0;
            bool validation = true;
            List<ToolFormModel> toolForm = new List<ToolFormModel>();
            toolForm = value.Tools;
            toolForm.ForEach(x =>
            {
                if (x.Quantity > 0 && x.Name != "")
                {
                    totalQuantity += x.Quantity;
                } else if(x.Name == "")
                {
                    validation = false;
                }
            });

            if(validation && totalQuantity > 0)
            {
                CodeReport codeReport = new CodeReport();
                codeReport.Id = value.Id;
                codeReport.Type = 2;
                codeReport.CodeProjectId = value.CodeProjectId;
                codeReport.CreatedBy = value.CreatedBy;
                codeReport.CreatedDate = DateTime.Now;
                codeReport.Date = DateTime.Now;

                dbContext.CodeReport.Add(codeReport);
                int result = dbContext.SaveChanges();
                if (result == 1)
                {
                    var reportId = codeReport.Id;
                    toolForm.ForEach(x =>
                    {
                        if (x.Quantity > 0 && x.Name != "")
                        {
                            x.Description = String.IsNullOrEmpty(x.Description) ? "" : x.Description;
                            x.CodeReportId = reportId;
                            dbContext.Tool.Add(ToolFormModel.MapDbObject(x));
                        }
                    });
                    dbContext.SaveChanges();
                    var report = dbContext.CodeReport.Where(x => x.Id == reportId).First();
                    report.User = dbContext.User.Where(x => x.Id == report.CreatedBy).First();

                    NotificationHub.NewFeed(ToolReportPresentationModel.ParseReport(report));
                    return reportId;
                } else
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public ToolReportPresentationModel Get(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ToolReportPresentationModel response = new ToolReportPresentationModel();
            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == Id).First();
            response = ToolReportPresentationModel.ParseReport(codeReport);
            return response;
        }
    }
}
