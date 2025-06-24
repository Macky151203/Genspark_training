import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Testservice {

  public count = signal(0)

  constructor() { }

  increment(){
    this.count.update(c => c + 1)
  }

  decrement(){
    this.count.update(c => c - 1)
  }
}
