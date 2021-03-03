using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Alpha
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration { };
                map.RunSignalR(hubConfiguration);
            });

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:44331", 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5ubP+s7r8?WEFaXEEPkn_uf7Lu+peb946uz+WzcKhfUKPA2WdVzpB9Bkf6aEeDktwH7mMJqTPmVBQ+67Z*WQTVJb?^9vk?DQ*qm9s2YEcXA-ndKjsnth#^D$Y^ujVatR"))
                    }
                }
            );
        }
    }
}
