using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Alpha.Models
{
    public class CodeProjectFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ClientId { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public string DocumentName { get; set; }
        public byte IsCompleted { get; set; }
        public List<HttpPostedFile> Documents { get; set; }
        public List<ProjectFormModel> Tasks { get; set; }
        public List<CodeProjectUserFormModel> Users { get; set; }

        public CodeProjectFormModel()
        {
        }

        public static CodeProject MapDbObject(CodeProjectFormModel project)
        {
            CodeProject codeProject = new CodeProject();

            codeProject.Id = project.Id;
            codeProject.Name = project.Name;
            codeProject.Address = project.Address;
            codeProject.CreatedDate = DateTime.Now;
            codeProject.ClientId = project.ClientId;
            codeProject.CreatedBy = project.CreatedBy;
            codeProject.ConfirmedDate = project.ConfirmedDate;
            codeProject.ConfirmedBy = project.ConfirmedBy;
            codeProject.DocumentName = project.DocumentName;
            codeProject.IsCompleted = project.IsCompleted;

            return codeProject;

        }

        public static CodeProject MapMultipartDbObject(String stream)
        {
            CodeProject codeProject = new CodeProject();
            var project = (CodeProjectFormModel)JsonConvert.DeserializeObject(stream);

            codeProject.Id = project.Id;
            codeProject.Name = project.Name;
            codeProject.Address = project.Address;
            codeProject.CreatedDate = DateTime.Now;
            codeProject.ClientId = project.ClientId;
            codeProject.CreatedBy = project.CreatedBy;
            codeProject.ConfirmedDate = project.ConfirmedDate;
            codeProject.ConfirmedBy = project.ConfirmedBy;
            codeProject.DocumentName = project.DocumentName;
            codeProject.IsCompleted = project.IsCompleted;

            return codeProject;
        }
    }
}