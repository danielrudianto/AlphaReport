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
using System.Web;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class RequestForInformationDocumentController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public int Post()
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            var httpRequest = HttpContext.Current.Request;
            var Files = httpRequest.Files;
            Array array = Files.AllKeys;
            var RequestForInformationId = Int32.Parse(httpRequest.Params["RequestForInformationId"]);
            if (Files.Count > 0)
            {
                foreach (string keys in array)
                {
                    var File = Files.Get(keys);
                    if (File != null && File.ContentLength > 0)
                    {
                        var ext = File.FileName.Substring(File.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        var guid = Guid.NewGuid();
                        string filePath = HttpContext.Current.Server.MapPath("~/RequestForInformationDocument/" + guid + extension);
                        File.SaveAs(filePath);

                        var document = new RequestForInformationDocument();
                        document.Name = File.FileName;
                        document.ImageUrl = "RequestForInformationDocument/" + guid + extension;
                        document.RequestForInformationId = RequestForInformationId;

                        dbContext.RequestForInformationDocument.Add(document);
                    }
                }

                return dbContext.SaveChanges();
            } else
            {
                return 0;
            }
        }
    }
}