import { Injectable, Inject } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  
  callEventLogin: Subject<any> = new Subject<any>();
  constructor() {
  }

  eventLogin(IsLoged: any) {
    this.callEventLogin.next(IsLoged);
    return this.callEventLogin.asObservable();
  }

  getUser():any
  {
    const userStrg = localStorage.getItem('usuario');
    return JSON.parse(userStrg);
  }

}
