using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class CodeReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }

        public byte IsDelete { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }

        public CodeReportPresentationModel(CodeReport value)
        {
            Id = value.Id;
            Type = value.Type;
            CreatedBy = value.User.FirstName + " " + value.User.LastName;
            CreatedDate = value.CreatedDate;
            CodeProjectId = value.CodeProjectId;
            IsDelete = value.IsDelete;
        }
    }
}