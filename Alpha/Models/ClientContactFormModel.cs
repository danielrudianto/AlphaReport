using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Alpha.Models
{
    public class ClientContactFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ClientId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public static ClientContact MapDbObject(ClientContactFormModel value)
        {
            ClientContact clientContact = new ClientContact();
            clientContact.Id = value.Id;
            clientContact.Name = value.Name;
            clientContact.Position = value.Position;
            clientContact.Email = value.Email;
            clientContact.PhoneNumber = value.PhoneNumber;
            clientContact.ClientId = value.ClientId;
            if(value.Id == 0)
            {
                clientContact.CreatedBy = 1;
                clientContact.CreatedDate = DateTime.Now;
            }

            return clientContact;
        }
    }
}