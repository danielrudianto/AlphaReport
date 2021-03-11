using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class RequestForInformationFormModel
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int CodeProjectId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }
        public string AddressedFor { get; set; }

        public RequestForInformationFormModel(RequestForInformation value)
        { 
        }

        public static RequestForInformation MapDbObject(RequestForInformationFormModel value)
        {
            RequestForInformation rfi = new RequestForInformation();
            rfi.Id = value.Id;
            rfi.Header = value.Header;
            rfi.Description = value.Description;
            rfi.AddressedFor = value.AddressedFor;
            rfi.CreatedBy = value.CreatedBy;
            rfi.CreatedDate = DateTime.Now;
            rfi.CodeProjectId = value.CodeProjectId;

            return rfi;
        }
    }
}