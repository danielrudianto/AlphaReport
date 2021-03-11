using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class ProjectTaskController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [Authorize]
        public List<ProjectTaskPresentationModel> Get()
        {
            List<ProjectTaskPresentationModel> response = new List<ProjectTaskPresentationModel>();

            List<ProjectTask> projectTasks = new List<ProjectTask>();
            alphaReportEntities dbContext = new alphaReportEntities();
            projectTasks = dbContext.ProjectTask.ToList();
            projectTasks.ForEach(x =>
            {
                if(!x.ParentId.HasValue)
                    response.Add(new ProjectTaskPresentationModel(x));
            });

            return response;
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize]
        public List<ProjectTaskPresentationModel> GetByType(int type)
        {
            List<ProjectTaskPresentationModel> response = new List<ProjectTaskPresentationModel>();

            List<ProjectTask> projectTasks = new List<ProjectTask>();
            alphaReportEntities dbContext = new alphaReportEntities();
            projectTasks = dbContext.ProjectTask.Where(x => x.Type == type).ToList();
            projectTasks.ForEach(x =>
            {
                if(!x.ParentId.HasValue)
                    response.Add(new ProjectTaskPresentationModel(x));
            });

            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize]
        public ProjectTaskPresentationModel Post(ProjectTaskFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ProjectTask projectTask = ProjectTaskFormModel.MapDbObject(value);
            dbContext.ProjectTask.Add(projectTask);
            int result = dbContext.SaveChanges();
            if (result == 1)
            {
                projectTask.User = dbContext.User.Where(x => x.Id == value.CreatedBy).First();
                ProjectTaskPresentationModel response = new ProjectTaskPresentationModel(projectTask);
                return response;
            }
            else
            {
                return null;
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Authorize]
        public int Put(ProjectTaskFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ProjectTask projectTask = dbContext.ProjectTask.Where(x => x.Id == value.Id).First();
            projectTask.Name = value.Name;
            projectTask.Description = value.Description;
            return dbContext.SaveChanges();
        }

        [HttpDelete]
        [AllowAnonymous]
        [Authorize]
        public int Delete(int id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            ProjectTask projectTask = new ProjectTask();
            projectTask = dbContext.ProjectTask.Where(x => x.Id == id).FirstOrDefault();
            if(projectTask != null)
            {
                if(projectTask.ProjectTask1.Count == 0)
                {
                    dbContext.ProjectTask.Remove(projectTask);
                    return dbContext.SaveChanges();
                } else
                {
                    return 0;
                }
            } else
            {
                return 0;
            }
        }
    }
}