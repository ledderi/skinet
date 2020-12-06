import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { IProduct } from 'src/app/shared/models/product';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error = (): void => {
    this.http.get(`${environment.apiUrl}/buggy/notfound`, { responseType: 'json' })
      .pipe(map(response => response))
      .subscribe(product => {
        console.log(product);
      }, err => {
        console.log(err);
      });
  }

  get500Error = (): void => {
    this.http.get(`${environment.apiUrl}/buggy/servererror`, { responseType: 'json' })
      .pipe(map(response => response))
      .subscribe(product => {
        console.log(product);
      }, err => {
        console.log(err);
      });
  }

  get400Error = (): void => {
    this.http.get(`${environment.apiUrl}/buggy/badrequest`, { responseType: 'json' })
      .pipe(map(response => response))
      .subscribe(product => {
        console.log(product);
      }, err => {
        console.log(err);
      });
  }

  get400ValidationError = (): void => {
    this.http.get<IProduct>(`${environment.apiUrl}/buggy/badrequest/abc`, { responseType: 'json' })
      .pipe(map(response => response))
      .subscribe(product => {
        console.log(product);
      }, err => {
        console.log(err);
        this.validationErrors = err.errors;
      });
  }

}
