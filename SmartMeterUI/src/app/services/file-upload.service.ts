import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  constructor(private http: HttpClient) { }
  upload(fileToUpload : any) {
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload,fileToUpload.name);
    return this.http.post('https://localhost:7165/api/meter-reading-uploads',formData)
           .subscribe(res => {
             console.log(res);
          });
  }
}
