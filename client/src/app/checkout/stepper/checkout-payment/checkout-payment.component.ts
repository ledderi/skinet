import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { BasketService } from 'src/app/basket/basket.service';
import { IOrderToApi } from 'src/app/shared/models/order';
import { CheckoutService } from '../../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() form: FormGroup;

  constructor(private checkoutService: CheckoutService, private basketService: BasketService, private router: Router) { }

  ngOnInit(): void {
  }

  submitOrder(): void {
    const order: IOrderToApi = {
      basketId: localStorage.getItem('basket-id'),
      DeliveryAddress: this.form.get('deliveryAddress').value,
      deliveryMethodId: this.form.get('deliveryMethod').get('deliveryMethodId').value };

    this.checkoutService.createOrder(order)
      .subscribe(newOrder => {
        this.basketService.clearBasket();
        this.router.navigateByUrl(`checkout/success`, { state: { orderId: newOrder.id } });
    });
  }
}
