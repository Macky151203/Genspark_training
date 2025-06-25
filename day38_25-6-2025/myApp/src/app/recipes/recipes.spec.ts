import { TestBed } from '@angular/core/testing';
import { RecipeService } from '../services/recipeservice';
import { HttpClient } from '@angular/common/http';
import { of, throwError } from 'rxjs';

describe('RecipeService', () => {
  let service: RecipeService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  const mockResponse = {
    recipes: [
      {
        id: 1,
        name: 'Biryani',
        cuisine: 'Indian',
        cookTimeMinutes: 60,
        ingredients: ['Rice', 'Chicken', 'Spices'],
        image: 'https://example.com/biryani.jpg'
      }
    ]
  };

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);

    TestBed.configureTestingModule({
      providers: [
        RecipeService,
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });

    service = TestBed.inject(RecipeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getAllRecipes and return data', (done) => {
    httpClientSpy.get.and.returnValue(of(mockResponse));

    service.getAllRecipes().subscribe((data:any) => {
      expect(data).toEqual(mockResponse);
      expect(httpClientSpy.get).toHaveBeenCalledWith('https://dummyjson.com/recipes');
      done();
    });
  });

  it('should handle error from getAllRecipes', (done) => {
    const error = new Error('Network error');
    httpClientSpy.get.and.returnValue(throwError(() => error));

    service.getAllRecipes().subscribe({
      next: () => {},
      error: (err:any) => {
        expect(err).toBe(error);
        done();
      }
    });
  });
});
