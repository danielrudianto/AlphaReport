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
    public class DailyReportController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public int? Post(DailyReportFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeReport codeReport = new CodeReport
            {
                Id = 0,
                CreatedBy = value.CreatedBy,
                CreatedDate = DateTime.Now,
                CodeProjectId = value.CodeProjectId,
                Date = DateTime.Now,
                Type = 6
            };

            dbContext.CodeReport.Add(codeReport);
            int result = dbContext.SaveChanges();
            if (result == 1)
            {
                var id = codeReport.Id;
                List<DailyTaskFormModel> Tasks = new List<DailyTaskFormModel>();
                Tasks = value.Tasks;
                Tasks.ForEach(Task =>
                {
                    if (Task.Children.Count > 0)
                    {
                        DailyTask dailyTask = new DailyTask
                        {
                            Id = 0,
                            Name = Task.Name,
                            Description = Task.Description,
                            CodeReportId = id,
                            ParentId = null,
                            Quantity = null,
                            Unit = null,
                            Note = null
                        };

                        dbContext.DailyTask.Add(dailyTask);
                        var taskResult = dbContext.SaveChanges();
                        if (taskResult == 1)
                        {
                            var parentId = dailyTask.Id;
                            Task.Children.ForEach(childTask =>
                            {
                                DailyTask childTaskModel = new DailyTask
                                {
                                    Id = 0,
                                    Name = childTask.Name,
                                    Description = childTask.Description,
                                    CodeReportId = id,
                                    ParentId = parentId,
                                    Quantity = childTask.Quantity,
                                    Unit = childTask.Unit,
                                    Note = childTask.Note
                                };

                                dbContext.DailyTask.Add(childTaskModel);
                            });

                            dbContext.SaveChanges();
                        }
                    }
                });

                codeReport.User = dbContext.User.Where(x => x.Id == codeReport.CreatedBy).First();
                NotificationHub.NewFeed(DailyReportPresentationModel.ParseReport(codeReport));
                return codeReport.Id;
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
                    string filePath = HttpContext.Current.Server.MapPath("~/DailyReport/" + guid + extension);
                    File.SaveAs(filePath);

                    var document = new DailyReportImage();
                    document.Name = File.FileName;
                    document.ImageUrl = "DailyReport/" + guid + extension;
                    document.CodeReportId = CodeStatusReportId;
                    
                    dbContext.DailyReportImage.Add(document);
                }
            }

            dbContext.SaveChanges();
            CodeReport report = new CodeReport();
            report = dbContext.CodeReport.Where(x => x.Id == CodeStatusReportId).First();
            var reportId = report.Id;

            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == reportId).First();
            codeReport.User = dbContext.User.Where(x => x.Id == codeReport.CreatedBy).First();

            NotificationHub.NewFeed(DailyReportPresentationModel.ParseReport(codeReport));
        }
    }
}
