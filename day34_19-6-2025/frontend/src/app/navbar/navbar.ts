import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Loginservice } from '../services/loginservice';
import { CommonModule } from '@angular/common';
import { ProfileService } from '../services/profile-service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit {

  showToast: boolean = false;
  islogged:boolean=false;
  name:string="";

  constructor(
    public loginservice: Loginservice
  ) {
    this.loginservice.islogged$.subscribe((data:any)=>this.islogged=data);
    this.loginservice.name$.subscribe((data:any)=>this.name=data);
  }

  ngOnInit(): void {
    
  }

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
