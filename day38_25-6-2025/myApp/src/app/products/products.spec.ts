// import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
// import { Recipe } from '../recipe/recipe';
// import { RecipeService } from '../services/recipeservice';
// import { of, throwError } from 'rxjs';
// import { RecipeModel } from '../models/Recipe';
// import { Component, Input } from '@angular/core';


// @Component({
//   selector: 'app-recipe',
//   standalone: true,
//   template: '<div></div>'
// })
// class MockRecipe {
//   @Input() recipe!: RecipeModel;
// }

// describe('Recipes Component', () => {
//   let component: Recipe;
//   let fixture: ComponentFixture<Recipe>;
//   let recipeServiceSpy: jasmine.SpyObj<RecipeService>;

//   const mockData = {
//     recipes: [
//       { id: 1, name: 'Biryani', cuisine: 'Indian', cookTimeMinutes: 45, ingredients: [], image: '' }
//     ]
//   };

//   beforeEach(waitForAsync(() => {
//     const spy = jasmine.createSpyObj('RecipeService', ['getAllRecipes']);

//     TestBed.configureTestingModule({
//       declarations: [Recipe],
//       imports: [MockRecipe],
//       providers: [{ provide: RecipeService, useValue: spy }]
//     }).compileComponents();

//     fixture = TestBed.createComponent(Recipe);
//     component = fixture.componentInstance;
//     recipeServiceSpy = TestBed.inject(RecipeService) as jasmine.SpyObj<RecipeService>;
//   }));

//   it('should fetch and set recipes on init', () => {
//     recipeServiceSpy.getAllRecipes.and.returnValue(of(mockData));

//     fixture.detectChanges(); 

//     expect(component).toBeTruthy();
//     // expect(component['recipes']).toEqual(mockData.recipes);
//     expect(recipeServiceSpy.getAllRecipes).toHaveBeenCalled();
//   });

//   it('should handle error on fetch failure', () => {
//     const consoleSpy = spyOn(console, 'error');
//     recipeServiceSpy.getAllRecipes.and.returnValue(throwError(() => new Error('Error fetching')));

//     fixture.detectChanges();

//     expect(consoleSpy).toHaveBeenCalledWith(jasmine.any(Error));
//   });
// });
