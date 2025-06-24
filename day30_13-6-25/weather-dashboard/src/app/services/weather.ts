import { Injectable, signal, effect } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class WeatherService {
  private readonly API_KEY = 'f904f2dca05718110dfddcb7dcf6b553';
  private readonly BASE_URL = 'https://api.openweathermap.org/data/2.5/weather';

  // weather : any= ""
  weather = signal<any | null>(null);
  error = signal<string>('');

  constructor(private http: HttpClient) {
  }


  fetchWeather(city: string): void {
    const url = `${this.BASE_URL}?q=${city}&appid=${this.API_KEY}&units=metric`;
    this.http.get<any>(url).subscribe({
      next: data => {
        this.weather.set(data);
        this.error.set('');
      },
      error: err => {
        console.error('Error fetching weather:', err);
        this.weather.set(null);
        this.error.set('Error fetching data');
      }
    });
  }
}
