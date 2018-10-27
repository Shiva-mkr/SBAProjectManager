import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpModule,Http } from '@angular/http';
import { AngularDateTimePickerModule } from 'angular2-datetimepicker';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { TaskComponent } from './task/task.component';
import { ViewTaskComponent } from './view-task/view-task.component';
import { ProjectComponent } from './project/project.component';

const appRoutes: Routes = [
  { path: 'user', component: UserComponent },
  { path: 'task',      component: TaskComponent },
  { path: 'task/:id',      component: TaskComponent },
  { path: 'viewTask', component: ViewTaskComponent },  
  { path: 'project', component: ProjectComponent }, 
  { path: '',redirectTo: '/task', pathMatch: 'full'  }  
];



@NgModule({
  declarations: [
    AppComponent,UserComponent, TaskComponent, ViewTaskComponent, ProjectComponent
  ],
  imports: [
    BrowserModule,FormsModule,HttpModule,AngularDateTimePickerModule,
     RouterModule.forRoot(
      appRoutes,
     
    )
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
