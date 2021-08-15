import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RelatorioComponent } from './relatorio.component';
import { TableMaterialModule } from 'src/app/helpers/modules/table-material.module';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatSelectModule, MatDividerModule, MatProgressSpinnerModule } from '@angular/material';
import { RelatorioRoutingModule } from './relatorio.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';
import { ChartsModule } from 'ng2-charts';
import { GraficoPizzaComponent } from './grafico-pizza/grafico-pizza.component';
import { GraficoBarrasComponent } from './grafico-barras/grafico-barras.component';
import { GraficoRoscaComponent } from './grafico-rosca/grafico-rosca.component';

@NgModule({
  declarations: [RelatorioComponent, GraficoPizzaComponent, GraficoBarrasComponent, GraficoRoscaComponent],
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
    RelatorioRoutingModule,
    FormsModule,
    ReactiveFormsModule,    
    BlockUIModule,
    ChartsModule
  ]
})
export class RelatorioModule { }
