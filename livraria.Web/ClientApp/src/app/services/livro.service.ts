import { SharedService } from './shared.service';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LivroService {


  private accessPointUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient, private _sharedService:SharedService) {
    this.accessPointUrl = baseUrl + 'api/livro/'
  }


  public GetAll(): Observable<any> {
    return this.http.get(this.accessPointUrl);
  }

  public Create(livro): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${this._sharedService.getUser().accessToken}`);
    return this.http.post(this.accessPointUrl, livro, { headers: headers});
  }

  public Edit(livro): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${this._sharedService.getUser().accessToken}`);
    return this.http.put(this.accessPointUrl, livro, { headers: headers});
  }

  public Delete(livro): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${this._sharedService.getUser().accessToken}`);
    return this.http.delete(this.accessPointUrl+ livro.livroId, { headers: headers});
  }
}
