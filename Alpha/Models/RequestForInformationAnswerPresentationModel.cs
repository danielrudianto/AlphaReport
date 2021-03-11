using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class RequestForInformationAnswerPresentationModel
    {
        public int? Id;
        public string Answer { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByImage { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int RequestForInformationId { get; set; }
        public RequestForInformationAnswerPresentationModel(RequestForInformationAnswer value)
        {
            Id = value.Id;
            UserId = value.CreatedBy;
            Answer = value.Answer;
            CreatedBy = value.User.FirstName + " " + value.User.LastName;
            CreatedByImage = value.User.ImageUrl;
            CreatedDate = value.CreatedDate;
            RequestForInformationId = value.RequestForInformationId;
        }
    }
}