import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';

import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketTotals$: Observable<IBasketTotals>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotals$;
  }

  decrementProductQuantity(item: IBasketItem): void {
    if (item.quantity > 0) {
      item.quantity -= 1;
      this.setBasket(item);
    }
  }

  incrementProductQuantity(item: IBasketItem): void {
    item.quantity += 1;
    this.setBasket(item);
  }

  deleteProduct(item: IBasketItem): void {
    this.basketService.removeFromCart(item.productId);
  }

  private setBasket(item: IBasketItem): void {
    if (item.quantity === 0) {
      this.basketService.removeFromCart(item.productId);
    } else {
      this.basketService.updateCart(item.productId, item.quantity);
    }
  }

}
