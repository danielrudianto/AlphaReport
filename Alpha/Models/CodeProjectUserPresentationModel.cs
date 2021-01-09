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
    public class CodeProjectUserPresentationModel
    {
        public int Id { get; set; }
        public UserPresentationModel User { get; set; }
        public int Position { get; set; }

        public CodeProjectUserPresentationModel(CodeProjectUser value)
        {
            Id = value.Id;
            User = new UserPresentationModel(value.User);
            Position = value.Position;
        }
    }
}