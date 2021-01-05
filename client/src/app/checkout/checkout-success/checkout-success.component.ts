import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.scss']
})
export class CheckoutSuccessComponent implements OnInit {
  viewMessage = 'View your order';

  private orderId: number;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation && navigation.extras && navigation.extras.state;

    if (state) {
      this.orderId = state.orderId;
      console.log(this.orderId);
    } else {
      this.viewMessage = 'View your orders';
    }
  }

  ngOnInit(): void {
  }

  viewOrder(): void {
    const url = this.orderId ? `orders/${this.orderId}` : 'orders' ;
    this.router.navigateByUrl(url);
  }

}
