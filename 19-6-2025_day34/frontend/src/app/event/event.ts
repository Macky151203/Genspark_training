import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventpageService } from '../services/eventpage-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-event',
  imports: [CommonModule],
  templateUrl: './event.html',
  styleUrl: './event.css'
})
export class Event implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router, private eventservice: EventpageService) { }
  id: string = "";
  event: any = {}
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.eventservice.geteventbyid(Number(this.id)).subscribe((data) => {
      this.event = data;
      console.log(data);
    });

  }

  // event = {
  //   title: 'Vikram',
  //   description: 'Kamal movie',
  //   date: '2025-11-10T20:31:39.356Z',
  //   price: 500,
  //   address: 'Grand Mall',
  //   city: 'Chennai',
  //   category: { name: 'Movie' },
  //   imageUrl: '' // optional
  // };

  count = 1;

  increment() {
    this.count = Math.min(this.count + 1, 10);
  }

  decrement() {
    this.count = Math.max(this.count - 1, 1);
  }

  bookNow() {
    this.eventservice.bookticket(this.event.title, this.count).subscribe({
      next: (response: Blob) => {
        const blob = new Blob([response], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `Ticket_${this.event.title}.pdf`;
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url); // Clean up
      },
      error: (err) => {
        console.error("Error downloading ticket:", err);
        alert("Ticket booking failed. Please try again.");
      }
    });
  }


}
