import { Injectable } from '@angular/core';
import { ServiceBase } from 'src/app/helpers/services/services-base.interface';
import { HttpClient } from '@angular/common/http';
import { Access, Role } from '../models/access.model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Account } from '../models/account.model';
import { AccountInsert } from '../models/register.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends ServiceBase {
  constructor(private readonly http: HttpClient) {
    super(http);
  }

  public login(username: string, password: string): Observable<Access> {
    return this.http.post<Access>(`${this._api}account/authenticate`, { email: username, password }, this._httpOptions)
      .pipe(map(response => {
        localStorage.setItem('currentUser', JSON.stringify(response));
        this._currentUserSubject.next(response);
        return response;
      }));
  }

  public get currentUserValue(): Access {
    return this._currentUserSubject.value;
  }

  public logout(): void {
    localStorage.clear();
    this._currentUserSubject.next(null);
  }

  public isADMIN(): boolean {
    return (this._currentUserSubject.value != null && this._currentUserSubject.value.role == Role.ADMIN);
  }

  public getAll(): Observable<Account[]> {
    return this.http.get<Account[]>(`${this._api}account`, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }

  public create(dto: Account): Observable<boolean> {
    return this.http.post<boolean>(`${this._api}account`, dto, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }

  register(register: AccountInsert): any {
    return this.http.post(`${this._api}account/register`, register, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }
}
