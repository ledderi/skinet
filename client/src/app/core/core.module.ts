import { NgModule } from '@angular/core';

import { ToastrModule } from 'ngx-toastr';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { SharedModule } from '../shared/shared.module';
import { TestErrorComponent } from './components/test-error/test-error.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { SectionHeaderComponent } from './components/section-header/section-header.component';

@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SectionHeaderComponent
  ],
  imports: [
    SharedModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right', preventDuplicates: true }),
    BreadcrumbModule,
    BsDropdownModule.forRoot()
  ],
  exports: [
    NavBarComponent,
    SectionHeaderComponent
  ]
})
export class CoreModule { }
