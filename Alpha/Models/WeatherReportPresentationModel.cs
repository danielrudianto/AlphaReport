using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class WeatherReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public int WeatherId { get; set; }

        public static WeatherReportPresentationModel ParseReport(CodeReport report)
        {
            WeatherReportPresentationModel response = new WeatherReportPresentationModel();
            response.Id = report.Id;
            response.Type = 4;
            response.CreatedBy = report.User.FirstName + " " + report.User.LastName;
            response.CreatedDate = report.CreatedDate;
            response.CodeProjectId = report.CodeProjectId;

            Weather weather = new Weather();
            weather = report.Weather.FirstOrDefault();
            if(weather == null)
            {
                response.WeatherId = 0;
                return response;
            } else
            {
                response.WeatherId = weather.WeatherId;
                return response;
            }
        }
    }
}