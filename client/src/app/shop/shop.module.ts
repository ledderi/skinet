import { NgModule } from '@angular/core';

import { ShopComponent } from './shop.component';
import { SharedModule } from '../shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopRoutingModule } from './shop-routing.module';

@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    ProductDetailsComponent
  ],
  imports: [
    SharedModule,
    ShopRoutingModule
  ],
  exports: []
})
export class ShopModule { }
