import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Helpers } from 'src/app/helpers/helpers';
import { Login } from '../models/login';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private login: Login;
  lastUrl: string;

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  loginPost(model: Login): Observable<Login> {
    const cabecalho = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = JSON.stringify(model);

    return this.httpClient.post<Login>(`${environment.apiUrl}/login`, body, { headers: cabecalho })
      .pipe(
        tap((login: Login) => this.handleLocalStorage(login))
      );
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('token') !== null;
  }

  getToken(): string {
    return this.login.accessToken;
  }

  handleLogin() {
    // this.router.navigate(['/login']);
    window.location.href = '/';
  }

  logout() {
    this.login = undefined;
    localStorage.clear();
    this.handleLogin();
  }

  getLoggedUserSchool(): string {
    return localStorage.getItem('escola');
  }

  getUserName(): string {
    return localStorage.getItem('nome');
  }

  getUserFirstName(): string {
    return localStorage.getItem('nome').split(' ')[0];
  }

  private handleLocalStorage(login: Login) {
    localStorage.setItem('token', login.accessToken);
    localStorage.setItem('nome', login.nome);
    localStorage.setItem('cpf', login.cpf);
  }
}
