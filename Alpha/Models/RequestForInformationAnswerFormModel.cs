using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class RequestForInformationAnswerFormModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int RequestForInformationId { get; set; }

        public static RequestForInformationAnswer MapDbObject(RequestForInformationAnswerFormModel value)
        {
            RequestForInformationAnswer response = new RequestForInformationAnswer
            {
                Id = 0,
                RequestForInformationId = value.RequestForInformationId,
                CreatedBy = value.CreatedBy,
                CreatedDate = DateTime.Now,
                Answer = value.Answer
            };

            return response;
        }
    }
}