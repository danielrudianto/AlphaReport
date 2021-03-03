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
    [RoutePrefix("")]
    public class ProjectController : ApiController
    {
        [Authorize]
        [AllowAnonymous]
        [Route("Api/Project/GetIncompleted/{type}")]
        [HttpGet]        
        public List<CodeProjectPresentationModel> GetIncompleted(int type = 1)
        {
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

        [Route("api/Project/GetByUser/{userProjectId}")]
        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public List<CodeProjectPresentationModel> GetByUser(int UserProjectId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<CodeProjectPresentationModel> response = new List<CodeProjectPresentationModel>();
            List<CodeProject> projects = new List<CodeProject>();
            projects = dbContext.CodeProject.Where(x => x.IsCompleted == 0 && x.ConfirmedBy != null).ToList();
            projects.ForEach((project) => {
            if (project.CodeProjectUser.Where(z => z.UserId == dbContext.User.Where(x => x.Id == UserProjectId).Select(y => y.Id).First()).Count() > 0)
                {
                    response.Add(new CodeProjectPresentationModel(project));
                }
            });
            return response;
        }

        [Authorize]
        [Route("Api/Project/ConfirmProject")]
        [HttpPost]
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

        [Authorize]
        [AllowAnonymous]
        [Route("Api/Project/GetById/{id}")]
        [HttpGet]
        public CodeProjectPresentationModel GetById(int id)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            CodeProjectPresentationModel response = new CodeProjectPresentationModel();
            CodeProject project = new CodeProject();
            project = dbContext.CodeProject.Where(x => x.Id == id).First();
            response = new CodeProjectPresentationModel(project);
            return response;
        }

        [Authorize]
        [AllowAnonymous]
        [Route("api/Project/GetByEmail")]
        [HttpGet]
        public List<CodeProjectPresentationModel> GetByEmail(string email)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<CodeProjectPresentationModel> response = new List<CodeProjectPresentationModel>();
            List<CodeProject> projects = new List<CodeProject>();
            projects = dbContext.CodeProject.Where(x => x.User.Email == email).ToList();
            projects.ForEach(project =>
            {
                response.Add(new CodeProjectPresentationModel(project));
            });

            return response;
        }

        [Authorize]
        [AllowAnonymous]
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

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        [Route("Api/Project/getTodayReports/{projectId}")]
        public List<object> getTodayReports(int projectId)
        {
            alphaReportEntities dbContext = new alphaReportEntities();
            List<CodeReport> reports = new List<CodeReport>();

            List<object> response = new List<object>();
            reports = dbContext.CodeReport.Where(x => x.CodeProjectId == projectId && x.Date.Month == DateTime.Today.Month && x.Date.Year == DateTime.Today.Year && x.Date.Day == DateTime.Now.Day && x.IsDelete == 0).OrderByDescending(y => y.CreatedDate).ToList();
            reports.ForEach(report =>
            {
                object O = new object();
                switch(report.Type)
                {
                    case 1:
                        O = WorkerReportPresentationModel.ParseReport(report);
                        break;
                    case 2:
                        O = ToolReportPresentationModel.ParseReport(report);
                        break;
                    case 3:
                        O = MaterialReportPresentationModel.ParseReport(report);
                        break;
                    case 4:
                        O = WeatherReportPresentationModel.ParseReport(report);
                        break;
                    case 5:
                        O = new RequestForInformationPresentationModel(dbContext.RequestForInformation.Where(x => x.Id == report.Id).First());
                        break;
                    case 7:
                        O = StatusReportPresentationModel.ParseReport(report);
                        break;

                }
                
                response.Add(O);
            });

            return response;
        }
    }
}
