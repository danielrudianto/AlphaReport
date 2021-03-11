﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Alpha.Models
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class ReportFeedController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public List<object> Get(int CodeProjectId, int Skipped = 0)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<CodeReport> Reports = new List<CodeReport>();
            Reports = dbContext.CodeReport.Where(x => x.CodeProjectId == CodeProjectId && x.IsDelete == 0).ToList();
            List<RequestForInformation> Rfis = new List<RequestForInformation>();

            Rfis = dbContext.RequestForInformation.Where(x => x.CodeProjectId == CodeProjectId && x.IsDelete == 0).ToList();
            Rfis.ForEach(Rfi =>
            {
                var CodeReport = new CodeReport();
                CodeReport.Id = Rfi.Id;
                CodeReport.Type = 5;
                CodeReport.CreatedDate = Rfi.CreatedDate;
                CodeReport.CreatedBy = Rfi.User.Id;
                CodeReport.User = dbContext.User.Where(x => x.Id == Rfi.User.Id).First();
                CodeReport.CodeProjectId = Rfi.CodeProjectId;

                Reports.Add(CodeReport);
            });

            var ReportResult = Reports.OrderByDescending(x => x.CreatedDate).Skip(Skipped).Take(10);
            var Result = ReportResult.ToList();

            List<object> EndResult = new List<object>();
            Result.ForEach(R =>
            {
                object O = new object();
                switch (R.Type)
                {
                    case 1:
                        O = WorkerReportPresentationModel.ParseReport(R);
                        break;
                    case 2:
                        O = ToolReportPresentationModel.ParseReport(R);
                        break;
                    case 3:
                        O = MaterialReportPresentationModel.ParseReport(R);
                        break;
                    case 4:
                        O = WeatherReportPresentationModel.ParseReport(R);
                        break;
                    case 5:
                        O = new RequestForInformationPresentationModel(dbContext.RequestForInformation.Where(x => x.Id == R.Id).First());
                        break;
                    case 6:
                        R.User = dbContext.User.Where(x => x.Id == R.CreatedBy).First();
                        O = DailyReportPresentationModel.ParseReport(R);
                        break;
                    case 7:
                        O = StatusReportPresentationModel.ParseReport(R);
                        break;
                };

                EndResult.Add(O);
            });

            return EndResult;
        }

        [Authorize]
        [AllowAnonymous]
        [HttpDelete]
        public int Delete(int Id, int UserId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == UserId).First();

            UserPresentationModel userPresentationModel = new UserPresentationModel(user);
            var position = userPresentationModel.LastPosition.PositionId.Value;

            CodeReport codeReport = new CodeReport();
            codeReport = dbContext.CodeReport.Where(x => x.Id == Id && x.IsDelete == 0).FirstOrDefault();
            if(codeReport != null && position > 2)
            {
                codeReport.IsDelete = 1;
                var result = dbContext.SaveChanges();

                if(result == 1)
                {
                    NotificationHub.DeleteFeed(Id);
                }

                return result;
            } else
            {
                return 0;
            }
        }
    }
}