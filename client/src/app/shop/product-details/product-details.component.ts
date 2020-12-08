import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BreadcrumbService } from 'xng-breadcrumb';

import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private activateRoute: ActivatedRoute, private shopService: ShopService, private breadcrumbService: BreadcrumbService) {
    this.breadcrumbService.set('@productName', ' ' );
  }

  ngOnInit(): void {
    const id = +this.activateRoute.snapshot.paramMap.get('id');

    this.shopService.getProduct(id).subscribe(product => {
      this.breadcrumbService.set('@productName', product.name );
      this.product = product;
    }, err => console.log(err));
  }

}
