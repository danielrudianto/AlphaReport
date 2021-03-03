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
    public class StatusReportController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public int? Post(StatusReportFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeReport codeReport = new CodeReport();
            codeReport.Id = 0;
            codeReport.CreatedBy = value.CreatedBy;
            codeReport.CreatedDate = DateTime.Now;
            codeReport.CodeProjectId = value.CodeProjectId;
            codeReport.Date = DateTime.Now;
            codeReport.Type = value.Type;

            dbContext.CodeReport.Add(codeReport);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                var id = codeReport.Id;
                StatusReport statusReport = new StatusReport();
                statusReport.CodeReportId = codeReport.Id;
                statusReport.Status = value.Status;

                dbContext.StatusReport.Add(statusReport);
                dbContext.SaveChanges();

                var report = dbContext.CodeReport.Where(x => x.Id == id).First();
                report.User = dbContext.User.Where(x => x.Id == value.CreatedBy).First();
                if(report != null)
                {
                    if (!value.Images)
                    {
                        NotificationHub.NewFeed(StatusReportPresentationModel.ParseReport(report));
                    }
                }
                
                return statusReport.Id;
            } else
            {
                return null;
            }
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public void UploadImages()
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            var httpRequest = HttpContext.Current.Request;
            var Files = httpRequest.Files;
            Array array = Files.AllKeys;
            int CodeStatusReportId = Int32.Parse(httpRequest.Params["CodeReportId"]);

            foreach (string keys in array)
            {
                var File = Files.Get(keys);
                if (File != null && File.ContentLength > 0)
                {
                    var ext = File.FileName.Substring(File.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    var guid = Guid.NewGuid();
                    string filePath = HttpContext.Current.Server.MapPath("~/StatusReport/" + guid + extension);
                    File.SaveAs(filePath);

                    var document = new StatusReportImage();
                    document.Name = File.FileName;
                    document.ImageUrl = "StatusReport/" + guid + extension;
                    document.CodeReportId = CodeStatusReportId;

                    dbContext.StatusReportImage.Add(document);
                }
            }

            dbContext.SaveChanges();
            StatusReport report = new StatusReport();
            report = dbContext.StatusReport.Where(x => x.Id == CodeStatusReportId).First();
            var reportId = report.CodeReportId;

            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == reportId).First();
            codeReport.User = dbContext.User.Where(x => x.Id == codeReport.CreatedBy).First();

            NotificationHub.NewFeed(StatusReportPresentationModel.ParseReport(codeReport));
        }
    }
}
