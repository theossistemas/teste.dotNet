import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPublisher } from '../../../models/publisher.model';

@Component({
  selector: 'app-publishers-form',
  templateUrl: './publishers-form.component.html',
})
export class PublishersFormComponent {

  public item: IPublisher = { name: '', website: '' };
  public _baseUrl = '';
  public _action = 'publishers'
  public id: any;
  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, public router: Router) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id !== '0') {
      this.http.get<IPublisher>(`${this._baseUrl}api/${this._action}/${this.id}`).subscribe(result => {
        this.item = result;
      }, error => console.error(error));
    }
  }

  public save() {
    console.log('save', this.item);
    if (this.item.id) {
      this.http.put<IPublisher>(`${this._baseUrl}api/${this._action}/${this.item.id}`, this.item).subscribe(result => {
        this.item = result;
        alert('Editado com sucesso!');
      }, error => console.error(error));
    } else {
      this.http.post<IPublisher>(`${this._baseUrl}api/${this._action}`, this.item).subscribe(result => {
        this.item = result;
        alert('Salvo com sucesso!');
      }, error => console.error(error));
    }

  }
}
