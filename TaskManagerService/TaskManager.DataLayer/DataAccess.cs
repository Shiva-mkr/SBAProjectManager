using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManger.DTOContext;

namespace TaskManager.DataLayer
{
    public class DataAccess
    {
        private readonly Task_ManagerEntities dbContext = new Task_ManagerEntities();

        #region GetTaskList

       
        public List<Task_Master> GetTaskList()
        {
            List<Task_Master> taskList = new List<Task_Master>();
            try
            {
var Query = (from task in dbContext.Task_Master join Ptask in dbContext.ParentTask_Master on task.Parent_ID equals Ptask.Parent_ID
                            into td   from Task_Masters in td.DefaultIfEmpty()                       
                            select new 
                            {
                                Task_ID = task.Task_ID,
                                Parent_ID = task.Parent_ID,
                                Task = task.Task,
                                StartDate = task.Start_Date,
                                EndDate = task.End_Date,
                                Priority = task.Priority,
                                IsTaskEnded = task.isTaskEnded,
                                ParentTask = Task_Masters.Parent_Task,
                                projectID=task.Project_Master.Project_ID,
                                projectName=task.Project_Master.Project
                                
                            }).Distinct().ToList();

                taskList = Query.ToList().Select(r => new Task_Master
                {
                    Task_ID = r.Task_ID,
                    Parent_ID = r.Parent_ID,
                    Task = r.Task,
                    Start_Date = r.StartDate,
                    End_Date = r.EndDate,
                    Priority = r.Priority,
                    isTaskEnded = r.IsTaskEnded,
                    ParentTask = r.ParentTask,
                    ProjectName=r.projectName,
                    Project_ID=r.projectID
                    
                }).ToList();
            }
            catch (Exception ex)
            {
               
            }
            return taskList;
        }
        #endregion


        #region AddTask
        public bool AddTask(Task_Master task,ParentTask_Master parentTask)
        {
            try
            {             
           
                var addTask = dbContext.Task_Master.Add(task);
                var addParentTask = dbContext.ParentTask_Master.Add(parentTask);
                User_Master user = dbContext.User_Master.Where(i => i.User_ID == task.userID).FirstOrDefault();
                user.Project_ID = task.Project_ID;
                user.Task_ID = addTask.Task_ID;
                dbContext.SaveChanges();
        
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region UpdateTask
        public bool UpdateTask(Task_Master task, ParentTask_Master parentTask)
        {
            try
            {
                Task_Master existTask = dbContext.Task_Master.Find(task.Task_ID);
                ((IObjectContextAdapter)dbContext).ObjectContext.Detach(existTask);

                dbContext.Entry(task).State = EntityState.Modified;
                dbContext.SaveChanges();

                ParentTask_Master existPTask = dbContext.ParentTask_Master.Find(parentTask.Parent_ID);
                ((IObjectContextAdapter)dbContext).ObjectContext.Detach(existPTask);

                //User_Master user = dbContext.User_Master.Find(task.userID);
                //user.Task_ID = task.Task_ID;
                //((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
                dbContext.Entry(parentTask).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region EndTask
        public bool EndTask(int taskId)
        {
            try
            {
                Task_Master taskData = dbContext.Task_Master.Find(taskId);

                taskData.isTaskEnded = 1;
                dbContext.Entry(taskData).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetTaskById
        public Task_Master GetTaskById(int taskId)
        {
            Task_Master taskData = new Task_Master();
            try
            {
                // taskData = dbContext.Task_Master.Find(taskId);
                var Query = (from task in dbContext.Task_Master
                             join Ptask in dbContext.ParentTask_Master on task.Parent_ID equals Ptask.Parent_ID 
                             
                             into td  from Task_Masters in td.DefaultIfEmpty()
                             where task.Task_ID == taskId
                             select new
                             {
                                 Task_ID = task.Task_ID,
                                 Parent_ID = task.Parent_ID,
                                 Task = task.Task,
                                 StartDate = task.Start_Date,
                                 EndDate = task.End_Date,
                                 Priority = task.Priority,
                                 IsTaskEnded = task.isTaskEnded,
                                 ParentTask = Task_Masters.Parent_Task,
                                 ProjectID = task.Project_Master.Project_ID,
                                 //user_ID = Task_Masters.User_ID,
                             }).Distinct().ToList();

                
                taskData = Query.ToList().Select(r => new Task_Master
                {
                    Task_ID = r.Task_ID,
                    Parent_ID = r.Parent_ID,
                    Task = r.Task,
                    Start_Date = r.StartDate,
                    End_Date = r.EndDate,
                    Priority = r.Priority,
                    isTaskEnded = r.IsTaskEnded,
                    ParentTask = r.ParentTask,
                    Project_ID=r.ProjectID,
                    userID= dbContext.User_Master.Where(i => i.Task_ID == r.Task_ID).FirstOrDefault().User_ID
                }).FirstOrDefault();
                return taskData;
            }
            catch (Exception ex)
            {
                return taskData;
            }
        }
        #endregion

        #region ParentTask
        public List<ParentTask_Master> ParentTask()
        {
            List<ParentTask_Master> parentTaskList = new List<DataLayer.ParentTask_Master>();

            try
            {

               var PTask = (from task in dbContext.Task_Master select new { Id = task.Task_ID, Parent_Task = task.Task }).ToList();
               foreach (var data in PTask) {
                    ParentTask_Master ptask = new ParentTask_Master();
                    ptask.Parent_ID = data.Id;
                    ptask.Parent_Task = data.Parent_Task;
                    parentTaskList.Add(ptask);
                }
                return parentTaskList;
            }
            catch (Exception ex)
            {
                
                using (var file = new StreamWriter(@"C:\FSD Capsule\WebPublish\log.txt", true))
                {
                    file.WriteLine(ex.ToString());
                    file.Close();
                }
                return parentTaskList;
            }
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        public List<User_Master> GetUserList()
        {

            List<User_Master> userList = (from user in dbContext.User_Master
                                          orderby user.User_ID descending
                                          select new
                                          {
                                              First_Name = user.First_Name,
                                              Last_Name = user.Last_Name,
                                              Employee_ID = user.Employee_ID,
                                              User_ID = user.User_ID
                                          }).ToList().Select(user => new User_Master()
                                          {
                                              First_Name = user.First_Name,
                                              Last_Name = user.Last_Name,
                                              Employee_ID = user.Employee_ID,
                                              User_ID = user.User_ID
                                          }).ToList();

                return userList;
            
        }
        #endregion 

        #region AddOrUpdateUser
        /// <summary>
        /// Add update user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public string AddOrUpdateUser(User_Master userModel)
        {

            if (userModel != null)
            {
                User_Master addUser = new User_Master();
                addUser.First_Name = userModel.First_Name;
                addUser.Last_Name = userModel.Last_Name;
                addUser.Employee_ID = userModel.Employee_ID;

                if (userModel.User_ID == 0)
                {
                    dbContext.User_Master.Add(addUser);
                }
                else
                {
                    addUser.User_ID = userModel.User_ID;
                    dbContext.Entry(addUser).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
                }

            return "true";
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Delete from user list
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public bool DeleteUser(int userID)
        {


            //User_Master deleteUser = new User_Master();
            User_Master deleteUser = dbContext.User_Master.Where(i => i.User_ID == userID).FirstOrDefault();
            dbContext.User_Master.Remove(deleteUser);
           // dbContext.Entry(deleteUser).State = System.Data.Entity.EntityState.Deleted;
            dbContext.SaveChanges();

            return true;


        }
        #endregion

        #region GetProjectList
        /// <summary>
        /// Get Poject
        /// </summary>
        /// <returns></returns>
        public List<ProjectrManagerContext> GetProjectList()
        {

            List<ProjectrManagerContext> projectLIst = (from proj in dbContext.Project_Master 
                                                orderby proj.Project_ID descending
                                                select new
                                                {
                                                    Project_ID = proj.Project_ID,
                                                    Project = proj.Project,
                                                    Priority = proj.Priority,
                                                    Start_Date = proj.Start_Date,
                                                    End_Date = proj.End_Date,
                                                    taskcount= proj.Task_Master.Count(),
                                                    taskCompletedCount=proj.Task_Master.Where(i=>i.isTaskEnded==1).Count(),
                                                    Manager=proj.Completed
                                                }).ToList().Select(projo => new ProjectrManagerContext()
                                                {
                                                    Project = projo.Project,
                                                    Project_ID = projo.Project_ID,
                                                    Priority = projo.Priority,
                                                    Start_Date = projo.Start_Date,
                                                    End_Date = projo.End_Date,
                                                    NoofTask=projo.taskcount,
                                                    NoofTaskCompleted=projo.taskCompletedCount,
                                                    Manager = projo.Manager
                                                }).ToList();

            return projectLIst;

        }
        #endregion


        #region Delete Project
        /// <summary>
        /// Delete from user list
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public bool DeleteProject(int projID)
        {


            //User_Master deleteUser = new User_Master();
            Project_Master deleteUser = dbContext.Project_Master.Where(i => i.Project_ID == projID).FirstOrDefault();
            dbContext.Project_Master.Remove(deleteUser);
            // dbContext.Entry(deleteUser).State = System.Data.Entity.EntityState.Deleted;
            dbContext.SaveChanges();

            return true;


        }
        #endregion

        #region AddOrUpdateUser
        /// <summary>
        /// Add update user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public string AddUpdateProject(Project_Master projModel)
        {

            if (projModel != null)
            {
                Project_Master addProject = new Project_Master();
                addProject.Project = projModel.Project;

                addProject.Priority = projModel.Priority;
                addProject.Start_Date = projModel.Start_Date;
                addProject.End_Date = projModel.End_Date;
                addProject.Completed = projModel.Completed;
                if (projModel.Project_ID == 0)
                {
                    dbContext.Project_Master.Add(addProject);
                }
                else
                {
                    addProject.Project_ID = projModel.Project_ID;
                    dbContext.Entry(addProject).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return "true";
        }
        #endregion

    }
}
