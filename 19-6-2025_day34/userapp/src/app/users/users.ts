import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  Validators,
  FormGroup,
  FormsModule,
} from '@angular/forms';
import { User } from '../models/usermodel';
import { Userservice } from '../services/userservice';
import { debounceTime, distinctUntilChanged, Subject, switchMap } from 'rxjs';

import { CustomValidators } from '../validators/customvalidators';

@Component({
  selector: 'app-users',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './users.html',
  styleUrl: './users.css',
})
export class Users implements OnInit {
  form: FormGroup;
  users = signal<User[]>([]);
  searchTerm = '';
  searchSubject = new Subject<string>();

  constructor(private userService: Userservice, private fb: FormBuilder) {
    this.form = this.fb.group(
      {
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20),
            CustomValidators.bannedWords(['admin', 'root']),
          ],
        ],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [Validators.required, CustomValidators.passwordStrength()],
        ],
        confirmPassword: ['', Validators.required],
        role: ['', Validators.required],
      },
      {
        validators: CustomValidators.matchPassword(
          'password',
          'confirmPassword'
        ),
      }
    );

    this.userService.users$.subscribe((users) => this.users.set(users));
  }
  ngOnInit(): void {
    this.searchSubject
      .pipe(
        debounceTime(2000),
        distinctUntilChanged(),
        switchMap(() => this.userService.searchUserByName(this.searchTerm))
      )
      .subscribe((data: any) => {
        this.users.set(data);
      });
  }

  addUser() {
    console.log('add user');
    if (this.form.invalid) {
      console.log('invalid inputs');
      return;
    }
    const { confirmPassword, ...user } = this.form.value;
    this.userService.addUser(user);
    this.form.reset();
  }

  searchuser() {
    this.searchSubject.next(this.searchTerm);
  }
}
