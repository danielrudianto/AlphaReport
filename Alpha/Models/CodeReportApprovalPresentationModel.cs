using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class CodeReportApprovalPresentationModel
    {
        public int Id { get; set; }
        public int CodeReportId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public byte Approval { get; set; }
        public int UserId { get; set; }

        public CodeReportApprovalPresentationModel(CodeReportApproval value)
        {
            Id = value.Id;
            CodeReportId = value.CodeReportId;
            CreatedBy = value.User.FirstName + " " + value.User.LastName;
            CreatedDate = value.CreatedDate;
            Comment = value.Comment;
            Approval = value.Approval;
            UserId = value.User.Id;
        }
    }
}