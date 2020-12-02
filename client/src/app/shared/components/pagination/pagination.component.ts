import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { IQueryResult } from '../../models/queryResult';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() pagingResult: IQueryResult<object>;
  @Output() pageChanged: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onPageChanged(page: any): void {
    this.pageChanged.emit(page);
  }

}
