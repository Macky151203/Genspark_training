import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Loginservice {

  private apiUrl = 'http://localhost:5136/api/v1/authentication';
  public isloggedin= signal(false);

  constructor(private httpclient:HttpClient) { 
    if(localStorage.getItem('token'))
    {
      this.isloggedin.update(value => true);
    }
  }

  
  logout() {
    localStorage.removeItem('token');
    this.isloggedin.update(value => false);
  }

  login(username: string, password: string): Observable<any> {
    const body = { username, password };
    return this.httpclient.post(this.apiUrl, body);
  }
}
