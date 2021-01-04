import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';

// import { CdkStepper } from '@angular/cdk/stepper';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit, AfterViewInit {
  form: FormGroup;

  // @ViewChild('stepper') stepper: CdkStepper;

  constructor(private accountService: AccountService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.createForm();
    this.getDeliveryAddress();
    this.getDeliveryMethodId();
  }

  ngAfterViewInit(): void {
  }

  private createForm(): void {
    this.form = new FormGroup({
      deliveryAddress: new FormGroup({
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        street: new FormControl('', [Validators.required]),
        city: new FormControl('', [Validators.required]),
        state: new FormControl('', [Validators.required]),
        zipcode: new FormControl('', [Validators.required])
      }),
      deliveryMethod: new FormGroup({
        deliveryMethodId: new FormControl('', [Validators.required])
      })
    });
  }

  private getDeliveryMethodId(): void {
    const deliveryMethodId = this.basketService.getBasketValue().deliveryMethodId;
    if (deliveryMethodId > 0) {
      this.form.get('deliveryMethod').setValue({ deliveryMethodId });
    }
  }

  private getDeliveryAddress(): void {
    this.accountService.getUserAddress().subscribe(address => {
        this.form.get('deliveryAddress').patchValue(address);
      }, err => { console.log(err); } );
  }

}
