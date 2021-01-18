import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ILog } from '../../../models/log.model';

@Component({
    selector: 'app-log-list',
    templateUrl: './log-list.component.html',
})

export class LogListComponent {
  public _baseUrl = '';
  public _action = 'logs'
  public list: ILog[] = [];
  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.getList();
  }

  delete(id: any) {
    this.http.delete<ILog>(`${this._baseUrl}api/${this._action}/${id}`).subscribe(result => {
      console.log(result);
      this.getList();
    }, error => console.error(error));
  }

  getList() {
    this.http.get<ILog[]>(`${this._baseUrl}api/${this._action}`).subscribe(result => {
      this.list = result;
    }, error => console.error(error));
  }
}
