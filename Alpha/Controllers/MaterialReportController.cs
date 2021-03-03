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
    public class MaterialReportController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public int? Post(MaterialReportFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeReport codeReport = new CodeReport();

            double totalQuantity = 0;
            bool validation = true;
            List<MaterialFormModel> materialForm = new List<MaterialFormModel>();
            materialForm = value.Materials;
            materialForm.ForEach(x =>
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
                codeReport.CodeProjectId = value.CodeProjectId;
                codeReport.CreatedBy = value.CreatedBy;
                codeReport.CreatedDate = DateTime.Now;
                codeReport.Date = DateTime.Today;
                codeReport.Type = value.Type;

                dbContext.CodeReport.Add(codeReport);
                int result = dbContext.SaveChanges();
                if (result == 1)
                {
                    var reportId = codeReport.Id;
                    materialForm.ForEach(x =>
                    {
                        if (x.Quantity > 0 && x.Name != "")
                        {
                            x.CodeReportId = reportId;
                            dbContext.Material.Add(MaterialFormModel.MapDbObject(x));
                        }
                    });
                    dbContext.SaveChanges();
                    var report = dbContext.CodeReport.Where(x => x.Id == reportId).First();
                    report.User = dbContext.User.Where(x => x.Id == report.CreatedBy).First();
                    var response = MaterialReportPresentationModel.ParseReport(report);
                    NotificationHub.NewFeed(response);
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
        public MaterialReportPresentationModel Get(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            MaterialReportPresentationModel response = new MaterialReportPresentationModel();
            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == Id).First();
            response = MaterialReportPresentationModel.ParseReport(codeReport);
            return response;
        }
    }
}
