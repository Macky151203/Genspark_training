import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventpageService } from '../services/eventpage-service';
import { CommonModule } from '@angular/common';
import { ConfirmationService } from '../services/confirmation-service';

@Component({
  selector: 'app-event',
  imports: [CommonModule],
  templateUrl: './event.html',
  styleUrl: './event.css'
})
export class Event implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router, private eventservice: EventpageService,private confirmationService: ConfirmationService) { }
  id: string = "";
  event: any = {}
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.eventservice.geteventbyid(Number(this.id)).subscribe((data) => {
      this.event = data;
      console.log(data);
    });

  }



  count = 1;

  increment() {
    this.count = Math.min(this.count + 1, 10);
  }

  decrement() {
    this.count = Math.max(this.count - 1, 1);
  }

  bookNow() {
    // this.eventservice.bookticket(this.event.title, this.count).subscribe({
    //   next: (response: Blob) => {
    //     const blob = new Blob([response], { type: 'application/pdf' });
    //     const url = window.URL.createObjectURL(blob);
    //     const a = document.createElement('a');
    //     a.href = url;
    //     a.download = `Ticket_${this.event.title}.pdf`;
    //     document.body.appendChild(a);
    //     a.click();
    //     a.remove();
    //     window.URL.revokeObjectURL(url); // Clean up
    //   },
    //   error: (err) => {
    //     console.error("Error downloading ticket:", err);
    //     alert("Ticket booking failed. Please try again.");
    //   }
    // });

    //add qty and detail to confirmation service
    this.confirmationService.setBookingData(this.event, this.count);
    this.router.navigate([`/confirmbooking/${this.id}`])
  }


}
