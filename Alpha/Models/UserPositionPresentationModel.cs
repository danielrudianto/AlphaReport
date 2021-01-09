using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class UserPositionPresentationModel
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public Nullable<int> PositionId;
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<int> UserId { get; set; }

        public UserPositionPresentationModel(UserPosition value)
        {
            Id = value.Id;
            switch(value.Position){
                case 1:
                    {
                        Position = "Site Engineer";
                        break;
                    }
                case 2:{
                        Position = "Site Supervisor";
                        break;
                    }
                case 3:
                    {
                        Position = "Project Manager";
                        break;
                    }
                case 4:
                    {
                        Position = "Back Office Staff";
                        break;
                    }
                case 5:
                    {
                        Position = "Administrator";
                        break;
                    }
                case 6:
                    {
                        Position = "SuperAdministrator";
                        break;
                    }
            }
            PositionId = value.Position;
            EffectiveDate = value.EffectiveDate;
            UserId = value.UserId;
        }
    }
}