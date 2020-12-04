import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';

import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private activateRoute: ActivatedRoute, private shopService: ShopService) { }

  ngOnInit(): void {
    const id = +this.activateRoute.snapshot.paramMap.get('id');

    this.shopService.getProduct(id).subscribe(product => {
      console.log(product);
      this.product = product;
    }, err => console.log(err));
  }

}
