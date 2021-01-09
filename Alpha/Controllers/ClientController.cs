using Newtonsoft.Json;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class ClientController : ApiController
    {
        public List<ClientPresentationModel> Get()
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<ClientPresentationModel> response = new List<ClientPresentationModel>();
            List<Client> clients = dbContext.Client.OrderBy(x => x.Name).ToList();
            foreach(Client client in clients)
            {
                response.Add(new ClientPresentationModel(client));
            }
            return response;
        }

        public ClientPresentationModel Post(ClientFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            Client client = ClientFormModel.MapDbObject(value);
            dbContext.Client.Add(client);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                ClientPresentationModel response = new ClientPresentationModel(client);
                return response;
            } else
            {
                return null;
            }
        }

        public ClientPresentationModel Put(ClientFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            Client client = dbContext.Client.Where(x => x.Id == value.Id).FirstOrDefault();
            if(client != null)
            {
                client.Name = value.Name;
                client.Address = value.Address;
                client.City = value.City;
                client.PhoneNumber = value.PhoneNumber;
                client.TaxIdentificationNumber = value.TaxIdentificationNumber;
                int result = dbContext.SaveChanges();
                if(result == 1)
                {
                    return new ClientPresentationModel(client);
                } else
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }

        public int Delete(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            Client client = dbContext.Client.Where(x => x.Id == Id).FirstOrDefault();
            if (client != null)
            {
                //Relationship check//
                if (!client.CodeProject.Any())
                {
                    List<ClientContact> clientContacts = new List<ClientContact>();
                    clientContacts = dbContext.ClientContact.Where(x => x.ClientId == Id).ToList();
                    foreach(ClientContact clientContact in clientContacts)
                    {
                        dbContext.ClientContact.Remove(clientContact);
                        
                    }
                    dbContext.SaveChanges();

                    dbContext.Client.Remove(client);
                    return dbContext.SaveChanges();
                } else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
