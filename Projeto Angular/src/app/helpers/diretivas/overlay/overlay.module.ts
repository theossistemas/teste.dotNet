import { InjectionService } from '../../../shared/services/injection.service';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OverlayComponent } from './overlay.component';
import { OverlayService } from './overlay.service';


@NgModule({
  declarations: [OverlayComponent],
  providers: [InjectionService],
  exports: [OverlayComponent],
  imports: [CommonModule],
  entryComponents: [OverlayComponent]
})
export class OverlayModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: OverlayModule,
      providers: [OverlayService],
    };
  }}
