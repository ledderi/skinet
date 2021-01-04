import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() form: FormGroup;

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  updateUserAddress(): void {
    this.accountService.updateUserAddress(this.form.value)
      .subscribe(_ => {
        this.toastr.success('default address updated successfuly');
      }, err => console.log(err));
  }

}
