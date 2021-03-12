using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class RequestForInformationPresentationModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string AddressedFor { get;set;}
        public System.DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }
        public int Type { get; set; }
        public int CodeProjectId { get; set; }
        public List<RequestForInformationAnswerPresentationModel> Answers { get; set; }
        public List<RequestForInformationDocumentPresentationModel> Documents { get; set; }

        public RequestForInformationPresentationModel(RequestForInformation value)
        {
            List<RequestForInformationAnswerPresentationModel> answerPresentationModels = new List<RequestForInformationAnswerPresentationModel>();
            List<RequestForInformationAnswer> answerModels = new List<RequestForInformationAnswer>();
            answerModels = value.RequestForInformationAnswer.Where(x => x.IsDelete == 0).ToList();
            answerModels.ForEach(x =>
            {
                answerPresentationModels.Add(new RequestForInformationAnswerPresentationModel(x));
            });

            List<RequestForInformationDocumentPresentationModel> documentPresentationModels = new List<RequestForInformationDocumentPresentationModel>();
            List<RequestForInformationDocument> documents = new List<RequestForInformationDocument>();

            documents = value.RequestForInformationDocument.ToList();
            documents.ForEach(document =>
            {
                documentPresentationModels.Add(new RequestForInformationDocumentPresentationModel(document));
            });

            Id = value.Id;
            CreatedBy = value.User.FirstName + " " + value.User.LastName;
            CreatedDate = value.CreatedDate;
            Description = value.Description;
            Header = value.Header;
            AddressedFor = value.AddressedFor;
            Answers = answerPresentationModels;
            Documents = documentPresentationModels;
            CodeProjectId = value.CodeProjectId;
            Type = 5;
        }
    }
}