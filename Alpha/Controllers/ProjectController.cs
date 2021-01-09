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
using System.Web;
using System.IO;
using HttpMultipartParser;
using System.Collections.Specialized;

namespace Alpha.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Api/Project")]
    [AllowAnonymous]
    public class ProjectController : ApiController
    {
        [HttpGet]
        [ActionName("GetIncompleted")]
        [Route("api/Project/GetIncompleted")]
        public List<CodeProjectPresentationModel> GetIncompleted(int type = 1)
        {
            //1 is for incompleted project.//
            alphaReportEntities dbContext = new alphaReportEntities();
            List<CodeProjectPresentationModel> response = new List<CodeProjectPresentationModel>();
            List<CodeProject> projects = new List<CodeProject>();
            if(type == 1)
            {
                projects = dbContext.CodeProject.Where(x => x.IsCompleted == 0).ToList();
            }
            projects.ForEach((project) => {
                response.Add(new CodeProjectPresentationModel(project));
            });

            return response;
        }

        [HttpGet]
        [ActionName("ConfirmProject")]
        [Route("api/Project/ConfirmProject")]
        public int ConfirmProject(int Id, int UserId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeProject codeProject = new CodeProject();
            codeProject = dbContext.CodeProject.Where(x => x.Id == Id).FirstOrDefault();
            if (codeProject != null)
            {
                codeProject.ConfirmedBy = UserId;
                codeProject.ConfirmedDate = DateTime.Now;
                return dbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        public CodeProjectPresentationModel Get(int id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeProjectPresentationModel response = new CodeProjectPresentationModel();
            CodeProject project = new CodeProject();
            project = dbContext.CodeProject.Where(x => x.Id == id).First();
            response = new CodeProjectPresentationModel(project);
            return response;

        }
        

        [Route("api/Project/InsertProject")]
        [HttpPost]
        public int InsertProject(CodeProjectFormModel value)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeProject codeProject = new CodeProject();
            codeProject = CodeProjectFormModel.MapDbObject(value);
            dbContext.CodeProject.Add(codeProject);
            int result = dbContext.SaveChanges();
            if(result == 1)
            {
                var ProjectId = codeProject.Id;
                List<ProjectFormModel> projects = new List<ProjectFormModel>();
                projects = value.Tasks;
                projects.ForEach((ProjectFormModel task) =>
                {
                    task.CodeProjectId = ProjectId;
                    Project project = new Project();
                    project = ProjectFormModel.MapDbObject(task);
                    dbContext.Project.Add(project);
                    dbContext.SaveChanges();

                    projects.Where(x => x.ParentId == task.Id).ToList().ForEach((a) =>
                    {
                        a.ParentId = project.Id;
                    });               
                });

                var UserProject = value.Users;
                UserProject.ForEach(User =>
                {
                    CodeProjectUser codeProjectUser = new CodeProjectUser();
                    codeProjectUser.UserId = User.UserId;
                    codeProjectUser.CodeProjectId = ProjectId;
                    codeProjectUser.Position = User.Position;

                    dbContext.CodeProjectUser.Add(codeProjectUser);
                    dbContext.SaveChanges();
                });

                return ProjectId;
            } else
            {
                return 0;
            }

            
        }
    }
}
