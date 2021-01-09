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
    public class UserPositionController : ApiController
    {
        public UserPresentationModel Post(UserPositionFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            UserPosition userPosition = new UserPosition();
            userPosition = UserPositionFormModel.MapDbObject(value);

            dbContext.UserPosition.Add(userPosition);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                return new UserPresentationModel(dbContext.User.Where(x => x.Id == value.UserId).FirstOrDefault());
            } else {
                return null;
            }
        }

        public UserPresentationModel Delete(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            UserPosition userPosition = new UserPosition();
            int UserId = dbContext.UserPosition.Where(x => x.Id == Id).Select(y => y.UserId).FirstOrDefault();
            userPosition = dbContext.UserPosition.Where(x => x.Id == Id && x.EffectiveDate >= DateTime.Now).FirstOrDefault();
            int positionCount = dbContext.UserPosition.Where(x => x.UserId == UserId).Count();
            if(userPosition != null && positionCount > 1)
            {
                dbContext.UserPosition.Remove(userPosition);
                int result = dbContext.SaveChanges();
                if (result == 1)
                {
                    return new UserPresentationModel(dbContext.User.Where(x => x.Id == UserId).FirstOrDefault());
                }
                else
                {
                    return null;
                }
            } else
            {
                return null;
            }
            
        }
    }
}
