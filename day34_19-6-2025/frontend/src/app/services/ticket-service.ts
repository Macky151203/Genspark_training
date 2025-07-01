import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { BookTicketInput } from '../models/BookTicketInput';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private httpclient:HttpClient) { }

  gettickets(email:string):Observable<any>{
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpclient.get(`http://localhost:5136/api/v1/ticket/gettickets/${email}`,{headers:headers})
  }

  cancelTicket(id:number):Observable<any>{
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpclient.delete(`http://localhost:5136/api/v1/ticket/${id}/cancel`,{headers:headers})
  }

  bookticket(input:BookTicketInput): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.httpclient.post(
      `http://localhost:5136/api/v1/ticket/book`,
      input,
      {
        headers: headers,
        responseType: 'blob'
      }
    );
  }
}