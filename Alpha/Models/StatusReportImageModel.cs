using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class StatusReportImageModel
    {
        public int Id { get; set; }
        public int CodeReportId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }

        public StatusReportImageModel(StatusReportImage value)
        {
            Id = value.Id;
            CodeReportId = value.CodeReportId;
            ImageUrl = value.ImageUrl;
            Name = value.Name;
        }
    }
}