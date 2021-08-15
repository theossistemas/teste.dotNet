import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { Label, MultiDataSet } from 'ng2-charts';

@Component({
  selector: 'app-grafico-rosca',
  templateUrl: './grafico-rosca.component.html',
  styleUrls: ['./grafico-rosca.component.css']
})
export class GraficoRoscaComponent implements OnInit {
 // Doughnut
 public doughnutChartLabels: Label[] = ['Exemplo 1', 'Exemplo 2', 'Exemplo 3'];
 public doughnutChartData: MultiDataSet = [
   [350, 450, 100],
   [50, 150, 120],
   [250, 130, 70],
 ];
 public doughnutChartType: ChartType = 'doughnut';

 constructor() { }

 ngOnInit() {
 }

 // events
 public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
   console.log(event, active);
 }

 public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
   console.log(event, active);
 }

}