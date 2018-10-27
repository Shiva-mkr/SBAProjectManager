using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManger.DTOContext
{
    public class TaskMangerContext
    {
        public int Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Task { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> IsTaskEnded { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string ParentTask { get; set; }
        public string Project_ID { get; set; }
        public string Project_Name{ get; set; }

        public int EmployeeId { get; set; }
        public int ProjectID { get; set; }
    }
    public class ParentTaskMangerContext
    {
        public int Id { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }

    public class UserManagerContext
    {
        public string UserID { get; set; }
        public string EmployeeID{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ProjectrManagerContext
    {
        public int Project_ID { get; set; }
        public string Project { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string Priority { get; set; }
        public int NoofTask { get; set; }
        public int NoofTaskCompleted { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Nullable<int> Manager { get; set; }


    }
}
