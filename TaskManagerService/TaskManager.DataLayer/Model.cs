using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataLayer
{
    public partial class Task_Master
    {
        public string ParentTask { get; set; }

        public int userID { get; set; }
        public string ProjectName { get; set; }
    }

    public partial class Task_MasterDTO: Task_Master
    {
        

    }

    }
