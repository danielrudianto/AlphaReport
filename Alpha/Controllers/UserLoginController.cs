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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class UserLoginController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public string Post(UserLoginFormModel value)
        {
            string email = value.Email;
            byte[] bytes = Encoding.Unicode.GetBytes(value.Password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            string password = Convert.ToBase64String(inArray);

            alphaReportEntities dbContext = new alphaReportEntities();

            User user = dbContext.User.Where(x => x.Email == email && x.IsActive == 1 && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                string key = "5ubPs7r8?WEFaXEEPknuf7Lupeb946uzWzcKhfUKPA2WdVzpB9Bkf6aEeDktwH7mMJqTPmVBQ67ZWQTVJb9vkDQqm9s2YEcXAndKjsnthDYujVatRAKNSmartReport";
                var issuer = "https://alpha.conveyor.cloud";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>();
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim("Email", user.Email));
                permClaims.Add(new Claim("FirstName", user.FirstName));
                permClaims.Add(new Claim("LastName", user.LastName));
                permClaims.Add(new Claim("ImageUrl", (user.ImageUrl == null) ? "" : user.ImageUrl));
                permClaims.Add(new Claim("Id", user.Id.ToString()));

                UserToken userToken = new UserToken();
                userToken.UserId = user.Id;
                userToken.Token = value.Token;
                dbContext.UserToken.Where(x => x.UserId != user.Id).ToList().ForEach(fbToken =>
                {
                    dbContext.UserToken.Remove(fbToken);
                });
                dbContext.SaveChanges();
                
                if(dbContext.UserToken.Where(x => x.UserId == user.Id && x.Token == value.Token).Count()  == 0)
                {
                    if(value.Token != null)
                    {
                        dbContext.UserToken.Add(userToken);
                        dbContext.SaveChanges();
                    }
                }

                if(value.Token != null)
                {
                    var token = new JwtSecurityToken(
                    issuer,
                    null,
                    permClaims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: credentials);
                    var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwt_token;
                } else
                {
                    var token = new JwtSecurityToken(
                    issuer,
                    null,
                    permClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);
                    var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwt_token;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
