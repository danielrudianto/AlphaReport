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
using System.Text;
using System.Security.Cryptography;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class UserLoginController : ApiController
    {
        public UserPresentationModel Post(UserLoginFormModel value)
        {
            string email = value.Email;
            byte[] bytes = Encoding.Unicode.GetBytes(value.Password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            string password = Convert.ToBase64String(inArray);

            alphaReportEntities dbContext = new alphaReportEntities();

            User user =dbContext.User.Where(x => x.Email == email && x.IsActive == 1 && x.Password == password).FirstOrDefault();
            if(user != null)
            {
                UserLogin userLogin = new UserLogin();
                value.UserId = user.Id;
                userLogin = UserLoginFormModel.MapDbObject(value);
                dbContext.UserLogin.Add(userLogin);
                dbContext.SaveChanges();

                return new UserPresentationModel(user);
            } else
            {
                return null;
            }
        }
    }
}
