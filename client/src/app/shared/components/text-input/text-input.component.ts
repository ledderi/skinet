import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('input') input: ElementRef;
  @Input() inputType = 'text';
  @Input() label = '';

  constructor(@Self() public controlDir: NgControl ) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];
    const asynValidators = control.asyncValidator ? [control.asyncValidator] : [];

    control.setValidators(validators);
    control.setAsyncValidators(asynValidators);
    control.updateValueAndValidity();
  }

  onChange: any = (event) => {};
  onTouched: any = () => {};

  writeValue(obj: any): void {
    if (this.input) {
      this.input.nativeElement.value = obj || '';
    }
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    throw new Error('Method not implemented.');
  }

  isInValid(): boolean {
    return this.controlDir && this.controlDir.control && this.controlDir.control.touched && this.controlDir.control.invalid
      && (this.controlDir.control.status !== 'PENDING');
  }

  isValid(): boolean {
    return this.controlDir && this.controlDir.control && this.controlDir.control.valid && (this.controlDir.control.status !== 'PENDING');
  }

  isPending(): boolean {
    return this.controlDir && this.controlDir.control && (this.controlDir.control.status === 'PENDING');
  }

}
