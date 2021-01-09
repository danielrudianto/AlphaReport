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
    public class UserController : ApiController
    {
        public List<UserPresentationModel> Get()
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<UserPresentationModel> response = new List<UserPresentationModel>();
            List<User> users = dbContext.User.OrderBy(x => x.FirstName).ToList();
            foreach(User user in users)
            {
                response.Add(new UserPresentationModel(user));
            }
            return response;
        }

        public UserPresentationModel Put(UserFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == value.Id).FirstOrDefault();
            if(user != null)
            {
                user.FirstName = value.FirstName;
                user.LastName = value.LastName;
                user.Email = value.Email;

                int result = dbContext.SaveChanges();
                if(result == 1)
                {
                    return new UserPresentationModel(user);
                } else
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("Api/User/Enable")]
        public int Enable(int id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = 1;
                return dbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == id).FirstOrDefault();
            if(user != null)
            {
                user.IsActive = 0;
                return dbContext.SaveChanges();
            } else
            {
                return 0;
            }
        }

        public UserPresentationModel Post(UserFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = UserFormModel.MapDbObject(value);

            dbContext.User.Add(user);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                int position = value.Position;
                UserPosition userPosition = new UserPosition();
                userPosition.CreatedBy = 1;
                userPosition.CreatedDate = DateTime.Now;
                userPosition.EffectiveDate = DateTime.Now;
                userPosition.Position = position;

                userPosition.UserId = user.Id;

                dbContext.UserPosition.Add(userPosition);
                int positionResult = dbContext.SaveChanges();

                if(positionResult == 1)
                {
                    return new UserPresentationModel(dbContext.User.Where(x => x.Id == user.Id).First());
                } else
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
