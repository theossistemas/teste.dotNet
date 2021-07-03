import { Component, OnInit, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit, OnChanges {

  @Input() text: string;
  @Input() show = false;

  constructor() { }

  ngOnInit() {
  }

  ngOnChanges() {
    if (this.show) {
      document.querySelector('body').style.overflow = 'hidden';
    } else if (!this.show) {
      document.querySelector('body').style.overflow = '';
    }
  }

}
