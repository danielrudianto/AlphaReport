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
        public byte IsCompleted { get; set; }
        public string CompletedBy { get; set; }
        public Nullable<DateTime> CompletedDate { get; set; }
        public double Progress { get; set; }
        public int Type { get; set; }

        public CodeProjectPresentationModel()
        {
        }

        public CodeProjectPresentationModel(CodeProject dbObject)
        {
            Double totalTask = 0;
            Double completedTask = 0;
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
                if(task.Quantity.HasValue && task.Quantity > 0)
                {
                    totalTask += ((task.Quantity.HasValue) ? task.Quantity.Value : 0) * ((task.Price.HasValue) ? task.Price.Value : 0);
                    completedTask += ((task.Done.HasValue) ? task.Done.Value : 0) * ((task.Price.HasValue) ? task.Price.Value : 0);
                }
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
            IsCompleted = dbObject.IsCompleted;
            CompletedBy = (dbObject.CompletedBy != null) ? (dbObject.User1.FirstName + " " + dbObject.User1.LastName) : null;
            CompletedDate = dbObject.CompletedDate;
            Users = userPresentations;
            Type = dbObject.Type;

            if (totalTask > 0)
            {
                Progress = completedTask / totalTask;
            } else
            {
                Progress = 0;
            }
            
        }
    }
}