import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { CheckoutService } from 'src/app/checkout/checkout.service';
import { IOrderFromApi } from 'src/app/shared/models/order';
import { IOrderTotals } from 'src/app/shared/models/order-totals';

import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {
  order: IOrderFromApi;
  orderTotals: IOrderTotals;

  constructor(private breadcrumbService: BreadcrumbService, private activateRoute: ActivatedRoute,
              private checkoutService: CheckoutService) {
      this.breadcrumbService.set('@orderId', ' ' );
  }

  ngOnInit(): void {
    const id = +this.activateRoute.snapshot.paramMap.get('id');
    this.checkoutService.getOrder(id)
      .subscribe(order => {
        this.order = order;
        this.orderTotals = { shipping: order.shippingPrice, subTotal: order.subTotal };
        this.breadcrumbService.set('@orderId', `Order #${ order.id } - ${ order.status }` );
      }, err => console.log(err));
  }

}
