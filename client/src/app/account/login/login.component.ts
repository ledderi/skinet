import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { EmailExistValidator } from 'src/app/shared/validators/email-exist.validator';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loginFailed = false;
  errorMessage = '';

  private returnUrl = '';

  constructor(private accountService: AccountService, private router: Router, private emailAsyncValidator: EmailExistValidator,
              private activatedRoute: ActivatedRoute) { }

  get email(): AbstractControl {
    return this.form.get('email');
  }

  get password(): AbstractControl {
    return this.form.get('password');
  }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', {
        validators: [Validators.required, Validators.email],
        // asyncValidators: [this.emailAsyncValidator.validate.bind(this)],
        // updateOn: 'blur'
      }),
      password: new FormControl('', [Validators.required]),
    });

    this.returnUrl = this.activatedRoute.snapshot.queryParamMap.get('returnUrl') || '/shop';
  }

  login(): void {
    this.loginFailed = false;

    this.accountService.login(this.form.value)
      .subscribe(_ => this.router.navigateByUrl(this.returnUrl),
        err => {
          this.loginFailed = true;
          this.errorMessage = err.status === 401 ? err.error.message : 'error occured';
        }
      );
  }
}
