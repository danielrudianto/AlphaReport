using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class UserLoginFormModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime Time { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public UserLoginFormModel(UserLogin users)
        {
        }

        public static UserLogin MapDbObject(UserLoginFormModel dbObject)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Id = dbObject.Id;
            userLogin.DateTime = DateTime.Now;
            userLogin.UserId = dbObject.UserId;

            return userLogin;
        }
    }
}