import { Component } from '@angular/core';
import { Testservice } from '../services/testservice';

@Component({
  selector: 'app-comp1',
  imports: [],
  templateUrl: './comp1.html',
  styleUrl: './comp1.css'
})
export class Comp1 {
  constructor(public service:Testservice) {

  }

}
