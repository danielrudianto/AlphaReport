using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class RequestForInformationDocumentPresentationModel
    {
        public int Id { get; set; }
        public int RequestForInformationId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }

        public RequestForInformationDocumentPresentationModel(RequestForInformationDocument value)
        {
            Id = value.Id;
            RequestForInformationId = value.RequestForInformationId;
            ImageUrl = value.ImageUrl;
            Name = value.Name;
        }
    }
}