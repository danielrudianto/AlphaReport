using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Alpha.Models
{
    public class ClientFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TaxIdentificationNumber { get; set; }

        public static Client MapDbObject(ClientFormModel value)
        {
            Client client = new Client();
            client.Id = value.Id;
            client.Name = value.Name;
            client.Address = value.Address;
            client.City = value.City;
            client.PhoneNumber = value.PhoneNumber;
            client.TaxIdentificationNumber = value.TaxIdentificationNumber;
            if(value.Id == 0)
            {
                client.CreatedBy = 1;
                client.CreatedDate = DateTime.Now;
            }

            return client;
        }
    }
}