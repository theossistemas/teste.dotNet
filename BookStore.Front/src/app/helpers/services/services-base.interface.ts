import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { Access } from 'src/app/shared/models/access.model';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

export interface IServiceBase {
  _api: string;
  _httpOptions: {};
  _httpClient: HttpClient;
}

export abstract class ServiceBase implements IServiceBase {

  public _api: string;
  public _httpOptions: {};
  public _httpClient: HttpClient;
  public _currentUserSubject: BehaviorSubject<Access>;
  public _currentUser: Observable<Access>;

  constructor(http: HttpClient) {
    this._api = environment.api;
    this._httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    this._httpClient = http;
    this._currentUserSubject = new BehaviorSubject<Access>(JSON.parse(localStorage.getItem('currentUser')));
    this._currentUser = this._currentUserSubject.asObservable();
  }
}
