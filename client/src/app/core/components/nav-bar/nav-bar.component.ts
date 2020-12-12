import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  cartSize$: Observable<number>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.cartSize$ = this.basketService.cartSize$;
  }

}
