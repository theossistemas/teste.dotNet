import { Injectable } from '@angular/core';
import { ServiceBase } from 'src/app/helpers/services/services-base.interface';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Book } from '../models/book.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import uuid from 'uuid';
const queryString = require('query-string');

@Injectable({
  providedIn: 'root'
})
export class BookService extends ServiceBase {
  constructor(private readonly http: HttpClient) {
    super(http);
  }

  public getAll(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this._api}Book`, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }

  public create(dto: Book): Observable<boolean> {
    return this.http.post<boolean>(`${this._api}Book`, dto, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }

  public edit(id: uuid, dto: Book): Observable<boolean> {
    return this.http.put<boolean>(`${this._api}Book/${id}`, dto, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }

  public delete(id: uuid): Observable<boolean> {
    return this.http.delete<boolean>(`${this._api}Book/${id}`, this._httpOptions)
      .pipe(map(response => {
        return response;
      }));
  }
}
