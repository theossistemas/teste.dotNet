import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IGenre } from '../../../models/genre.model';

@Component({
  selector: 'app-genre-form',
  templateUrl: './genre-form.component.html',
})
export class GenreFormComponent {

  public genre: IGenre = { name: '', description: '' };
  public _baseUrl = '';
  public id: any;
  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, public router: Router) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id !== '0') {
      this.http.get<IGenre>(this._baseUrl + 'api/genres/' + this.id).subscribe(result => {
        this.genre = result;
      }, error => console.error(error));
    }
  }

  public save() {
    console.log('save', this.genre);
    if (this.genre.id) {
      this.http.put<IGenre>(this._baseUrl + 'api/genres/' + this.genre.id, this.genre).subscribe(result => {
        this.genre = result;
        alert('Editado com sucesso!');
      }, error => console.error(error));
    } else {
      this.http.post<IGenre>(this._baseUrl + 'api/genres', this.genre).subscribe(result => {
        this.genre = result;
        alert('Salvo com sucesso!');
      }, error => console.error(error));
    }

  }
}
