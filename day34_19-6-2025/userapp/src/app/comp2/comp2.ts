import { Component } from '@angular/core';
import { Testservice } from '../services/testservice';

@Component({
  selector: 'app-comp2',
  imports: [],
  templateUrl: './comp2.html',
  styleUrl: './comp2.css'
})
export class Comp2 {
  constructor(private service:Testservice) {

  }

  onClick(){
    this.service.increment()
  }

}
