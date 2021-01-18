import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { IPublisher } from '../../../models/publisher.model';

@Component({
  selector: 'app-publishers-list',
  templateUrl: './publishers-list.component.html',
})
export class PublishersListComponent {

  public _baseUrl = '';
  public _action = 'publishers'
  public list: IPublisher[] = [];
  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.getList();
  }

  delete(id: any) {
    this.http.delete<IPublisher>(`${this._baseUrl}api/${this._action}/${id}`).subscribe(result => {
      console.log(result);
      this.getList();
    }, error => console.error(error));
  }

  getList() {
    this.http.get<IPublisher[]>(`${this._baseUrl}api/${this._action}`).subscribe(result => {
      this.list = result;
    }, error => console.error(error));
  }
}
