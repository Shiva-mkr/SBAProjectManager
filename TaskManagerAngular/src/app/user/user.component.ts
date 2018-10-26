import { Component,NgModule,OnInit,Pipe } from '@angular/core';
import { SharedService } from '../../app/Services/Services';
import { Http, Response,HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Task, User } from '../Services/Model';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(public _service: SharedService) { }

  ngOnInit() {
    this.GetUser('id');
  }
  FirstName='';
  LastName='';
  EmployeeID='';
  UserID:Number;
  data:any;
  result:any;
  userList:any;
  userListMaster:any;
  SearchName='';
  ResetUser()
  {
   this.FirstName='';
   this.LastName='';
   this.EmployeeID='';
   this.UserID=0;
  }
  SaveUser(){
    this.data=new User();
    this.data.FirstName=this.FirstName;
    this.data.lastName=this.LastName;
    this.data.EmployeeID=this.EmployeeID;
    this.data.UserID=this.UserID;
    if (this.data.FirstName.length == 0 || this.data.lastName.length == 0 || this.data.EmployeeID.length == 0) { 
      alert("Please enter all mandatory fields"); 
        return; 
     } 
     this._service.AddOrUpdateUser(this.data).subscribe((res: Response) => {
      this.result = res.json();
      if(this.result=='true'){
       alert("User details saved succesfully");
       this.GetUser('id');
      }
      else{
        alert("Unable to save details");
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
  Edituser(data){
   this.FirstName=data.FirstName;
   this.LastName=data.LastName;
   this.UserID=data.UserID;
   this.EmployeeID=data.EmployeeID;
  }

  DeleteUser(data){
    this._service.DeleteUser(data.UserID).subscribe((res: Response) => {
      this.result = res.json();
      if(this.result!='Error'){
        alert("user  deleted succesfully");
        this.GetUser('id');
      }
      else{
        alert("Unable to delete user");
      }
      
    }, (error) => {
      console.log("Error While Processing Results");
    });
  }

  SearchUser(){    
    //alert(this.SearchName);
    this.userList = this.userListMaster.filter(x => x.LastName.toLowerCase().includes(this.SearchName.toLowerCase())||x.FirstName.toLowerCase().includes(this.SearchName.toLowerCase()) );
  }
  

}
