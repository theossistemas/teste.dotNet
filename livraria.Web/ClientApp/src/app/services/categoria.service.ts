import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SharedService } from './shared.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  private accessPointUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient, private _sharedService:SharedService) {
    this.accessPointUrl = baseUrl + '/api/categoria'
  }


  public GetAll(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${this._sharedService.getUser().accessToken}`);
    return this.http.get(this.accessPointUrl, {headers:headers});
  }

}
