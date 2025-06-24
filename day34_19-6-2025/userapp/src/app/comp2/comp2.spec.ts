import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Comp2 } from './comp2';

describe('Comp2', () => {
  let component: Comp2;
  let fixture: ComponentFixture<Comp2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Comp2]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Comp2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
