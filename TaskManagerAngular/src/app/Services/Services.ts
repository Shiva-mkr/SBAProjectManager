import { Injectable } from '@angular/core';
import { taskData, Task,ProjectModel } from '../Services/Model';
import { Http, Headers, RequestOptions } from '@angular/http';


@Injectable({
  providedIn: 'root'
})

export class SharedService {

  taskdata: any;
  taskList: any;
  //url='http://172.18.1.114:81//api/Task'
 url='http://localhost:50451/api/Task';
 Userurl='http://localhost:50451/api/User';

constructor(private httpServ: Http) { }
  getTaskListUri = this.url+"/GetTaskList";

  GetTaskList(){
    return this.httpServ.get(this.getTaskListUri);
  }

  getTaskUri = this.url+"/GetTaskById";
  GetTaskById(Id:number){
    return this.httpServ.get(this.getTaskUri+ "?Id="+Id);
  }

  CreateTaskUri = this.url+"/CreateTask";
  AddTask(data: Task) {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });
    return this.httpServ.post(this.CreateTaskUri, data, options);
  }

  updateTaskUri = this.url+"/UpdateTask";
  UpdateTask(data: Task) {

    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.httpServ.post(this.updateTaskUri, data, options);
  }

  endTaskUri = this.url+"/EndTask?Id=";
  EndTask(Id:number){
    return this.httpServ.get(this.endTaskUri+Id);
  }

  getParentTaskUri = this.url+"/ParentTask";
  GetParentTask(){
    return this.httpServ.get(this.getParentTaskUri);
  }

  getUserUri = this.Userurl+"/GetUserList?sortColumn=";
  GetUserList(data:string){
    return this.httpServ.get(this.getUserUri+data);
  }
  saveUserURI = this.Userurl+"/AddOrUpdateUsererList";
  AddOrUpdateUser(data: Task) {

    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.httpServ.post(this.saveUserURI, data, options);
  }

  deleteuserUri = this.Userurl+"/DeleteUser?ID=";
  DeleteUser(Id:string){
    return this.httpServ.get(this.deleteuserUri+Id);
  }

  deleteprojectUri = this.Userurl+"/DeleteProject?ID=";
  DeleteProject(Id:string){
    return this.httpServ.get(this.deleteprojectUri+Id);
  }
  getProjectUri = this.Userurl+"/GetProjectList?sortColumn=";
  GetProjectList(data:string){
    return this.httpServ.get(this.getProjectUri+data);
  }

  saveProjectURI = this.Userurl+"/AddOrUpdateProject";
  AddOrUpdateProject(data: ProjectModel) {

    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.httpServ.post(this.saveProjectURI, data, options);
  }


}