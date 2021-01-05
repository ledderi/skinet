import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IOrderFromApi, IOrderToApi } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = `${environment.apiUrl}/orders`;

  constructor(private http: HttpClient) { }

  getDeliveryMethods = (): Observable<IDeliveryMethod[]> => {
    return this.http.get<IDeliveryMethod[]>(`${this.baseUrl}/deliveryMethods`)
      .pipe(map(dms => {
        const sorted = dms.sort((x, y) => x.price - y.price);
        return sorted;
      }));
  }

  createOrder = (newOrder: IOrderToApi): Observable<IOrderFromApi> => {
    return this.http.post<IOrderFromApi>(`${this.baseUrl}`, newOrder);
  }

  getOrders = (): Observable<IOrderFromApi[]> => {
    return this.http.get<IOrderFromApi[]>(this.baseUrl);
  }

  getOrder = (id: number): Observable<IOrderFromApi> => {
    return this.http.get<IOrderFromApi>(`${this.baseUrl}/${id}`);
  }
}
