import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../services/profile-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {
  userdata: any = {};
  ticketCount: number = 0;

  constructor(private profileservice: ProfileService) {}

  ngOnInit(): void {
    const email = localStorage.getItem('username');
    if (email) {
      this.profileservice.getUserProfile().subscribe({
        next: (data:any) => {
          this.userdata = data;
          this.ticketCount = data.tickets?.length || 0;
          this.profileservice.setcustomername(data.name)
        },
        error: (err:any) => {
          console.error('Failed to load profile:', err);
        }
      });
    }
  }
}
