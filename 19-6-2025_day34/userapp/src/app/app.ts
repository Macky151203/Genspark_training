import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Users } from "./users/users";
import { Comp1 } from './comp1/comp1';
import { Comp2 } from './comp2/comp2';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Users , Comp1, Comp2],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'userapp';
}
