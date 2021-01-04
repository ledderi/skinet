import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

import { BasketService } from 'src/app/basket/basket.service';
import { IDeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { CheckoutService } from '../../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
  deliveryMethods$: Observable<IDeliveryMethod[]>;

  @Input() form: FormGroup;

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.deliveryMethods$ = this.checkoutService.getDeliveryMethods();
  }

  updateShipping(deliveryMethod: IDeliveryMethod): void {
    this.basketService.setShippingAndHandling(deliveryMethod);
  }

}
