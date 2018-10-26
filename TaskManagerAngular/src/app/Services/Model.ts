export class Task {
    Task: string;
    ParentTask: string;
    StartDate: string;
    EndDate: string;
    Priority: string;
    Parent_ID: string;
    Task_ID: number;       
    IsTaskEnded : number;
    Start_Date:string;
    End_Date:string;
}

export class User {
    FirstName: string;
    LastName: string;
    EmployeeID: string;
    UserID: string;    
   
}

export class ProjectModel {
    Project_ID: string;
    Project: string;
    Start_Date: string;
    End_Date: string;
    Priority:string;
    Manager:string;    
   
}


export const taskData: any=""; 
