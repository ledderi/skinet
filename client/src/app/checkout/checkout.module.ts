import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CdkStepperModule } from '@angular/cdk/stepper';

import { CheckoutRoutingModule } from './checkout-routing.module';
import { SharedModule } from '../shared/shared.module';

import { CheckoutComponent } from './checkout.component';
import { StepperComponent } from './stepper/stepper.component';
import { CheckoutAddressComponent } from './stepper/checkout-address/checkout-address.component';
import { CheckoutDeliveryComponent } from './stepper/checkout-delivery/checkout-delivery.component';
import { CheckoutPaymentComponent } from './stepper/checkout-payment/checkout-payment.component';
import { CheckoutReviewComponent } from './stepper/checkout-review/checkout-review.component';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';

@NgModule({
  declarations: [CheckoutComponent,
    StepperComponent,
    CheckoutAddressComponent,
    CheckoutDeliveryComponent,
    CheckoutPaymentComponent,
    CheckoutReviewComponent,
    CheckoutSuccessComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule,
    CdkStepperModule
  ],
  exports: [
    CheckoutComponent
  ]
})
export class CheckoutModule { }
