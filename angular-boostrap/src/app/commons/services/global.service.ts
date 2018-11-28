import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Token } from 'src/app/models/token';


@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  INFORMATION_AUTH = 'auth';

  constructor() { }

  public getAuthLogin(){
    const auth = localStorage.getItem(this.INFORMATION_AUTH);
    return JSON.parse(auth);
  }

  public setAuthLogin(model: Token){
    localStorage.setItem(this.INFORMATION_AUTH, JSON.stringify(model));
    return true;
  }

  public clearAuthLogin(){
    localStorage.removeItem(this.INFORMATION_AUTH);
    window.location.replace('/');
    return true;
  }

  public userLogged(){
    const auth = localStorage.getItem(this.INFORMATION_AUTH);
    return (auth == undefined || auth == null) ? false : true;
  }
}
