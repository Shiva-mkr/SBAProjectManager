import { Component,NgModule,OnInit } from '@angular/core';
import { SharedService } from '../app/Services/Services';
import { Http, Response,HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Task } from './Services/Model';
declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  
   constructor(public _service: SharedService) { }

   ngOnInit() {
   

  }

}



