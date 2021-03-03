using Alpha.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class WeatherReportController : ApiController
    {
        public int Post(WeatherReportFormModel value)
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
                Weather weatherForm = new Weather();
                weatherForm.WeatherId = value.WeatherId;
                weatherForm.CodeReportId = reportId;
                dbContext.Weather.Add(weatherForm);
                dbContext.SaveChanges();

                var report = dbContext.CodeReport.Where(x => x.Id == reportId).First();
                report.User = dbContext.User.Where(x => x.Id == report.CreatedBy).First();
                NotificationHub.NewFeed(WeatherReportPresentationModel.ParseReport(report));

                dbContext.SaveChanges();
            }
            
            return 1;
        }

        public WeatherReportPresentationModel Get(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            WeatherReportPresentationModel response = new WeatherReportPresentationModel();
            CodeReport Report = new CodeReport();
            Weather Weather = new Weather();

            Report = dbContext.CodeReport.Where(x => x.Id == Id).First();
            response = WeatherReportPresentationModel.ParseReport(Report);

            return response;
        }
    }
}