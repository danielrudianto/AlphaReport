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
    public class CodeProjectUserFormModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CodeProjectId { get; set; }
        public int Position { get; set; }

        public CodeProjectUser MapDbObject(CodeProjectUser value)
        {
            CodeProjectUser user = new CodeProjectUser();
            user.Id = value.Id;
            user.UserId = value.UserId;
            user.CodeProjectId = value.CodeProjectId;
            user.Position = value.Position;

            return user;
        }
    }
}