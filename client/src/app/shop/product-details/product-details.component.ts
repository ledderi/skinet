import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BreadcrumbService } from 'xng-breadcrumb';

import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(private activateRoute: ActivatedRoute, private shopService: ShopService,
              private breadcrumbService: BreadcrumbService, private basketService: BasketService) {
    this.breadcrumbService.set('@productName', ' ' );
  }

  ngOnInit(): void {
    const id = +this.activateRoute.snapshot.paramMap.get('id');

    this.shopService.getProduct(id).subscribe(product => {
      this.breadcrumbService.set('@productName', product.name );
      this.product = product;
    }, err => console.log(err));
  }

  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  addToCart(): void {
    this.basketService.addToCart(this.product, this.quantity);
  }

}
