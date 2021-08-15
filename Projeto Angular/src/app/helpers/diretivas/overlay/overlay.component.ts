import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'overlay',
  template: `
  <div
    (click)="click.emit(true)"
    [style.zIndex]="zIndex"
    [@overlayTransition]="animationState"
    class="overlay">
      <div class="svg">
        <ng-content></ng-content>
      </div>
  </div>
`,
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./overlay.component.css'],
  animations: [
    trigger('overlayTransition', [
      state(
        'active',
        style({
          opacity: 1,
          visibility: 'visible'
        })
      ),
      state(
        'inactive',
        style({
          opacity: 0,
          visibility: 'hidden'
        })
      ),
      transition('* => active', [animate('100ms ease-in')]),
      transition('* => inactive', [animate('100ms ease-out')]),
      transition('* => void', [
        style({
          opacity: 0,
          visibility: 'hidden',
          'pointer-events': 'none'
        }),
        animate('100ms ease-out')
      ])
    ])
  ]
})
export class OverlayComponent {
  @Input() visible: boolean = false;
  @Input() zIndex: number = 990;

  @Output() click = new EventEmitter();

  get animationState(): string {
    return this.visible ? 'active' : 'inactive';
  }
}
