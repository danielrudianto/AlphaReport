using Alpha.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace Alpha
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NotificationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        public override Task OnConnected()
        {
            hubContext.Clients.All.hello("connected");
            Clients.Caller.hello("connectedWithoutHub");
            return base.OnConnected();
        }

        public static void Hello(String name)
        {
            hubContext.Clients.All.hello(name);
        }

        public static void UpdateApprovals(CodeReportApproval value)
        {
            hubContext.Clients.All.newApproval(new CodeReportApprovalPresentationModel(value));
        }

        public static void NewRequestForInformationAnswer(RequestForInformationAnswerPresentationModel value)
        {
            hubContext.Clients.All.newRequestForInformationAnswer(value);
        }

        public static void ProjectCompleted(int Id)
        {
            hubContext.Clients.All.ProjectCompleted(Id);
        }

        public static void NewFeed(object feed)
        {
            hubContext.Clients.All.newFeed(feed);
        }

        public static void DeleteFeed(int Id)
        {
            hubContext.Clients.All.deleteFeed(Id);
        }
    }
}