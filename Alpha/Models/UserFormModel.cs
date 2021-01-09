using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class UserFormModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte IsActive { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public int Position { get; set; }

        public static User MapDbObject(UserFormModel value)
        {
            User users = new User();
            users.Id = value.Id;
            users.FirstName = value.FirstName;
            users.LastName = value.LastName;
            users.Email = value.Email;
            users.ImageUrl = value.ImageUrl;
            
            if(value.Id == 0)
            {
                users.IsActive = 1;
            } else
            {
                users.IsActive = value.IsActive;
            }

            users.Password = Crypto.HashPassword(value.Password);

            return users;
        }
    }
}