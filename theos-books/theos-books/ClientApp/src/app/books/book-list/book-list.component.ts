import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { IBook } from '../../../models/book.model';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
})
export class BookListComponent {
  public _baseUrl = '';
  public _action = 'books'
  public list: IBook[] = [];

  public isAuthenticated: Observable<boolean>;

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authorizeService: AuthorizeService) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.getList();
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  delete(id: any) {
    this.http.delete<IBook>(`${this._baseUrl}api/${this._action}/${id}`).subscribe(result => {
      console.log(result);
      this.getList();
    }, error => console.error(error));
  }

  getList() {
    this.http.get<IBook[]>(`${this._baseUrl}api/${this._action}`).subscribe(result => {
      this.list = result;
    }, error => console.error(error));
  }
}
