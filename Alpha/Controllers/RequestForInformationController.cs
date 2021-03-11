using Newtonsoft.Json;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class RequestForInformationController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public List<RequestForInformationPresentationModel> Get(int projectId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<RequestForInformation> requestForInformation = new List<RequestForInformation>();

            List<RequestForInformationPresentationModel> response = new List<RequestForInformationPresentationModel>();
            requestForInformation = dbContext.RequestForInformation.Where(x => x.CodeProjectId == projectId).OrderBy(x => x.CreatedDate).ToList();
            requestForInformation.ForEach(item =>
            {
                response.Add(new RequestForInformationPresentationModel(item));
            });

            return response;

        }

        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        public int Post(RequestForInformationFormModel value)
        {
            if (value == null)
            {
                return 0;
            } else
            {
                alphaReportEntities dbContext = new alphaReportEntities();
                CodeProject codeProject = dbContext.CodeProject.Where(x => x.Id == value.CodeProjectId).First();
                if(codeProject == null || codeProject.IsCompleted == 1 || codeProject.IsDelete == 1)
                {
                    throw new InvalidOperationException();
                } else
                {
                    RequestForInformation rfi = new RequestForInformation();
                    rfi = RequestForInformationFormModel.MapDbObject(value);
                    dbContext.RequestForInformation.Add(rfi);
                    int result = dbContext.SaveChanges();
                    if (result == 1)
                    {
                        var response = dbContext.RequestForInformation.Where(x => x.Id == rfi.Id).First();
                        response.User = dbContext.User.Where(x => x.Id == rfi.CreatedBy).First();
                        var RFIResponse = new RequestForInformationPresentationModel(response);
                        NotificationHub.NewFeed(RFIResponse);

                        return rfi.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public RequestForInformationPresentationModel GetById(int Id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            RequestForInformation requestForInformation = new RequestForInformation();
            requestForInformation = dbContext.RequestForInformation.Where(x => x.Id == Id).First();

            RequestForInformationPresentationModel response = new RequestForInformationPresentationModel(requestForInformation);
            return response;
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public RequestForInformationAnswerPresentationModel PostAnswer(RequestForInformationAnswerFormModel answer)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            RequestForInformationAnswer requestForInformationAnswer = new RequestForInformationAnswer();
            requestForInformationAnswer = RequestForInformationAnswerFormModel.MapDbObject(answer);
            dbContext.RequestForInformationAnswer.Add(requestForInformationAnswer);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                var response = dbContext.RequestForInformationAnswer.Where(x => x.Id == requestForInformationAnswer.Id).First();
                response.User = dbContext.User.Where(x => x.Id == requestForInformationAnswer.CreatedBy).First();
                var RFIResponse = new RequestForInformationAnswerPresentationModel(response);

                NotificationHub.NewRequestForInformationAnswer(RFIResponse);
                return RFIResponse;

            } else
            {
                return null;
            }
        }
    }
}