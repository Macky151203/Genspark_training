import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  paymentForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.paymentForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      contact: ['', [Validators.required, Validators.pattern(/^[6-9]\d{9}$/)]],
      amount: ['', [Validators.required, Validators.min(1)]]
    });
  }

  pay() {
    if (this.paymentForm.invalid) {
      this.paymentForm.markAllAsTouched();
      return;
    }

    const form = this.paymentForm.value;

    const options: any = {
      key: 'rzp_test_I1bHELIBcoH8Ph',
      amount: form.amount * 100, 
      currency: 'INR',
      name: form.name,
      description: 'Custom Payment',
      image: 'https://your-logo-url.com/logo.png',
      handler: function (response: any) {
        alert('Payment successful! Payment ID: ' + response.razorpay_payment_id);
      },
      prefill: {
        name: form.name,
        email: form.email,
        contact: form.contact
      },
      notes: {
        purpose: 'User-Entered Payment'
      },
      theme: {
        color: '#007bff'
      }
    };

    const rzp = new (window as any).Razorpay(options);
    rzp.open();
  }
}
