import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../services/profile-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
//@ts-ignore
import * as bootstrap from 'bootstrap';



@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {
  userdata: any = {};
  ticketCount: number = 0;
  editData: any = {};

  constructor(private profileservice: ProfileService) { }

  ngOnInit(): void {
    const email = localStorage.getItem('username');
    if (email) {
      this.profileservice.getUserProfile().subscribe({
        next: (data: any) => {
          this.userdata = data;
          this.ticketCount = data.tickets?.length || 0;
          this.profileservice.setcustomername(data.name);
        },
        error: (err: any) => {
          console.error('Failed to load profile:', err);
        }
      });
    }
  }

  openEditModal() {
    // Create a shallow copy of user data to allow editing
    this.editData = { ...this.userdata };
  }

  submitEdit() {
    console.log("update profile")
    console.log(this.editData)
    this.profileservice.updateUserProfile(this.editData).subscribe({
      next: (data: any) => {
        console.log(data);
        this.userdata = data;
        this.profileservice.setcustomername(data.name);
        const modalEl = document.getElementById('editProfileModal');
        if (modalEl) {
          const modal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
          modal.hide();
        }
      },
      error: (err: any) => {
        alert(err.error);
        console.log(err);
      }
    })
    // this.profileservice.(this.editData).subscribe({
    //   next: (updated: any) => {
    //     this.userdata = updated;
    //     this.profileservice.setcustomername(updated.name);
    //     // Close modal manually
    //     const modalEl = document.getElementById('editProfileModal');
    //     if (modalEl) {
    //       const modal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
    //       modal.hide();
    //     }
    //   },
    //   error: (err: any) => {
    //     console.error('Update failed:', err);
    //   }
    // });
  }
}
