using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Alpha.Models
{
    public class ClientContactPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public ClientContactPresentationModel(ClientContact value)
        {
            Id = value.Id;
            Name = value.Name;
            Position = value.Position;
            PhoneNumber = value.PhoneNumber;
            Email = value.Email;
            CreatedBy = value.User.FirstName + " " + value.User.LastName;
            CreatedDate = value.CreatedDate;
        }
    }
}