import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  cartSize$: Observable<number>;
  currentUser$: Observable<IUser>;

  constructor(private basketService: BasketService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.cartSize$ = this.basketService.cartSize$;
    this.currentUser$ = this.accountService.currentUser$;
  }

  logout(): void {
    this.accountService.logout();
  }

}
