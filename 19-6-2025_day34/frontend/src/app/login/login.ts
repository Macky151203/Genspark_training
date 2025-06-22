import { Component } from '@angular/core';
import { Loginservice } from '../services/loginservice';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  constructor(private loginservice: Loginservice){}

  onLoginClick(){
    this.loginservice.login("cust10@gmail.com","cust10").subscribe({
      next: (data) => {
        console.log(data);
        localStorage.setItem("token", data.token);
        localStorage.setItem("username", data.username);

        this.loginservice.isloggedin.update(value => true);
        //push to /home
      },
      error: (err) => {
        console.error('Login failed:', err.error);
        alert(`Login failed,${err.error}`);
      }
    });
  }

}
