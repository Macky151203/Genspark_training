import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Maincomp } from './maincomp';

describe('Maincomp', () => {
  let component: Maincomp;
  let fixture: ComponentFixture<Maincomp>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Maincomp]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Maincomp);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
