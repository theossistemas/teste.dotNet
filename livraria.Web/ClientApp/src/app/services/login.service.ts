import { Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private accessPointUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient) {
    this.accessPointUrl = baseUrl + 'api/login'
  }

  public login(uaurio: any): Observable<any> {
    return this.http.post(this.accessPointUrl, uaurio);
  }

}
