import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor(private httpclient:HttpClient) { }

  gettickets(email:string):Observable<any>{
    return this.httpclient.get(`http://localhost:5136/api/v1/ticket/gettickets/${email}`)
  }
}
