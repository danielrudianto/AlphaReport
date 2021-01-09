using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class ClientPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool HasRelation { get; set; }
        public List<ClientContactPresentationModel> Contacts { get; set; }

        public ClientPresentationModel(Client client)
        {
            List<ClientContact> clientContacts = client.ClientContact.Where(x => x.ClientId == client.Id).ToList();
            List<ClientContactPresentationModel> clientContactList = new List<ClientContactPresentationModel>();
            foreach (ClientContact clientContact in clientContacts) {
                clientContactList.Add(new ClientContactPresentationModel(clientContact));
            }

            Id = client.Id;
            Name = client.Name;
            Address = client.Address;
            City = client.City;
            PhoneNumber = client.PhoneNumber;

            TaxIdentificationNumber = client.TaxIdentificationNumber;
            if(client.User != null)
            {
                CreatedBy = client.User.FirstName + " " + client.User.LastName;
            } else
            {
                CreatedBy = "";
            }
            
            CreatedDate = client.CreatedDate;
            HasRelation = client.CodeProject.Any();
            Contacts = clientContactList;
        }
    }
}