import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  view(id: number): void {
    this.router.navigate(['/shop', id]);
  }

}
