import { SharedService } from './shared.service';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AutorService {


  private accessPointUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient, private _sharedService:SharedService) {
    this.accessPointUrl = baseUrl + '/api/autor'
  }


  public GetAll(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${this._sharedService.getUser().accessToken}`);

    return this.http.get(this.accessPointUrl, {headers:headers});
  }

}
