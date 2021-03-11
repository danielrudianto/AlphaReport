using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class ProjectTaskPresentationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public List<ProjectTaskPresentationModel> Children { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public ProjectTaskPresentationModel(ProjectTask value)
        {
            Id = value.Id;
            Name = value.Name;
            Description = value.Description;
            Type = value.Type;
            CreatedBy = value.User.FirstName + " " + value.User.LastName;
            CreatedDate = value.CreatedDate;

            if (value.ProjectTask1.Any())
            {
                List<ProjectTaskPresentationModel> ChildrenPresentationTask = new List<ProjectTaskPresentationModel>();

                List<ProjectTask> ChildrenTask = new List<ProjectTask>();
                ChildrenTask = value.ProjectTask1.ToList();
                ChildrenTask.ForEach(x =>
                {
                    ChildrenPresentationTask.Add(new ProjectTaskPresentationModel(x));
                });

                Children = ChildrenPresentationTask;
            } else
            {
                Children = new List<ProjectTaskPresentationModel>();
            }
            
        }
    }
}