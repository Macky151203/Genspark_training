import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HomepageService } from '../services/homepage-service';

@Component({
  selector: 'app-home',
  imports: [FormsModule, CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  searchText = '';
  filters = {
    minPrice: null as number | null,
    maxPrice: null as number | null,
    category: '',
    title: '',
    city: ''
  };

  categories : string[] = [];
  cities : string[] = [];

  latestEvents = [
    { title: 'Concert A', description: 'Music concert', category: 'Music', price: 500 },
    { title: 'Tech Talk B', description: 'Tech event', category: 'Tech', price: 200 },
    { title: 'Marathon C', description: 'Sports event', category: 'Sports', price: 100 },
  ];

  // allEvents = [...this.latestEvents, /* more dummy data here */];

  allEvents : any=[]

  constructor(private homepageService: HomepageService){}

  ngOnInit(){
    this.homepageService.getallevents().subscribe(
      {
        next:(data)=>{
          this.allEvents = data as any;
          this.categories = Array.from(new Set(this.allEvents.map((e:any) => e.category.name)));
          this.cities = Array.from(new Set(this.allEvents.map((e:any) => e.city)));
          console.log(this.categories)
          console.log(this.allEvents)
        },
        error:(err)=>{
          console.log(err)
        },
        complete:()=>{
          console.log("All done");
        }
      })
  }

  filteredEvents() {
    return this.allEvents.filter((e:any) => {
      const matchesMinPrice = this.filters.minPrice == null || e.price >= this.filters.minPrice;
      const matchesMaxPrice = this.filters.maxPrice == null || e.price <= this.filters.maxPrice;
      const matchesCategory = !this.filters.category || e.category.name === this.filters.category;
      const matchesName = !this.filters.title || e.title.toLowerCase().includes(this.filters.title.toLowerCase());
      const matchesSearch = !this.searchText || e.title.toLowerCase().includes(this.searchText.toLowerCase());
      const matchescity= !this.filters.city || e.city === this.filters.city;

      return matchesMinPrice && matchesMaxPrice && matchesCategory && matchesName && matchesSearch && matchescity;
    });
  }
}
