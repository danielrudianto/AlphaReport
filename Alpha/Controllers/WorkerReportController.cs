using Alpha.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Security.Claims;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class WorkerReportController : ApiController
    {
        [System.Web.Http.Authorize]
        [AllowAnonymous]
        [HttpPost]
        public int Post(WorkerReportFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeReport codeReport = new CodeReport();
            codeReport.CodeProjectId = value.CodeProjectId;
            codeReport.CreatedBy = value.CreatedBy;
            codeReport.CreatedDate = DateTime.Now;
            codeReport.Date = DateTime.Today;
            codeReport.Type = value.Type;

            dbContext.CodeReport.Add(codeReport);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                var reportId = codeReport.Id;
                var workers = value.Workers;
                workers.ForEach(worker =>
                {
                    if(worker.Quantity > 0 && worker.Name != "")
                    {
                        
                        Worker workerItem = new Worker();
                        workerItem = WorkerFormModel.MapDbObject(worker);
                        workerItem.CodeReportId = reportId;
                        dbContext.Worker.Add(workerItem);
                    }
                    
                });
                var report = dbContext.CodeReport.Where(x => x.Id == reportId).First();
                report.User = dbContext.User.Where(x => x.Id == report.CreatedBy).First();
                NotificationHub.NewFeed(WorkerReportPresentationModel.ParseReport(report));

                dbContext.SaveChanges();
                return 1;
            } else
            {
                return 0;
            }
            
            
        }

        [System.Web.Http.Authorize]
        [AllowAnonymous]
        [HttpGet]
        public WorkerReportPresentationModel Get(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            WorkerReportPresentationModel response = new WorkerReportPresentationModel();
            CodeReport Report = new CodeReport();
            Report = dbContext.CodeReport.Where(x => x.Id == Id).First();
            response = WorkerReportPresentationModel.ParseReport(Report);
            return response;
        }
    }
}