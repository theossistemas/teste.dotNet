
import { Injectable } from '@angular/core';

import { Token } from 'src/app/models/token';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from './../../commons/services/global.service';
import { environment } from './../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  _URL = `${environment.endpoint}/1.0/book`;

  constructor(
    private httpClient: HttpClient
  ) {}


  userLogin(data) : Observable<Token> {
      return this.httpClient.post<Token>(`${this._URL}/1.0/login`, {
        ... data
      });
  }

}
