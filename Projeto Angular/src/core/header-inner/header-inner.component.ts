import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-header-inner',
  templateUrl: './header-inner.component.html'
})
export class HeaderInnerComponent {
  constructor(
    private _router: Router) {
      
    //this.receiverLogin(false);
  }

  sair(){
    console.log('foi')
    this._router.navigate(['login']);
  }
}