import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { PaginationModule } from 'ngx-bootstrap/pagination';

import { PaginationComponent } from './components/pagination/pagination.component';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { RouterModule } from '@angular/router';
import { TextInputComponent } from './components/text-input/text-input.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { BasketSummeryComponent } from './components/basket-summery/basket-summery.component';

@NgModule({
  declarations: [
    PaginationComponent,
    PaginationHeaderComponent,
    TextInputComponent,
    OrderTotalsComponent,
    BasketSummeryComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    RouterModule,
    PaginationModule,
    PaginationComponent,
    PaginationHeaderComponent,
    TextInputComponent,
    OrderTotalsComponent,
    ReactiveFormsModule,
    BasketSummeryComponent
  ]
})
export class SharedModule { }
