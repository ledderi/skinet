import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { IAddress } from '../shared/models/address';

import { ILogin } from '../shared/models/login';
import { IRegister } from '../shared/models/register';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource: ReplaySubject<IUser> = new ReplaySubject(1);
  currentUser$: Observable<IUser> = this.currentUserSource.asObservable();

  private apiUrl = `${environment.apiUrl}/account`;

  constructor(private http: HttpClient, private router: Router) { }

  getUserAccount = (): void => {
    const token = localStorage.getItem('token');

    if (token === null) {
      this.currentUserSource.next(null);
      return;
    }

    this.http.get<IUser>(this.apiUrl)
        .pipe(map(user => {
          this.currentUserSource.next(user);
        })
    ).subscribe();
  }

  login = (login: ILogin): Observable<IUser> => {
    return this.http.post<IUser>(`${this.apiUrl}/login`, login)
      .pipe(map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
        return user;
      }));
  }

  register = (register: IRegister): Observable<IUser> => {
    return this.http.post<IUser>(`${this.apiUrl}/register`, register)
      .pipe(map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
        return user;
      }));
  }

  logout = (): void => {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExist = (email: string): Observable<boolean> => {
    return this.http.get<boolean>(`${this.apiUrl}/emailexist?email=${email}`);
  }

  getUserAddress = (): Observable<IAddress> => {
    return this.http.get<IAddress>(`${this.apiUrl}/address`);
  }

  updateUserAddress = (address: IAddress): Observable<IAddress> => {
    return this.http.put<IAddress>(`${this.apiUrl}`, address);
  }
}
