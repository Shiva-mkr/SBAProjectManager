using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataLayer;

using TaskManger.DTOContext;

namespace TaskManager.BusinessLayer
{
    public class TaskmanagerBL
    {
        private DataAccess dataAccess = new DataAccess();

        #region
        /// <summary>
        /// GetTaskList
        /// </summary>
        /// <returns></returns>
        public List<TaskMangerContext> GetTaskList(string sortBy)
        {
              var taskList = dataAccess.GetTaskList();

            List<TaskMangerContext> taskListData = new List<TaskMangerContext>();
            List<TaskMangerContext> taskListsortData = new List<TaskMangerContext>();
            foreach (var item in taskList)
            {
                TaskMangerContext tmContext = new TaskMangerContext();
                tmContext.Task_ID = item.Task_ID;
                tmContext.Parent_ID = item.Parent_ID;
                tmContext.Task = item.Task;
                tmContext.StartDate = item.Start_Date;
                tmContext.EndDate = item.End_Date.Value;
                tmContext.Priority = item.Priority;
                tmContext.IsTaskEnded = item.isTaskEnded == null ? 0 : item.isTaskEnded;
                tmContext.ParentTask = item.ParentTask;
                tmContext.Project_ID = Convert.ToString(item.Project_ID);
                tmContext.Project_Name = Convert.ToString(item.ProjectName);
                taskListData.Add(tmContext);

                if (sortBy.ToLower() == "startdate")
                {
                    taskListsortData = taskListData.OrderBy(i => i.StartDate).ToList();
                }
                else if (sortBy.ToLower() == "enddate")
                {
                    taskListsortData = taskListData.OrderBy(i => i.EndDate).ToList();
                }
                else if (sortBy.ToLower() == "priority")
                {
                    taskListsortData = taskListData.OrderByDescending(i => i.Priority).ToList();
                }
                else if(sortBy.ToLower() == "completed")
                {
                    taskListsortData = taskListData.OrderByDescending(i => i.IsTaskEnded).ToList();
                }
            }
            return taskListsortData;
        }
        #endregion

        #region
        /// <summary>
        /// AddTask
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parentTask"></param>
        /// <returns></returns>
        public bool AddTask(TaskMangerContext task, ParentTaskMangerContext parentTask)
        {
            bool IsTaskAdded;

            Task_Master tmContext = new Task_Master();
            tmContext.Task_ID = task.Task_ID;
            tmContext.Parent_ID = Convert.ToInt32(task.ParentTask);
            tmContext.Task = task.Task;
            tmContext.Start_Date = task.StartDate;
            tmContext.End_Date = task.EndDate;
            tmContext.Priority = task.Priority;
            tmContext.isTaskEnded = task.IsTaskEnded ;
            tmContext.ParentTask = task.ParentTask;
            tmContext.Project_ID = task.ProjectID;
            tmContext.userID = task.EmployeeId;
            ParentTask_Master pmContext = new ParentTask_Master();
            //pmContext.Parent_ID =(int) parentTask.Parent_ID;
            pmContext.Parent_Task = parentTask.Parent_Task;

            IsTaskAdded = dataAccess.AddTask(tmContext, pmContext);
            return IsTaskAdded;
        }
        #endregion

        #region
        /// <summary>
        /// UpdateTask
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parentTask"></param>
        /// <returns></returns>
        public bool UpdateTask(TaskMangerContext task, ParentTaskMangerContext parentTask)
        {
            Task_Master tmContext = new Task_Master();
            tmContext.Task_ID = task.Task_ID;
            tmContext.Parent_ID = task.Parent_ID;
            tmContext.Task = task.Task;
            tmContext.Start_Date = task.StartDate;
            tmContext.End_Date = task.EndDate;
            tmContext.Priority = task.Priority;
            tmContext.isTaskEnded = task.IsTaskEnded;
            tmContext.ParentTask = task.ParentTask;
            tmContext.Project_ID = Convert.ToInt32(task.ProjectID);
            tmContext.userID = task.EmployeeId;
            ParentTask_Master pmContext = new ParentTask_Master();
            pmContext.Parent_ID =(int)task.Task_ID;
            pmContext.Parent_Task = task.Task;
            bool IsTaskUpdated;
            IsTaskUpdated = dataAccess.UpdateTask(tmContext, pmContext);
            return IsTaskUpdated;
        }
        #endregion

        #region
        /// <summary>
        /// EndTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool EndTask(int taskId)
        {
            bool IsTaskEnded;
            IsTaskEnded = dataAccess.EndTask(taskId);
            return IsTaskEnded;

        }
        #endregion

        #region
        /// <summary>
        /// GetTaskById
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskMangerContext GetTaskById(int taskId)
        {
            TaskMangerContext taskData = new TaskMangerContext();
            Task_Master task = (dataAccess.GetTaskById(taskId));
            taskData.Task_ID = task.Task_ID;
            taskData.Parent_ID = task.Parent_ID;
            taskData.Task = task.Task;
            taskData.StartDate = task.Start_Date;
            taskData.EndDate = task.End_Date;
            taskData.Priority = task.Priority;
            taskData.IsTaskEnded = task.isTaskEnded;
            taskData.ParentTask = task.ParentTask;
            taskData.Start_Date = task.Start_Date.Value.ToShortDateString();
            taskData.End_Date = task.End_Date.Value.ToShortDateString();
            taskData.Project_ID = Convert.ToString(task.Project_ID);
            taskData.EmployeeId = Convert.ToInt32(task.userID);
            return taskData;
        }
        #endregion

        #region 
        /// <summary>
        /// ParentTask
        /// </summary>
        /// <returns></returns>
        public List<ParentTaskMangerContext> ParentTask()
        {
            List<ParentTask_Master> ParentTaskList;
            ParentTaskList = dataAccess.ParentTask();
            List<ParentTaskMangerContext> pmContext = (from ptlist in ParentTaskList
                                                       select new ParentTaskMangerContext
                                                       {
                                                           Parent_ID = ptlist.Parent_ID,
                                                           Parent_Task = ptlist.Parent_Task
                                                       }).ToList();

            return pmContext;
        }
        #endregion


        #region GetActiveUserList
        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        public List<UserManagerContext> GetUserList(string sortColumn)
        {
            List<User_Master> userList;
            userList = dataAccess.GetUserList();
            List<UserManagerContext> userListMaster = null;
            if (sortColumn.ToLower() == "firstname")
            {
                userListMaster = (from user in userList
                                                           select new UserManagerContext()
                                                           {
                                                               FirstName = user.First_Name,
                                                               LastName = user.Last_Name,
                                                               EmployeeID = user.Employee_ID.ToString(),
                                                               UserID = user.User_ID.ToString()
                                                           }).ToList().OrderBy(i => i.FirstName).ToList();
            }
            else if (sortColumn.ToLower() == "lastname")
            {
                 userListMaster = (from user in userList
                                                           select new UserManagerContext()
                                                           {
                                                               FirstName = user.First_Name,
                                                               LastName = user.Last_Name,
                                                               EmployeeID = user.Employee_ID.ToString(),
                                                               UserID = user.User_ID.ToString()
                                                           }).ToList().OrderBy(i => i.LastName).ToList();
            }
            else
            {
                 userListMaster = (from user in userList
                                                           select new UserManagerContext()
                                                           {
                                                               FirstName = user.First_Name,
                                                               LastName = user.Last_Name,
                                                               EmployeeID = user.Employee_ID.ToString(),
                                                               UserID = user.User_ID.ToString()
                                                           }).ToList().OrderBy(i => i.EmployeeID).ToList();
            }

                return userListMaster;

        }
        #endregion

        #region AddUpdateUser
        /// <summary>
        /// Ad update user
        /// </summary>
        /// <returns></returns>
        public string AddOrUpdateUsererList(UserManagerContext userData)
        {
            string result = string.Empty;


            User_Master userListMaster = new User_Master();

            userListMaster.First_Name = userData.FirstName;
            userListMaster.Last_Name = userData.LastName;
            userListMaster.Employee_ID = userData.EmployeeID;
            userListMaster.User_ID = Convert.ToInt32(userData.UserID);

            result = dataAccess.AddOrUpdateUser(userListMaster);
            return result;


        }
        #endregion

        #region 
        /// <summary>
        /// ParentTask
        /// </summary>
        /// <returns></returns>
        public bool DeleteUser(string userID)
        {
         
            bool result = dataAccess.DeleteUser(Convert.ToInt32(userID));           

            return result;
        }
        #endregion

        #region 
        /// <summary>
        /// Delete Project
        /// </summary>
        /// <returns></returns>
        public bool DeleteProject(string projectID)
        {

            bool result = dataAccess.DeleteProject(Convert.ToInt32(projectID));

            return result;
        }
        #endregion

        #region AddUpdateUser
        /// <summary>
        /// Ad update user
        /// </summary>
        /// <returns></returns>
        public string AddOrUpdateProject(ProjectrManagerContext projModel)
        {
            string result = string.Empty;


            Project_Master addProject = new Project_Master();

            addProject.Project = projModel.Project;
            addProject.Project_ID = projModel.Project_ID;
            addProject.Priority = projModel.Priority;
            addProject.Start_Date = projModel.Start_Date;
            addProject.End_Date = projModel.End_Date;
            addProject.Completed=projModel.Manager;
        

            result = dataAccess.AddUpdateProject(addProject);
            return result;


        }
        #endregion

        #region GetActiveUserList
        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        public List<ProjectrManagerContext> GetProjectList(string sortColumn)
        {
            List<ProjectrManagerContext> projList;
            projList = dataAccess.GetProjectList();
            List<ProjectrManagerContext> userListMaster = null;
            if (sortColumn.ToLower() == "startdate")
            {
                userListMaster = (from proj in projList
                                  select new ProjectrManagerContext()
                                  {
                                      Project_ID = proj.Project_ID,
                                      Project = proj.Project,
                                      Priority = proj.Priority,
                                      StartDate = proj.Start_Date.Value.ToShortDateString(),
                                      EndDate = proj.End_Date.Value.ToShortDateString(),
                                      NoofTask=proj.NoofTask,
                                      NoofTaskCompleted=proj.NoofTaskCompleted,
                                      Manager=proj.Manager
                                  }).ToList().OrderBy(i => i.Start_Date).ToList();
            }
            else if (sortColumn.ToLower() == "enddate")
            {
                userListMaster = (from proj in projList
                                  select new ProjectrManagerContext()
                                  {
                                      Project_ID = proj.Project_ID,
                                      Project = proj.Project,
                                      Priority = proj.Priority,
                                      StartDate = proj.Start_Date.Value.ToShortDateString(),
                                      EndDate = proj.End_Date.Value.ToShortDateString(),
                                      NoofTask = proj.NoofTask,
                                      NoofTaskCompleted = proj.NoofTaskCompleted,
                                      Manager = proj.Manager
                                  }).ToList().OrderBy(i => i.End_Date).ToList();
            }
            else if (sortColumn.ToLower() == "priority")
            {
                userListMaster = (from proj in projList
                                  select new ProjectrManagerContext()
                                  {
                                      Project_ID = proj.Project_ID,
                                      Project = proj.Project,
                                      Priority = proj.Priority,
                                      StartDate = proj.Start_Date.Value.ToShortDateString(),
                                      EndDate = proj.End_Date.Value.ToShortDateString(),
                                      NoofTask = proj.NoofTask,
                                      NoofTaskCompleted = proj.NoofTaskCompleted,
                                      Manager = proj.Manager
                                  }).ToList().OrderByDescending(i => i.Priority).ToList();
            }
            else
            {

                userListMaster = (from proj in projList
                                  select new ProjectrManagerContext()
                                  {
                                      Project_ID = proj.Project_ID,
                                      Project = proj.Project,
                                      Priority = proj.Priority,
                                      StartDate = proj.Start_Date.Value.ToShortDateString(),
                                      EndDate = proj.End_Date.Value.ToShortDateString(),
                                      NoofTask = proj.NoofTask,
                                      NoofTaskCompleted = proj.NoofTaskCompleted,
                                      Manager = proj.Manager
                                  }).ToList().OrderByDescending(i => i.NoofTaskCompleted).ToList();
            }

            return userListMaster;

        }
        #endregion


    }
}
