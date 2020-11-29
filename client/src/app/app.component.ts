import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { IProduct } from './models/product';
import { IQueryResult } from './models/queryResult';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'skinet';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get<IQueryResult<IProduct>>(`${environment.apiUrl}/products?pagesize=50`)
      .pipe(map( response => response))
      .subscribe(
          (queryResult: IQueryResult<IProduct>) => this.products = queryResult.data,
        err => console.log(err));
  }
}
