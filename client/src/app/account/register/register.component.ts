import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  loginFailed = false;
  errorMessage = '';

  constructor(private accountService: AccountService, private router: Router) { }

  get email(): AbstractControl {
    return this.form.get('email');
  }

  get password(): AbstractControl {
    return this.form.get('password');
  }

  ngOnInit(): void {
    this.form = new FormGroup({
      displayName: new FormControl('', [Validators.required]),
      email: new FormControl('', {
        validators: [Validators.required, Validators.email],
        asyncValidators: [this.emailExistValidator()],
        updateOn: 'blur'
      }) ,
      password: new FormControl('', [Validators.required])
    });
  }

  register(): void {
    this.loginFailed = false;

    this.accountService.register(this.form.value)
      .subscribe(
        _ => this.router.navigateByUrl('/shop'),
        err => {
          this.loginFailed = true;
          this.errorMessage = err.error.message;
        }
      );
  }

  emailExistValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors> => {
      return this.accountService.checkEmailExist(control.value)
        .pipe(map(isExist => isExist ? { emailExist: true } : null)
      );
    };
  }

}
