import { ConfirmDialogComponent } from './../../shared/components/confirm-dialog/confirm-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArquivosComponent } from './arquivos.component';
import { TableMaterialModule } from 'src/app/helpers/modules/table-material.module';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatSelectModule, MatDividerModule, MatProgressSpinnerModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';
import { ArquivosRoutingModule } from './arquivos.routing';
import { ConfirmDialogModule } from 'src/app/shared/components/confirm-dialog/confirm-dialog.module';



@NgModule({
  declarations: [ArquivosComponent],
  imports: [
    CommonModule,
    TableMaterialModule,    
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatDividerModule,
    MatProgressSpinnerModule, 
    ArquivosRoutingModule,
    FormsModule,
    ReactiveFormsModule,    
    BlockUIModule,
    ConfirmDialogModule
  ],
  entryComponents: [
    ConfirmDialogComponent
  ]
})
export class ArquivosModule { }
