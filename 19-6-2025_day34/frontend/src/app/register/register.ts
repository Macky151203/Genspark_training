import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Loginservice } from '../services/loginservice';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder,private loginservice:Loginservice,private router:Router) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      role: ['user', Validators.required]
    });
  }

  onRegisterClick() {
    if (this.registerForm.valid) {
      console.log("Registration Data:", this.registerForm.value);
      const obj={name:this.registerForm.value.name,email:this.registerForm.value.email,password:this.registerForm.value.password,phoneNumber:this.registerForm.value.phoneNumber}
      this.loginservice.register(obj,this.registerForm.value.role).subscribe({
        next:(data:any)=>{
          console.log(data);
          //route here
          this.router.navigate(['/login']);
        }
      })
    }
  }
}
