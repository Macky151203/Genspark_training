import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Users } from "./users/users";
import { Comp1 } from './comp1/comp1';
import { Comp2 } from './comp2/comp2';
import { Login } from './login/login';
import { Maincomp } from './maincomp/maincomp';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'userapp';
}
