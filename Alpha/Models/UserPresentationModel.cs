using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class UserPresentationModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte IsActive { get; set; }
        public string ImageUrl { get; set; }
        public Boolean HasRelation { get; set; }
        public UserPositionPresentationModel LastPosition { get; set; }
        public List<UserPositionPresentationModel> Positions { get; set; }

        public UserPresentationModel(User users)
        {
            List<UserPosition> userPositions = new List<UserPosition>();
            userPositions = users.UserPosition.OrderByDescending(x => x.EffectiveDate).ToList();
            List<UserPositionPresentationModel> userPresentationPositions = new List<UserPositionPresentationModel>();
            foreach(UserPosition userPosition in userPositions)
            {
                userPresentationPositions.Add(new UserPositionPresentationModel(userPosition));
            }

            Id = users.Id;
            FirstName = users.FirstName;
            LastName = users.LastName;
            Email = users.Email;
            IsActive = users.IsActive;
            ImageUrl = users.ImageUrl;
            HasRelation = users.RequestForInformation.Any() || users.RequestForInformationAnswer.Any() || users.Client.Any() || users.ClientContact.Any() || users.CodeProject.Any() || users.CodeProject1.Any() || users.CodeReport.Any() || users.CodeReport1.Any();
            LastPosition = new UserPositionPresentationModel(users.UserPosition.Where(x => x.EffectiveDate <= DateTime.Now).OrderByDescending(x => x.EffectiveDate).First());
            Positions = userPresentationPositions;
        }
    }
}