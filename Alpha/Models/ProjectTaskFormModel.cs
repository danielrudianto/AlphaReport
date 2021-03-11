using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class ProjectTaskFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int? ParentId { get; set; }
        public int CreatedBy { get; set; }

        public static ProjectTask MapDbObject(ProjectTaskFormModel value)
        {
            ProjectTask projectTask = new ProjectTask();
            projectTask.Id = value.Id;
            projectTask.Name = value.Name;
            projectTask.Description = value.Description;
            if (value.Id == 0)
            {
                projectTask.Type = value.Type;
                projectTask.CreatedBy = value.CreatedBy;
                projectTask.CreatedDate = DateTime.Now;
            }
            
            if (value.ParentId.HasValue)
                projectTask.ParentId = value.ParentId.Value;

            return projectTask;
        }
    }
}