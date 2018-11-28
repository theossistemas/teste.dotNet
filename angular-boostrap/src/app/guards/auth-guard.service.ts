import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { GlobalService } from './../commons/services/global.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(
    private globalService: GlobalService,
    private router: Router,
  ) { }
  
  canActivate() {
    if ( this.globalService.userLogged() ) {
      return true;
    }
    this.globalService.clearAuthLogin();
  }
}