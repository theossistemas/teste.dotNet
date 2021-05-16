import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-chart-radar',
  templateUrl: './chart-radar.component.html',
  styleUrls: ['./chart-radar.component.css']
})
export class ChartRadarComponent implements OnInit {

  @Input() data: any;
  @Input() options: any;

  constructor() { }

  ngOnInit(): void {
  }

}
