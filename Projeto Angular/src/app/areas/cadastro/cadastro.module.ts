import { CriarEditarLivroComponent } from './livro/criar-editar-livro/criar-editar-livro.component';
import { VisualizarLivroComponent } from './livro/visualizar-livro/visualizar-livro.component';
import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatSelectModule, MatDividerModule, MatProgressSpinnerModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { BlockUIModule } from 'ng-block-ui';
import { InputMaskModule } from 'src/app/helpers/diretivas/input-mask/input-mask.module';
import { TableMaterialModule } from 'src/app/helpers/modules/table-material.module';

@NgModule({
    declarations: [
      VisualizarLivroComponent, 
      CriarEditarLivroComponent],
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
  export class CadastroModule { }