import { TestBed, async,ComponentFixture,inject } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Task } from './Services/Model';
import { SharedService } from '../app/Services/Services';
import { Http, Response,HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Component,NgModule,OnInit } from '@angular/core';


describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule,HttpModule ],
      declarations: [
        AppComponent
      ],
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'TaskManagerAngular'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('TaskManagerAngular');
  });

  
  
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let result :any;
  it('should add new task',inject([SharedService], (service : SharedService) => { 
    expect(service).toBeTruthy(); 
  
    
    let  taskData = new Task() 
    taskData.Task = "Test New 1"; 
    taskData.Parent_ID = '1021'; 
    taskData.Start_Date = '2018-08-25'; 
    taskData.End_Date = '2018-08-30'; 
    taskData.Priority = '50'; 
  
 
    service.AddTask(taskData).subscribe((res: Response) => { 
      result = res.json(); 
   expect(result).toBeTruthy; 
    }); 
      
  })); 


  it('should update task',inject([SharedService], (service : SharedService) => { 
    expect(service).toBeTruthy(); 
  
    
    let  taskData = new Task() 
    taskData.Task = "Test New 1 Update"; 
    taskData.Parent_ID = '1021'; 
    taskData.Start_Date = '2018-08-25'; 
    taskData.End_Date = '2018-08-30'; 
    taskData.Priority = '50'; 
  
 
    service.UpdateTask(taskData).subscribe((res: Response) => { 
      result = res.json(); 
   expect(result).toBeTruthy; 
    });
  })); 
  
  it('Get Parent Task',inject([SharedService], (service : SharedService) => { 
    expect(service).toBeTruthy(); 
    
 
    service.GetParentTask().subscribe((res: Response) => { 
      result = res.json(); 
   expect(result).toBeTruthy; 
    });
  }));  

  it('Get Task List',inject([SharedService], (service : SharedService) => { 
    expect(service).toBeTruthy(); 
    
 
    service.GetTaskList().subscribe((res: Response) => { 
      result = res.json(); 
   expect(result).toBeTruthy; 
    });
  }));  
});
