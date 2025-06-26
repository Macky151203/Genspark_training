import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private hubConnection!: signalR.HubConnection;

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
      console.log(a);
      console.log(b);
      console.log(c);
      console.log(d);
      console.log(e);
      this.messages.push({ a,b,c,d,e });
    });
  }

  //trigger an event from here and get by recent event by adding created at attribute


  //


}
