import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private hubConnection!: signalR.HubConnection;

  public eventRecieved$= new Subject<void>();
  private updatesubject=new BehaviorSubject<any[]>([]);
  updates$=this.updatesubject.asObservable();


  public messages: { a: string; b: string ,c:string,d:string,e:string}[] = [];

  constructor() { }
  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5136/eventhub') 
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connected'))
      .catch(err => console.log('SignalR error:', err));

    this.hubConnection.on('ReceiveMessage', (a: string, b: string,c:string,d:string,e:string) => {
      
      this.eventRecieved$.next();
      const obj={a,b,c,d,e};
      this.messages.push(obj);
      console.log("messages from service- ",this.messages)
      this.updatesubject.next(this.messages);

    });
  }


}
