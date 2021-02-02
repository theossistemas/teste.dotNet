import { NgModule } from '@angular/core';
import { LoaderComponent } from './loader.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [CommonModule, RouterModule],
    declarations: [LoaderComponent],
    exports: [LoaderComponent]
})
export class LoaderModule {}
