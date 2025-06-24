import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomepageService {



  constructor(private http: HttpClient) { }

  getallevents(): Observable<any> {
    return this.http.get<any>('http://localhost:5136/api/events');
  }

}
