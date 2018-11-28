import { Book } from './../../models/book';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from './../../commons/services/global.service';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  
  _URL = `${environment.endpoint}/1.0/book`;

  constructor(
    private globalService : GlobalService,
    private httpClient: HttpClient
  ) {}

  getBookById(id: number){
    return this.httpClient.get(`${this._URL}/${id}`);  
  }

  saveBook(data: Book) {
    return this.httpClient.post(`${this._URL}`, data);
  }

  updateBook(id: number, data: Book){
    return this.httpClient.put(`${this._URL}/${id}`, data);  
  }
}
