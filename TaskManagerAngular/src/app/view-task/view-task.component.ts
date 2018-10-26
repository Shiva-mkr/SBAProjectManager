import { Component,NgModule,OnInit } from '@angular/core';
import { SharedService } from '../../app/Services/Services';
import { Http, Response,HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Task } from './../Services/Model';
declare var $: any;

@Component({
  selector: 'app-view-task',
  templateUrl: './view-task.component.html',
  styleUrls: ['./view-task.component.css']
})
export class ViewTaskComponent implements OnInit {

  title = 'TaskManagerAngular';
  AddTask=true;
  ViewTask=false;
  AddMenucss='active';
  ViewMenucss=''
  TaskName='';
  Priority='50';
  ParentTask='';
  StartDate='';
  EndDate:string;  
  
   searchTask='';  
   searchPTask='';
   searchpriorityFrom=0;
   SearchPriorityTo=0;
TaskListMaster:any;
  ParentTaskList:any;
  TaskList:any;
  data:Task;
  AddtaskResStatus:false;
  Task_ID:number=0;
  searchFromDate='';
  searchToDate='';
  constructor(public _service: SharedService) { }

  ngOnInit() {
  this.GetTaskList();
   this.ParentTask = null;

 }

 SearchTask()
 {
   if(this.searchTask.length>0){
     this.SearchByTask();
     return;
   }
   else if(this.searchPTask.length>0)
   {
     
this.SearchByParentTask();
return;
   }
   else if(this.searchpriorityFrom>0 || this.SearchPriorityTo>0)
   {      
     this.SearchByPriority();
     return;
   }
   else if(this.searchFromDate.length>0 || this.searchToDate.length>0)
   {      
     this.SearchByDate();
     return;
   }
   this.TaskList=this.TaskListMaster;
 }
 SearchByTask() { 
   this.TaskList = this.TaskListMaster.filter(x => x.Task.toLowerCase().includes(this.searchTask.toLowerCase())); 
 }
 
 SearchByParentTask() { 
   this.TaskList = this.TaskListMaster.filter(x=>(x.ParentTask!=null&&  x.ParentTask.toLowerCase().includes(this.searchPTask.toLowerCase()))); 
       
 }

 SearchByPriority() { 
   if(this.searchpriorityFrom>0 && this.SearchPriorityTo>0){
   this.TaskList = this.TaskListMaster.filter(x => (x.Priority >= this.searchpriorityFrom && x.Priority <= this.SearchPriorityTo)); 
   }
   else if(this.searchpriorityFrom>0){
     
     this.TaskList = this.TaskListMaster.filter(x => (x.Priority >= this.searchpriorityFrom)); 
     }
     else if(this.SearchPriorityTo>0){
       this.TaskList = this.TaskListMaster.filter(x => (x.Priority <= this.SearchPriorityTo)); 
       }
 }
 
 SearchByDate() { 
   if(this.searchFromDate.length>0 && this.searchToDate.length>0){
   this.TaskList = this.TaskListMaster.filter(x => (x.startdate >= this.searchFromDate && x.startdate <= this.searchToDate)); 
   }
   else if(this.searchFromDate.length>0){      
     this.TaskList = this.TaskListMaster.filter(x => (new Date(x.StartDate) >=new Date(this.searchFromDate))); 
     }
     else if(this.searchToDate.length>0){
       this.TaskList = this.TaskListMaster.filter(x => (new Date(x.EndDate) <= new Date( this.searchToDate))); 
       }
 } 


 GetParentTask() {
   this._service.GetParentTask().subscribe((res: Response) => {
     this.ParentTaskList = res.json();
   }, (error) => {
     console.log("Error While Processing Results");
   });
 }

 GetTaskList() {
   this._service.GetTaskList().subscribe((res: Response) => {
     this.TaskListMaster = res.json();
     this.TaskList=this.TaskListMaster;
   }, (error) => {
     console.log("Error While Processing Results");
   });
 }

  ToggleView(type)
  {
    
   if(type=="add"){
     this.AddTask=true;
     this.ViewTask=false;
     this.AddMenucss='active';
   this.ViewMenucss='';
   this.ResetTaskData();
   }
   else{
     this.AddTask=false;
     this.ViewTask=true;
     this.AddMenucss='';
     this.ViewMenucss='active';
     this.GetTaskList();
   }
  }

  ResetTaskData()
  {
   
   this.TaskName='';
   this.Priority='';
   this.ParentTask=null;
   this.StartDate='';
   this.EndDate='';
   this.Task_ID=0;
   }



  
  EditTask(param)
  {
   
   this._service.GetTaskById(param.Task_ID).subscribe((res: Response) => {
     this.data = res.json();
     //this.GetParentTask();
     if(this.data!=null){
      
      this.ToggleView('add');
      this.TaskName=this.data.Task;
      //this.StartDate=this.data.Start_Date.toString().slice(0,10);
      this.StartDate= new Date(this.data.Start_Date).toISOString().substring(0, 10);
     // alert(this.StartDate);
      this.EndDate= new Date(this.data.End_Date).toISOString().substring(0, 10);
      this.Priority=this.data.Priority;
      this.ParentTask=(this.data.Parent_ID=='0')?null:this.data.Parent_ID;
      this.Task_ID=this.data.Task_ID;
//     $('#startdate').val(this.StartDate);
      //document.getElementById('startdate')[0].value=this.StartDate;
//alert(this.StartDate);
     }
     else{
       alert("Unable to get task data");
     }
     
   }, (error) => {
     console.log("Error While Processing Results");
   });
 
  }


  EndTask(param)
  {
   
   this._service.EndTask(param.Task_ID).subscribe((res: Response) => {
     this.AddtaskResStatus = res.json();
     //this.GetParentTask();
     if(!this.data){
      
   alert("task ended succesfully");
   this.GetTaskList();
     }
     else{
       alert("Unable to end task data");
     }
     
   }, (error) => {
     console.log("Error While Processing Results");
   });
 
  }

  CreateTask(){
   //alert(this.Task_ID);
     this.data=new Task();
      this.data.Task=this.TaskName;
      this.data.ParentTask=this.ParentTask;
      
      this.data.StartDate=this.StartDate;
      this.data.EndDate=this.EndDate;
      this.data.Priority=this.Priority;
        this.data.Task_ID=this.Task_ID;
      if (this.data.Task.length == 0 || this.data.StartDate.length == 0 || this.data.EndDate.length == 0) { 
             alert("Please enter all mandatory fields"); 
               return; 
            } 
           
     if(this.data.Task_ID==0){
       this._service.AddTask(this.data).subscribe((res: Response) => {
         this.AddtaskResStatus = res.json();
         if(this.AddtaskResStatus){
          alert("Task created succesfully");
          this.GetParentTask();
         }
         else{
           alert("Unable to create task");
         }
         
       }, (error) => {
         console.log("Error While Processing Results");
       });
     }
     else{
       this.data.Parent_ID=this.ParentTask;
       this._service.UpdateTask(this.data).subscribe((res: Response) => {
         this.AddtaskResStatus = res.json();
         if(this.AddtaskResStatus){
          alert("Task updated succesfully");
          this.GetParentTask();
         }
         else{
           alert("Unable to update task");
         }
         
       }, (error) => {
         console.log("Error While Processing Results");
       });
     }
     
  }



}
