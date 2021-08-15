import { MesPipe } from './mes.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [MesPipe],
    exports: [MesPipe]
})
export class MesPipeModule { }
