import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputMaskDirective } from './input-mask.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [InputMaskDirective],
  exports: [InputMaskDirective]
})
export class InputMaskModule { }