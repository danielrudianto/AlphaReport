using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class WeatherPresentationModel
    {
        public int Id { get; set; }
        public int WeatherId { get; set; }
        public int CodeReportId { get; set; }

        public WeatherPresentationModel(Weather value)
        {
            Id = value.Id;
            WeatherId = value.WeatherId;
            CodeReportId = value.CodeReportId;
        }
    }
}