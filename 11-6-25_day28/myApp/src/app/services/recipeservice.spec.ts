import { TestBed } from '@angular/core/testing';

import { Recipeservice } from './recipeservice';

describe('Recipeservice', () => {
  let service: Recipeservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Recipeservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
