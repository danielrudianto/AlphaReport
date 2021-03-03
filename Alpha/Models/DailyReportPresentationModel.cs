using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class DailyReportPresentationModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CodeProjectId { get; set; }
        public List<DailyTaskFormModel> Tasks { get; set; }

        public static DailyReportPresentationModel ParseReport(CodeReport report)
        {
            List<DailyTaskFormModel> Tasks = new List<DailyTaskFormModel>();
            report.DailyTask.Where(x => x.ParentId == null).ToList().ForEach(x =>
            {
                var parentTask = new DailyTaskFormModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Children = new List<DailyChildrenTaskFormModel>()
                };

                report.DailyTask.Where(y => y.ParentId == x.Id).ToList().ForEach(z =>
                {
                    parentTask.Children.Add(new DailyChildrenTaskFormModel
                    {
                        Id = z.Id,
                        Name = z.Name,
                        Description = z.Description,
                        Quantity = z.Quantity.Value,
                        Unit = z.Unit,
                    });
                });

                Tasks.Add(parentTask);
            });
            DailyReportPresentationModel response = new DailyReportPresentationModel
            {
                Id = report.Id,
                Type = report.Type,
                CreatedBy = (report.User == null) ? "" : (report.User.FirstName + " " + report.User.LastName),
                CreatedDate = report.CreatedDate,
                CodeProjectId = report.CodeProjectId,
                Tasks = Tasks
            };

            return response;
        }
    }
}