import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule, LOCALE_ID } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PessoaComponent } from './pessoa.component';
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatDatepickerModule, MatSelectModule, MatDividerModule, MatProgressSpinnerModule, MatNativeDateModule, MAT_DATE_LOCALE, DateAdapter } from '@angular/material';
import { RouterModule } from '@angular/router';
import { TableMaterialModule } from 'src/app/helpers/modules/table-material.module';
import { BrDateAdapter } from 'src/app/helpers/date-picker/br-date-adapter';
import { BlockUIModule } from 'ng-block-ui';
import { InputMaskModule } from 'src/app/helpers/diretivas/input-mask/input-mask.module';


@NgModule({
  declarations: [
    PessoaComponent
    ],
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
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    BlockUIModule,    
    MatNativeDateModule,
    InputMaskModule
  ]  
})
export class PessoaModule { }
