import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserLogin } from '../models/user-login.model';

const AUTH_API = 'https://localhost:5001/livraria-api/autenticacao/login';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { } 

  login(credentials:any): Observable<any> {

    return this.http.post<UserLogin>(`${AUTH_API}`, {
      Login: credentials.username,
      Password: credentials.password
    });
  }

  register(user:any): Observable<any> {
    return this.http.post(AUTH_API + 'signup', {
      username: user.username,
      email: user.email,
      password: user.password
    }, httpOptions);
  }
}
