import { GlobalService } from './../services/global.service';
import { Directive, AfterViewInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';

@Directive({
  selector: '[appSecure]'
})
export class SecureDirective implements AfterViewInit {
  
  constructor(
    private globalService: GlobalService,
    private router: Router,
    private eltRef: ElementRef) {
  }
  
  ngAfterViewInit(): void {
    if (!this.globalService.userLogged() ) {
      let el: HTMLElement = this.eltRef.nativeElement;
      el.parentNode.removeChild( el );
    }
  }
}
