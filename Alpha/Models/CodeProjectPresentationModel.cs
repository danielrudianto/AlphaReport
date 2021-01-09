using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Alpha.Models
{
    public class CodeProjectPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public ClientPresentationModel Client { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string ConfirmedBy { get; set; }
        public string DocumentName { get; set; }
        public string Address { get; set; }
        public List<CodeProjectDocumentModel> Documents { get; set; }
        public List<ProjectFormModel> Tasks { get; set; }
        public List<CodeProjectUserPresentationModel> Users { get; set; }

        public CodeProjectPresentationModel()
        {
        }

        public CodeProjectPresentationModel(CodeProject dbObject)
        {
            var documents = dbObject.CodeProjectDocument.ToList();
            List<CodeProjectDocumentModel> codeProjectDocuments = new List<CodeProjectDocumentModel>();
            documents.ForEach(document =>
            {
                codeProjectDocuments.Add(new CodeProjectDocumentModel(document));
            });

            var tasks = dbObject.Project.OrderBy(x => x.ParentId).OrderBy(x => x.Timeline).ToList();
            List<ProjectFormModel> projects = new List<ProjectFormModel>();
            tasks.ForEach(task =>
            {
                projects.Add(new ProjectFormModel(task));
            });

            var users = dbObject.CodeProjectUser.ToList();
            List<CodeProjectUserPresentationModel> userPresentations = new List<CodeProjectUserPresentationModel>();
            users.ForEach(user =>
            {
                userPresentations.Add(new CodeProjectUserPresentationModel(user));
            });

            Id = dbObject.Id;
            Name = dbObject.Name;
            CreatedDate = dbObject.CreatedDate;
            Client = new ClientPresentationModel(dbObject.Client);
            CreatedBy = dbObject.User.FirstName + " " + dbObject.User.LastName;
            ConfirmedBy = (dbObject.User1 != null) ? (dbObject.User1.FirstName + " " + dbObject.User1.LastName) : "";
            ConfirmedDate = dbObject.ConfirmedDate;
            Address = dbObject.Address;
            DocumentName = dbObject.DocumentName;
            Documents = codeProjectDocuments;
            Tasks = projects;
            Users = userPresentations;
        }
    }
}