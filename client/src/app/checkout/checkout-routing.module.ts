import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { CheckoutComponent } from './checkout.component';
import { IsUserAuthorized } from './user-authorized.guard';

const routes: Routes = [
  { path: '', component: CheckoutComponent, canActivate: [IsUserAuthorized] }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CheckoutRoutingModule { }
