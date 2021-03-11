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
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

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
        [HttpPost]
        public string UpdateProfilePicture()
        {
            string response = null;
            alphaReportEntities dbContext = new alphaReportEntities();
            var httpRequest = HttpContext.Current.Request;
            var Files = httpRequest.Files;
            Array array = Files.AllKeys;
            var UserId = Int32.Parse(httpRequest.Params["Id"]);
            if (Files.Count > 0)
            {
                foreach (string keys in array)
                {
                    var file = Files.Get(keys);
                    if (file != null && file.ContentLength > 0)
                    {
                        var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        var guid = Guid.NewGuid();
                        string filePath = HttpContext.Current.Server.MapPath("~/ProfileImages/" + guid + extension);
                        file.SaveAs(filePath);

                        var image = Image.FromFile(filePath);
                        var height = image.Height;
                        var width = image.Width;


                        //Max Height 250//
                        var scale = height / 160;
                        var desiredHeight = 160;
                        var desiredWidth = width / scale;

                        var newGuid = Guid.NewGuid();
                        var newPath = HttpContext.Current.Server.MapPath("~/ProfileImages/" + newGuid + extension);
                        var resizedImage = ResizeImage(image, desiredWidth, desiredHeight);
                        resizedImage.Save(newPath);

                        var originalImageUrl = dbContext.User.Where(x => x.Id == UserId).First().ImageUrl;
                        File.Delete(HttpContext.Current.Server.MapPath("~/" + originalImageUrl));

                        dbContext.User.Where(x => x.Id == UserId).FirstOrDefault().ImageUrl = "ProfileImages/" + newGuid + extension;
                        dbContext.SaveChanges();

                        response = "ProfileImages/" + newGuid + extension;
                    }
                }
            }

            return response;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
        [HttpGet]
        public UserPresentationModel GetUserProfile(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            User user = new User();
            user = dbContext.User.Where(x => x.Id == Id).FirstOrDefault();
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
