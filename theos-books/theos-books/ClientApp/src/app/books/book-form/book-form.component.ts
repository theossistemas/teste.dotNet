import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IBook } from '../../../models/book.model';
import { IGenre } from '../../../models/genre.model';
import { IPublisher } from '../../../models/publisher.model';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
})

export class BookFormComponent {

  public item: IBook = {
    genre: {},
    publisher: {}
  };
  public _baseUrl = '';
  public _action = 'books'
  public id: any;
  public genres: IGenre[] = [];
  public publishers: IPublisher[] = [];
  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, public router: Router) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id !== '0') {
      this.http.get<IBook>(`${this._baseUrl}api/${this._action}/${this.id}`).subscribe(result => {
        this.item = result;
      }, error => console.error(error));
    }
    this.getGenreList();
    this.getPublisherList();
  }

  getGenreList() {
    this.http.get<IGenre[]>(this._baseUrl + 'api/genres').subscribe(result => {
      this.genres = result;
    }, error => console.error(error));
  }

  getPublisherList() {
    this.http.get<IPublisher[]>(`${this._baseUrl}api/publishers`).subscribe(result => {
      this.publishers = result;
    }, error => console.error(error));
  }

  public save() {
    this.item.genre = this.genres.find(g => g.id.toString() === this.item.genre.id.toString());
    this.item.publisher = this.publishers.find(p => p.id.toString() === this.item.publisher.id.toString());
    console.log('save 2', this.item);
    if (this.item.id) {
      this.http.put<IBook>(`${this._baseUrl}api/${this._action}/${this.item.id}`, this.item).subscribe(result => {
        this.item = result;
        alert('Editado com sucesso!');
      }, error => console.error(error));
    } else {
      this.http.post<IBook>(`${this._baseUrl}api/${this._action}`, this.item).subscribe(result => {
        this.item = result;
        this.id = result.id;
        alert('Salvo com sucesso!');
      }, error => console.error(error));
    }
  }

  compareByOptionId(idFist, idSecond) {
    return idFist && idSecond && idFist.id == idSecond.id;
  }
}
