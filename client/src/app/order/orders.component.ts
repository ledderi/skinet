import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { CheckoutService } from '../checkout/checkout.service';
import { IOrderFromApi } from '../shared/models/order';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders$: Observable<IOrderFromApi[]>;

  constructor(private checkoutService: CheckoutService, private router: Router) { }

  ngOnInit(): void {
    this.orders$ = this.checkoutService.getOrders();
  }

  viewOrder(orderId: number): void {
    this.router.navigateByUrl(`orders/${orderId}`);
  }

}
