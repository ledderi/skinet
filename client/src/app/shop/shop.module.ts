import { NgModule } from '@angular/core';

import { ShopComponent } from './shop.component';
import { SharedModule } from '../shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';

@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent
  ],
  imports: [
    SharedModule
  ],
  exports: [
    ShopComponent
  ]
})
export class ShopModule { }
