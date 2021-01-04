import { Component, Input, OnInit } from '@angular/core';

import { CdkStepper } from '@angular/cdk/stepper';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class StepperComponent extends CdkStepper implements OnInit {
  @Input() linearModeSelected = false;

  ngOnInit(): void {
    this.linear = this.linearModeSelected;
  }

  selectStepByIndex(index: number): void {
    this.selectedIndex = index;
  }

}
