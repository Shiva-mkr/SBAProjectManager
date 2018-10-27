using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.BusinessLayer;
using TaskManger.DTOContext;

namespace TaskManager.Service.Controllers
{
    public class UserController : ApiController
    {
        private TaskmanagerBL taskBL = new TaskmanagerBL();
        [HttpGet]
        public List<UserManagerContext> GetUserList(string sortColumn)
        {
            try
            {
                var data = taskBL.GetUserList(sortColumn);
                return data;
            }
            catch (Exception ex)
            {
                List<UserManagerContext> data = new List<UserManagerContext>();
                return data;
            }
        }
        [HttpPost]
        public string AddOrUpdateUsererList(UserManagerContext userData)
        {
            try
            {
                var data = taskBL.AddOrUpdateUsererList(userData);
                return data;
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        [HttpGet]
        public string DeleteUser(string ID)
        {
            try
            {
                var data = taskBL.DeleteUser(ID);
                return data.ToString();
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }

        [HttpGet]
        public string DeleteProject(string ID)
        {
            try
            {
                var data = taskBL.DeleteProject(ID);
                return data.ToString();
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }

        [HttpPost]
        public string AddOrUpdateProject(ProjectrManagerContext projData)
        {
            try
            {
                var data = taskBL.AddOrUpdateProject(projData);
                return data;
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }

        [HttpGet]
        public List<ProjectrManagerContext> GetProjectList(string sortColumn)
        {
            try
            {
                var data = taskBL.GetProjectList(sortColumn);
                return data;
            }
            catch (Exception ex)
            {
                List<ProjectrManagerContext> data = new List<ProjectrManagerContext>();
                return data;
            }
        }
    }
}
