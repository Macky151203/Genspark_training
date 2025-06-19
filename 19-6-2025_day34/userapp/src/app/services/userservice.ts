import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { User } from '../models/usermodel';

@Injectable({
  providedIn: 'root',
})
export class Userservice {
  private usersSubject = new BehaviorSubject<User[]>([]);
  users$ = this.usersSubject.asObservable();

  constructor() {
    const initialUsers: User[] = [
      {
        username: 'Alice',
        email: 'alice@example.com',
        password: 'alice123',
        role: 'admin',
      },
      {
        username: 'Bob',
        email: 'bob@example.com',
        password: 'bob123',
        role: 'user',
      },
      {
        username: 'Charlie',
        email: 'charlie@example.com',
        password: 'charlie123',
        role: 'user',
      },
    ];
    this.usersSubject.next(initialUsers);
  }

  addUser(user: User) {
    const oldusers = this.usersSubject.getValue();
    this.usersSubject.next([...oldusers, user]);
  }

  searchUserByName(searchterm: string) {
    const allusers = this.usersSubject.getValue();
    const filteredusers = allusers.filter((user) =>
      user.username.toLowerCase().includes(searchterm.toLowerCase())
    );
    return of(filteredusers);
  }
}
