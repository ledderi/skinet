import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { forkJoin, Subscription } from 'rxjs';

import { IKeyValuePair } from '../shared/models/keyValuePair';
import { IProduct } from '../shared/models/product';
import { IQueryRequest } from '../shared/models/queryRequest';
import { IQueryResult } from '../shared/models/queryResult';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit, OnDestroy {
  @ViewChild('search') search: ElementRef;

  brands: IKeyValuePair[] = [];
  types: IKeyValuePair[] = [];
  products: IProduct[] = [];
  selectedBrand = 0;
  selectedType = 0;
  queryResult: IQueryResult<IProduct>;

  private query: IQueryRequest;
  private subscription: Subscription;

  constructor(private shopService: ShopService) {
    this.query = this.getQueryRequest();
  }

  ngOnInit(): void {

    const types$ = this.shopService.getTypes();
    const brands$ = this.shopService.getBrands();
    const products$ = this.shopService.getProducts(this.query);

    this.subscription = forkJoin([types$, brands$, products$ ]).subscribe(result => {
      this.types = [{ id: 0, name: 'All'}, ...result[0]];
      this.brands = [{ id: 0, name: 'All'}, ...result[1]];
      this.queryResult = result[2];
      this.products = result[2].data;
    }, err => {
      console.log(err);
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  sortSelected(sort: string): void {
    if (sort === '1') {
      this.query.orderBy = 'name';
      this.query.orderDirection = 'asc';
    } else {
      this.query.orderBy = 'price';
      this.query.orderDirection = (sort === '2') ? 'asc' : 'desc';
    }

    this.paiging();
  }

  selectBrand(brandId: number): void {
    this.selectedBrand = brandId;
    this.query.pageIndex = 1;
    this.query.productBrandId = brandId === 0 ? null : brandId;
    this.paiging();
  }

  selectType(typeId: number): void {
    this.selectedType = typeId;
    this.query.pageIndex = 1;
    this.query.productTypeId = typeId === 0 ? null : typeId;
    this.paiging();
  }

  pageChanged(page: any): void {
    if (this.query.pageIndex !== page.page) {
      this.query.pageIndex = page.page;
      this.paiging();
    }
  }

  searchProducts(): void {
    this.query.search = this.search.nativeElement.value;
    this.paiging();
  }

  clearSearch(): void {
    this.search.nativeElement.value = '';
    this.query.search = '';
    this.paiging();
  }

  private paiging(): void {
    this.shopService.getProducts(this.query).subscribe(result => {
      this.queryResult = result;
      this.products = result.data;
    }, err => console.log(err));
  }

  private getQueryRequest(): IQueryRequest {
    return { pageSize: 6, pageIndex: 1, orderBy: 'name', orderDirection: 'asc' };
  }

}
