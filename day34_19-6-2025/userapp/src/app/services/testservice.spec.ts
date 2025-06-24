import { TestBed } from '@angular/core/testing';

import { Testservice } from './testservice';

describe('Testservice', () => {
  let service: Testservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Testservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
