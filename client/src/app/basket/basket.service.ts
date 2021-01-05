import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { Basket, IBasket, IBasketItem } from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IOrderTotals } from '../shared/models/order-totals';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private cartSize: BehaviorSubject<number> = new BehaviorSubject(0);
  cartSize$: Observable<number> = this.cartSize.asObservable();

  private basketSource: BehaviorSubject<IBasket> = new BehaviorSubject(null);
  basket$: Observable<Basket> = this.basketSource.asObservable();

  private basketTotalsSource: BehaviorSubject<IOrderTotals> = new BehaviorSubject(null);
  basketTotals$: Observable<IOrderTotals> = this.basketTotalsSource.asObservable();

  private apiUrl = `${environment.apiUrl}/basket`;

  constructor(private http: HttpClient) { }

  getBasket = (): void => {
    const basketId = localStorage.getItem('basket-id');

    if (basketId) {
      let params: HttpParams = new HttpParams();
      params = params.append('basketid', basketId);
      this.http.get<Basket>(this.apiUrl, { params })
        .pipe(map(basket => {
          this.basketSource.next(basket);
          this.cartSize.next(basket.items.length);
          this.basketTotalsSource.next(this.calculateTotalBasket());
          return basket;
        }, err => console.log(err))
      ).subscribe(basket => console.log(basket));
    }
  }

  addToCart = (product: IProduct, quantity: number): void => {
    let basket = this.getBasketValue();

    if (!basket) {
      basket = new Basket();
      localStorage.setItem('basket-id', basket.id );
    }

    let item = basket.items.find(p => p.productId === product.id);
    if (item) {
      item.quantity += quantity;
    } else {
      item = this.mapProductToBasketItem(product, quantity);
      basket.items.push(item);
    }

    this.setBasket(basket);
  }

  updateCart = (productId: number, quantity: number): void => {
    const basket = this.getBasketValue();
    const basketItem = basket.items.find(p => p.productId === productId);
    basketItem.quantity = quantity;

    this.setBasket(basket);
  }

  removeFromCart = (productId: number): void => {
    const basket = this.getBasketValue();

    const itemIndex = basket.items.findIndex(p => p.productId === productId);
    basket.items.splice(itemIndex, 1);

    if (basket.items.length === 0) {
      this.deleteCart();
    } else {
      this.setBasket(basket);
    }
  }

  setShippingAndHandling = (deliveryMethod: IDeliveryMethod): void => {
    const basket = this.getBasketValue();
    basket.deliveryMethodId = deliveryMethod.id;
    basket.shippingAndHandling = deliveryMethod.price;
    this.setBasket(basket);
  }

  getBasketValue(): Basket {
    return this.basketSource.getValue();
  }

  clearBasket = (): void => {
    localStorage.removeItem('basket-id');
    this.basketSource.next(null);
    this.basketTotalsSource.next(null);
    this.cartSize.next(0);
  }

  private setBasket = (basket: IBasket): void => {
    this.http.post<Basket>(this.apiUrl, basket)
      .subscribe(
        newBasket => {
          this.basketSource.next(newBasket);
          const cartSize = newBasket === null ? 0 : newBasket.items.length;
          this.cartSize.next(cartSize);
          this.basketTotalsSource.next(newBasket === null ? null :  this.calculateTotalBasket());
        },
        err => console.log(err));
  }

  private deleteCart = (): void => {
    const basketId = localStorage.getItem('basket-id');

    this.http.delete<boolean>(`${this.apiUrl}/${basketId}`)
      .pipe(map(deleted => {
        localStorage.removeItem('basket-id');
        this.basketSource.next(null);
        this.basketTotalsSource.next(null);
        this.cartSize.next(0);
      }, err => console.log(err))
    ).subscribe();
  }

  private mapProductToBasketItem(product: IProduct, quantity: number): IBasketItem {
    return { productId: product.id,
      productName: product.name,
      price: product.price,
      quantity,
      brand: product.productBrand,
      type: product.productType, pictureUrl: product.pictureUrl};
  }

  private calculateTotalBasket(): IOrderTotals {
    const basket = this.getBasketValue();
    const subTotal = basket.items.reduce((a, b) => a + (b.quantity * b.price), 0);
    const shipping = basket.shippingAndHandling;

    return { shipping, subTotal };
  }
}
