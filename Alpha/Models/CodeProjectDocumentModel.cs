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
    public class CodeProjectDocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int CodeProjectId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public CodeProjectDocumentModel()
        {
            CreatedDate = DateTime.Now;
        }

        public CodeProjectDocumentModel(CodeProjectDocument value)
        {
            Id = value.Id;
            Name = value.Name;
            Url = value.Url;
            CodeProjectId = value.CodeProjectId;
            CreatedBy = value.CreatedBy;
            CreatedDate = value.CreatedDate;
        }

        public CodeProjectDocument MapDbObject(CodeProjectDocumentModel value)
        {
            CodeProjectDocument document = new CodeProjectDocument();
            document.Id = value.Id;
            document.Name = value.Name;
            document.Url = value.Url;
            document.CodeProjectId = value.CodeProjectId;
            document.CreatedBy = value.CreatedBy;
            document.CreatedDate = value.CreatedDate;

            return document;
        }
    }
}