import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { PaginationModule } from 'ngx-bootstrap/pagination';

import { PaginationComponent } from './components/pagination/pagination.component';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { RouterModule } from '@angular/router';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [
    PaginationComponent,
    PaginationHeaderComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    PaginationModule.forRoot()
  ],
  exports: [
    CommonModule,
    RouterModule,
    PaginationModule,
    PaginationComponent,
    PaginationHeaderComponent,
    TextInputComponent
  ]
})
export class SharedModule { }
