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
    public class ClientContactController : ApiController
    {
        public ClientContact Post(ClientContactFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ClientContact clientContact = ClientContactFormModel.MapDbObject(value);
            dbContext.ClientContact.Add(clientContact);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                return clientContact;
            } else
            {
                return null;
            }
        }

        public int Put(ClientContactFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ClientContact clientContact = new ClientContact();
            clientContact = dbContext.ClientContact.Where(x => x.Id == value.Id).FirstOrDefault();
            if(clientContact != null)
            {
                clientContact.Name = value.Name;
                clientContact.PhoneNumber = value.PhoneNumber;
                clientContact.Email = value.Email;
                clientContact.Position = value.Position;
                return dbContext.SaveChanges();
            } else
            {
                return 0;
            }
            
        }

        public int Delete(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ClientContact clientContact = new ClientContact();
            clientContact = dbContext.ClientContact.Where(x => x.Id == Id).FirstOrDefault();
            if(clientContact != null)
            {
                dbContext.ClientContact.Remove(clientContact);
                return dbContext.SaveChanges();
            } else
            {
                return 0;
            }
            
        }
    }
}
