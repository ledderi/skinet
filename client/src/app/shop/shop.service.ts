import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { IKeyValuePair } from '../shared/models/keyValuePair';
import { IProduct } from '../shared/models/product';
import { IQueryRequest } from '../shared/models/queryRequest';
import { IQueryResult } from '../shared/models/queryResult';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  private baseUrl = '';

  constructor(private http: HttpClient) {
    this.baseUrl = environment.apiUrl;
  }

  getProducts = (request: IQueryRequest): Observable<IQueryResult<IProduct>> => {
    let params: HttpParams = new HttpParams();
    params = params.append('pageIndex', request.pageIndex.toString());
    params = params.append('pageSize', request.pageSize.toString());
    params = params.append('orderBy', request.orderBy);
    params = params.append('orderDirection', request.orderDirection);
    if (request.productBrandId) {
      params = params.append('productBrandId', request.productBrandId.toString());
    }
    if (request.productTypeId) {
      params = params.append('productTypeId', request.productTypeId.toString());
    }
    if (request.search) {
      params = params.append('search', request.search);
    }

    return this.http.get<IQueryResult<IProduct>>(`${this.baseUrl}/products`, { params });
  }

  getProduct = (id: number): Observable<IProduct> => {
    return this.http.get<IProduct>(`${this.baseUrl}/products/${id}`);
  }

  getBrands = (): Observable<IKeyValuePair[]> => {
    return this.http.get<IKeyValuePair[]>(`${this.baseUrl}/products/brands`);
  }

  getTypes = (): Observable<IKeyValuePair[]> => {
    return this.http.get<IKeyValuePair[]>(`${this.baseUrl}/products/types`);
  }
}
