import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { IGenre } from '../../../models/genre.model';

@Component({
  selector: 'app-genre-list',
  templateUrl: './genre-list.component.html',
})
export class GenreListComponent {

  public _baseUrl = '';
  public genres: IGenre[] = [];
  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.getList();
  }

  delete(id: any) {
    this.http.delete<IGenre>(this._baseUrl + 'api/genres/' + id).subscribe(result => {
      console.log(result);
      this.getList();
    }, error => console.error(error));
  }

  getList() {
    this.http.get<IGenre[]>(this._baseUrl + 'api/genres').subscribe(result => {
      this.genres = result;
    }, error => console.error(error));
  }

}
