import { Component,NgModule,OnInit,Pipe } from '@angular/core';
import { SharedService } from '../../app/Services/Services';
import { Http, Response,HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Task, User,  ProjectModel } from '../Services/Model';
@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  constructor(public _service: SharedService) { }

  ngOnInit() {
    this.GetProjectList('startdate');
    this.GetUser('firstname');
  }
  ProjectName='';
  StartDate='';
  EndDate='';
  Priority:Number=0;
  EmployeeID='';
  projectdata:ProjectModel;
  Project_ID:Number=0;
  result:any;
  projectlist:any;
  projectlistMaster:any;
  userList:any;
  userListMaster:any;
  SearchName='';
  ResetProject(){
    this.ProjectName='';
    this.StartDate='';
    this.EndDate='';
    this.Priority=0;
    this.EmployeeID='';
    this.Project_ID=0;
  }

  SaveProject()
  {
  this.projectdata=new ProjectModel();
  this.projectdata.Project_ID=this.Project_ID.toString();
  this.projectdata.Project=this.ProjectName;
  this.projectdata.Priority=this.Priority.toString();
  this.projectdata.Start_Date  =this.StartDate;
  this.projectdata.End_Date=this.EndDate;
  this.projectdata.Manager=this.EmployeeID.toString();

  if (this.projectdata.Project.length == 0 || this.projectdata.Manager.length==0
    || this.projectdata.Start_Date.length == 0|| this.projectdata.End_Date.length == 0) { 
    alert("Please enter all mandatory fields"); 
      return; 
   } 
   this._service.AddOrUpdateProject(this.projectdata).subscribe((res: Response) => {
    this.result = res.json();
    if(this.result=='true'){
     alert("Project details saved succesfully");
     this.GetProjectList('startdate');
    }
    else{
      alert("Unable to save details");
    }
    
  }, (error) => {
    console.log("Error While Processing Results");
  });
  }



GetProjectList(data)
{
  this._service.GetProjectList(data).subscribe((res: Response) => {
    this.result = res.json();
    if(this.result!='Error'){
      this.projectlist=this.result;
      this.projectlistMaster=this.result;
    }
    else{
      alert("Unable to to get user details");
    }
    
  }, (error) => {
    console.log("Error While Processing Results");
  });
}


GetUser(data)
{
  this._service.GetUserList(data).subscribe((res: Response) => {
    this.result = res.json();
    if(this.result!='Error'){
      this.userList=this.result;
      this.userListMaster=this.result;
    }
    else{
      alert("Unable to to get user details");
    }
    
  }, (error) => {
    console.log("Error While Processing Results");
  });
}

GetEmployee(data)
{
  
  this.EmployeeID=data;
}


SearchProject(){    
  //alert(this.SearchName);
  this.projectlist = this.projectlistMaster.filter(x => x.Project.toLowerCase().includes(this.SearchName.toLowerCase()));
}

EditProject(data){
  this.Project_ID=data.Project_ID;
  this.ProjectName=data.Project;
  this.StartDate=new Date(data.StartDate).toISOString().substring(0, 10);
  this.EndDate=new Date(data.EndDate).toISOString().substring(0, 10);
 
  this.EmployeeID=data.Manager;
  this.Priority=data.Priority;
 
}

DeleteProejct(data){
  this._service.DeleteProject(data.Project_ID).subscribe((res: Response) => {
    this.result = res.json();
    if(this.result!='Error'){
      alert("Project  deleted succesfully");
      this.GetProjectList('startdate');
    }
    else{
      alert("Unable to delete project");
    }
    
  }, (error) => {
    console.log("Error While Processing Results");
  });
}

}