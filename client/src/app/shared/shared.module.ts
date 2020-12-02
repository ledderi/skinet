import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { PaginationModule } from 'ngx-bootstrap/pagination';

import { PaginationComponent } from './components/pagination/pagination.component';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';

@NgModule({
  declarations: [
    PaginationComponent,
    PaginationHeaderComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    PaginationModule.forRoot()
  ],
  exports: [
    CommonModule,
    PaginationModule,
    PaginationComponent,
    PaginationHeaderComponent
  ]
})
export class SharedModule { }
