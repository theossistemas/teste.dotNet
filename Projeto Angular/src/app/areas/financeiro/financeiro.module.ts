import { NgModule, LOCALE_ID } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinanceiroComponent } from './financeiro.component';
import { TableMaterialModule } from 'src/app/helpers/modules/table-material.module';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatSelectModule, MatDividerModule, MatProgressSpinnerModule, MAT_DATE_LOCALE } from '@angular/material';
import { ArquivosRoutingModule } from '../arquivos/arquivos.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';
import { ConfirmDialogModule } from 'src/app/shared/components/confirm-dialog/confirm-dialog.module';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { FinanceiroRoutingModule } from './financeiro.routing';



@NgModule({
  declarations: [FinanceiroComponent],
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
    FinanceiroRoutingModule,
    FormsModule,
    ReactiveFormsModule,    
    BlockUIModule,
    ConfirmDialogModule
  ],
  entryComponents: [
    ConfirmDialogComponent
  ],
  
  
})
export class FinanceiroModule { }
