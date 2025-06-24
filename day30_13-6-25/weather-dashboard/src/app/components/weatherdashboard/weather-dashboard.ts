import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CitySearchComponent } from '../citysearch/city-search';
import { WeatherCardComponent } from '../weathercard/weather-card';
import { WeatherService } from '../../services/weather';

@Component({
  selector: 'app-weather-dashboard',
  standalone: true,
  imports: [CommonModule, CitySearchComponent, WeatherCardComponent],
  templateUrl: "./weather-dashboard.html",
  styleUrl: "./weather-dashboard.css"
})
export class WeatherDashboardComponent {

  constructor(public weatherService: WeatherService) {
  }

  
}
