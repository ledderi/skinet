import { NgModule } from '@angular/core';

import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    NavBarComponent
  ],
  imports: [
    SharedModule
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
