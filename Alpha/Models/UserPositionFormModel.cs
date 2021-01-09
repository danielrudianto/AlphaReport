using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class UserPositionFormModel
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public int UserId { get; set; }

        public static UserPosition MapDbObject(UserPositionFormModel value)
        {
            UserPosition userPosition = new UserPosition();
            userPosition.Id = value.Id;
            userPosition.Position = value.Position;
            userPosition.EffectiveDate = value.EffectiveDate;
            userPosition.UserId = value.UserId;
            
            if(value.Id == 0)
            {
                userPosition.CreatedDate = DateTime.Now;
                userPosition.CreatedBy = 1;
            }
            return userPosition;
        }
    }
}