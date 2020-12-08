import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;

  constructor(private router: Router, private bc: BreadcrumbService) { }

  ngOnInit(): void {
  }

  view(id: number): void {
    this.router.navigate(['/shop', id]);
  }

}
