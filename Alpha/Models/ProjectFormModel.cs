using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;

namespace Alpha.Models
{
    public class ProjectFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? BudgetPrice { get; set; }
        public double? Quantity { get; set; }
        public double Done { get; set; }
        public byte IsDelete { get; set; }
        public int CodeProjectId { get; set; }
        public int OrderNumber { get; set; }
        public Nullable<int> ParentId { get; set; }
        public double? EstimatedDuration { get; set; }
        public int? Timeline { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public List<ProjectFormModel> Children { get; set; }
        public string Unit { get; set; }

        public ProjectFormModel()
        {

        }
        public ProjectFormModel(Project value)
        {
            Id = value.Id;
            Name = value.Name;
            BudgetPrice = value.BudgetPrice;
            Quantity = value.Quantity;
            IsDelete = value.IsDelete;
            CodeProjectId = value.CodeProjectId;
            ParentId = value.ParentId;
            EstimatedDuration = value.EstimatedDuration;
            Timeline = value.Timeline;
            Price = value.Price;
            Description = value.Description;
            Children = new List<ProjectFormModel>();
            Unit = value.Unit;
        }

        public static Project MapDbObject(ProjectFormModel value)
        {
            Project project = new Project();

            project.Id = value.Id;
            project.Name = value.Name;
            project.BudgetPrice = value.BudgetPrice;
            project.Quantity = value.Quantity;
            project.IsDelete = value.IsDelete;
            project.CodeProjectId = value.CodeProjectId;
            project.ParentId = value.ParentId;
            project.EstimatedDuration = value.EstimatedDuration;
            project.Timeline = value.Timeline;
            project.Price = value.Price;
            project.Description = value.Description;
            project.Unit = value.Unit;

            return project;

        }
    }
}