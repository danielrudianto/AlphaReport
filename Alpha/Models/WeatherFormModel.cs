using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class WeatherFormModel
    {
        public int Id { get; set; }
        public int WeatherId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int CodeReportId { get; set; }

        public static Weather MapDbObject(WeatherFormModel value)
        {
            Weather weatherReport = new Weather();
            weatherReport.Id = value.Id;
            weatherReport.WeatherId = value.WeatherId;
            weatherReport.CodeReportId = value.CodeReportId;

            return weatherReport;
        }
    }
}