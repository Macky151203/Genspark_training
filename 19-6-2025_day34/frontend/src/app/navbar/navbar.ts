import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Loginservice } from '../services/loginservice';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {

  showToast: boolean = false;

  constructor(public loginservice: Loginservice) { }



  handlelogout() {
    this.loginservice.logout();
    this.showToast = true;

    setTimeout(() => {
      this.showToast = false;
    }, 2000);

  }

  closeToast() {
    this.showToast = false;
  }

}
