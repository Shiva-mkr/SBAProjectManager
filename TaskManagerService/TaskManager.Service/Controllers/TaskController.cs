using Newtonsoft.Json;
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
    public class TaskController : ApiController
    {
        private TaskmanagerBL taskBL = new TaskmanagerBL();

        #region  GetTaskList
        /// <summary>
        /// Get all task list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetTaskList(string sortBy)
        {
            try
            {
                var response = Request.CreateResponse<List<TaskMangerContext>>(HttpStatusCode.OK, taskBL.GetTaskList(sortBy));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
        #endregion


        #region AddTask
        /// <summary>
        /// Add a new task to the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CreateTask([FromBody]object data)
        {
            try
            {
                TaskMangerContext taskData = new TaskMangerContext();
                ParentTaskMangerContext parentTaskData = new ParentTaskMangerContext();

                taskData = JsonConvert.DeserializeObject<TaskMangerContext>(data.ToString());
                taskData.StartDate = Convert.ToDateTime(taskData.StartDate);
                taskData.EndDate = Convert.ToDateTime(taskData.EndDate);
                taskData.StartDate = (taskData.StartDate.Value.Year > 2000) ? taskData.StartDate : null;
                taskData.EndDate = (taskData.EndDate.Value.Year > 2000) ? taskData.EndDate : null;
                
                taskData.Parent_ID = taskData.Parent_ID;
                parentTaskData.Parent_ID = taskData.Task_ID;
                parentTaskData.Parent_Task = taskData.Task;

                bool addTask = taskBL.AddTask(taskData, parentTaskData);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, addTask);
                return response;


            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.ExpectationFailed, false);
            }
        }
        #endregion

        #region UpdateTask
        /// <summary>
        /// Update an existing task item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateTask([FromBody]object data)
        {
            try
            {
                TaskMangerContext taskData = new TaskMangerContext();
                ParentTaskMangerContext parentTaskData = new ParentTaskMangerContext();
                taskData = JsonConvert.DeserializeObject<TaskMangerContext>(data.ToString());
                
                taskData.StartDate = Convert.ToDateTime(taskData.StartDate);
                taskData.EndDate = Convert.ToDateTime(taskData.EndDate);
                taskData.StartDate = (taskData.StartDate.Value.Year > 2000) ? taskData.StartDate : null;
                taskData.EndDate = (taskData.EndDate.Value.Year > 2000) ? taskData.EndDate : null;
                parentTaskData.Parent_ID = taskData.Parent_ID;
                parentTaskData.Parent_Task = taskData.Task;
                bool updateTask = taskBL.UpdateTask(taskData, parentTaskData);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, updateTask);
                return response;

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.ExpectationFailed, false);
            }
        }
        #endregion

        #region EndTask
        /// <summary>
        /// End the task item selected by user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage EndTask([FromUri]int Id)
        {
            try
            {
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, taskBL.EndTask(Id));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
        }
        #endregion

        #region GetTaskById
        /// <summary>
        /// Get Task Data Using Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetTaskById([FromUri]int Id)
        {
            try
            {
                var response = Request.CreateResponse<TaskMangerContext>(HttpStatusCode.OK, taskBL.GetTaskById(Id));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, false);
            }
        }
        #endregion

        #region ParentTask
        /// <summary>
        /// Get Parent Task List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ParentTask()
        {
            try
            {
                var response = Request.CreateResponse<List<ParentTaskMangerContext>>(HttpStatusCode.OK, taskBL.ParentTask());
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
        }
        #endregion
    }
}