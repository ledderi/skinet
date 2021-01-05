import { Component, Input, OnInit } from '@angular/core';

import { IOrderTotals } from '../../models/order-totals';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
  @Input() showCheckOutButton = false;
  @Input() showMessage = false;
  @Input() totals: IOrderTotals;

  constructor() { }

  ngOnInit(): void {
  }

}
