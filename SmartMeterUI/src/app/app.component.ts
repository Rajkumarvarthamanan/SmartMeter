import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { FileUploadService } from './services/file-upload.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public service :FileUploadService){}
  title = 'SmartMeterUI';
  selectedFile : File ;
  onFileSelected(event: any)
  {
    this.selectedFile=event.target.files[0];
  }
  onUpload()
  {
    this.service.upload(this.selectedFile); 
  }
}
