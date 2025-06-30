import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private httpclient:HttpClient){ }

  public customername=signal<any>(null)

  setcustomername(name:string){
    this.customername.set(name);
  }



  getUserProfile():Observable<any>{
    const email=localStorage.getItem('username');
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpclient.get(`http://localhost:5136/api/v1/admin/${email}`,{headers:headers})
  }
}
