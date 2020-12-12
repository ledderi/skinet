import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;

  constructor(private router: Router, private basketService: BasketService) { }

  ngOnInit(): void {
  }

  view(id: number): void {
    this.router.navigate(['/shop', id]);
  }

  addToCart(): void {
    this.basketService.addToCart(this.product, 1);
  }

}
