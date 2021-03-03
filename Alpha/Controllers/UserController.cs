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
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class UserController : ApiController
    {
        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        public UserPresentationModel GetById(int UserId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == UserId).First();
            if(user != null)
            {
                return new UserPresentationModel(user);
            } else
            {
                return null;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        public List<UserPresentationModel> Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                alphaReportEntities dbContext = new alphaReportEntities();
                List<UserPresentationModel> response = new List<UserPresentationModel>();
                List<User> users = dbContext.User.OrderBy(x => x.FirstName).ToList();
                foreach (User user in users)
                {
                    response.Add(new UserPresentationModel(user));
                }
                return response;
            } else
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public bool CheckAvailbility(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            return (dbContext.User.Where(x => x.Id == Id && x.IsActive == 1).ToList().Count > 0) ? true : false;
        }

        public int Get(string token, string email)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Email == email && x.IsActive == 1).FirstOrDefault();
            if(user == null)
            {
                return 0;
            } else
            {
                var count = dbContext.UserToken.Where(x => x.UserId == user.Id && x.Token == token).Count();
                if(count == 0)
                {
                    UserToken userToken = new UserToken();
                    userToken.UserId = user.Id;
                    userToken.Token = token;

                    dbContext.UserToken.Add(userToken);
                    dbContext.SaveChanges();
                }
                return 1;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPut]
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

        [AllowAnonymous]
        [Authorize]
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

        [AllowAnonymous]
        [Authorize]
        [HttpDelete]
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

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public int ResetPassword(ResetPasswordFormModel value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value.OldPassword);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            string password = Convert.ToBase64String(inArray);

            byte[] newBytes = Encoding.Unicode.GetBytes(value.NewPassword);
            byte[] newnIArray = HashAlgorithm.Create("SHA1").ComputeHash(newBytes);
            string newPassword = Convert.ToBase64String(newnIArray);

            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == value.UserId && x.Password == password).FirstOrDefault();
            if(user != null)
            {
                user.Password = newPassword;
                return dbContext.SaveChanges();
            } else
            {
                return 0;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
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
