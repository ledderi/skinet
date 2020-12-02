import { Component, OnInit, Input } from '@angular/core';

import { IQueryResult } from '../../models/queryResult';

@Component({
  selector: 'app-pagination-header',
  templateUrl: './pagination-header.component.html',
  styleUrls: ['./pagination-header.component.scss']
})
export class PaginationHeaderComponent implements OnInit {
  @Input() pagingResult: IQueryResult<object>;

  constructor() { }

  ngOnInit(): void {
  }

}
