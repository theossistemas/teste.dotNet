import { Book } from './../../models/book';
import { GlobalService } from './../../commons/services/global.service';
import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PublicService {
  
  _URL = `${environment.production}/book`;

  constructor(
    private globalService : GlobalService,
    private httpClient: HttpClient
  ) {}

  getAllBooks(){
    return this.httpClient.get<Array<Book>>("https://reqres.in/api/users?page=2");  
  }

}
