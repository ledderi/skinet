import { NgModule } from '@angular/core';

import { ToastrModule } from 'ngx-toastr';

import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { SharedModule } from '../shared/shared.module';
import { TestErrorComponent } from './components/test-error/test-error.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';

@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    SharedModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right', preventDuplicates: true })
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
