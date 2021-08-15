import { MatDialogModule, MatDialogRef } from '@angular/material';
import { ConfirmDialogComponent } from './confirm-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule,
        MatDialogModule       
    ],
    declarations: [ConfirmDialogComponent],
    exports: [ConfirmDialogComponent]
})
export class ConfirmDialogModule { }