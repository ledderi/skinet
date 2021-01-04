import { Component, Input, OnInit } from '@angular/core';

import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from '../../models/basket';

@Component({
  selector: 'app-basket-summery',
  templateUrl: './basket-summery.component.html',
  styleUrls: ['./basket-summery.component.scss']
})
export class BasketSummeryComponent implements OnInit {
  @Input() items: IBasketItem[] = [];
  @Input() showButtons = false;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
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
