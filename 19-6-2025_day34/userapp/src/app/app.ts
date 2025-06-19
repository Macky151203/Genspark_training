import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Users } from "./users/users";

@Component({
  selector: 'app-root',
  imports: [ Users],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'userapp';
}
