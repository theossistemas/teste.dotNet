import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatGridListModule, MatPaginatorModule, MatTableModule, MatSortModule, MatPaginatorIntl } from '@angular/material';
import { getPtBrPaginatorIntl } from 'src/app/helpers/paginator/pt-br-paginator-locale';

@NgModule({
  imports: [
    CommonModule,
    MatDialogModule,
    MatGridListModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule   
  ],
  exports: [
    MatDialogModule,
    MatGridListModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule 
  ],   
  providers: [
    { provide: MatPaginatorIntl, useValue: getPtBrPaginatorIntl() }    
  ]
})
export class TableMaterialModule { }
