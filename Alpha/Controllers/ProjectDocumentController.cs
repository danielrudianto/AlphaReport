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
using System.IO;
using HttpMultipartParser;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using System.Web.Hosting;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class ProjectDocumentController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public void Post()
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            var httpRequest = HttpContext.Current.Request;
            var Files = httpRequest.Files;
            Array array = Files.AllKeys;
            int CreatedBy = Int32.Parse(httpRequest.Params["CreatedBy"]);
            int CodeProjectId = Int32.Parse(httpRequest.Params["CodeProjectId"]);

            foreach (string keys in array)
            {
                var File = Files.Get(keys);
                if (File != null && File.ContentLength > 0)
                {
                    var ext = File.FileName.Substring(File.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    var guid = Guid.NewGuid();
                    string filePath = HttpContext.Current.Server.MapPath("~/ProjectDocument/" + guid + extension);
                    File.SaveAs(filePath);

                    var document = new CodeProjectDocument();
                    document.Name = File.FileName;
                    document.Url = "ProjectDocument/" + guid + extension;
                    document.CreatedBy = CreatedBy;
                    document.CreatedDate = DateTime.Now;
                    document.CodeProjectId = CodeProjectId;
                    dbContext.CodeProjectDocument.Add(document);
                }
                
                dbContext.SaveChanges();
            }
        }
    }
}
