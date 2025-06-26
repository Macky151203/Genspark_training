import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConfirmationService } from '../services/confirmation-service';
import { EventService } from '../services/event-service';

@Component({
  selector: 'app-event',
  imports: [CommonModule],
  templateUrl: './event.html',
  styleUrl: './event.css'
})
export class Event implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router,private confirmationService: ConfirmationService,private eventservice: EventService) { }
  id: string = "";
  event: any = {}
  similarEvents: any[] = [];
  ngOnInit(): void {
  this.route.params.subscribe(params => {
    this.id = params['id'];
    this.eventservice.geteventbyid(Number(this.id)).subscribe((data) => {
      this.event = data;
      this.fetchSimilarEvents();
      console.log(data);
    });
  });
}
  count = 1;

  fetchSimilarEvents() {
    this.eventservice.getallevents().subscribe((allEvents:any) => {
      console.log(allEvents)
      this.similarEvents = allEvents.filter(
        (e: any) => e.categoryId === this.event.categoryId && e.id !== this.event.id
      );
    });
  }

  increment() {
    this.count = Math.min(this.count + 1, 10);
  }

  decrement() {
    this.count = Math.max(this.count - 1, 1);
  }

  handleBookNow(id: number) {
    this.router.navigate([`/event/${id}`]);
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

  gotohome(){
    this.router.navigate(['/'])
  }


}
