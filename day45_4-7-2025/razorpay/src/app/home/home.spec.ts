import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Home } from './home';  // adjust path as needed

describe('Home Component', () => {
  let component: Home;
  let fixture: ComponentFixture<Home>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule,Home],
    }).compileComponents();

    fixture = TestBed.createComponent(Home);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should mark fields as invalid if empty', () => {
    component.paymentForm.setValue({
      name: '',
      email: '',
      contact: '',
      amount: ''
    });
    expect(component.paymentForm.valid).toBeFalse();
  });

  it('should mark contact invalid if not 10-digit Indian number', () => {
    component.paymentForm.get('contact')?.setValue('12345');
    expect(component.paymentForm.get('contact')?.valid).toBeFalse();
  });


  it('should validate form with correct data', () => {
    component.paymentForm.setValue({
      name: 'John Doe',
      email: 'john@example.com',
      contact: '9876543210',
      amount: 200
    });
    expect(component.paymentForm.valid).toBeTrue();
  });


it('should open Razorpay when form is valid', () => {
  const openSpy = jasmine.createSpy('open');
  (window as any).Razorpay = function () {
    return {
      open: openSpy
    };
  };

  component.paymentForm.setValue({
    name: 'Jane',
    email: 'jane@example.com',
    contact: '9999999999',
    amount: 300
  });

  component.pay();

  expect(openSpy).toHaveBeenCalled();
});

});
