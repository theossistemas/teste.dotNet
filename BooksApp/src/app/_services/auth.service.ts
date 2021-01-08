import { Injectable } from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  protected  UrlServiceV1: string = "http://localhost:5000/v1/api/user";

 jwtHelper = new JwtHelperService();
 decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(`${this.UrlServiceV1}/login`, model).pipe(
      map((response:any)=>{
        const user = response;

        if(user){
          localStorage.setItem('token', user.tocken);
          this.decodedToken = this.jwtHelper.decodeToken(user.tocken);

          sessionStorage.setItem('username', this.decodedToken.unique_name)
          sessionStorage.setItem('userid', this.decodedToken.nameid)
          sessionStorage.setItem('role', this.decodedToken.role)
        }
      })
    )
  }
  //veriicar se está locagado
  isLoggedIn(){
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token)
  }

  register(model:any){
    return this.http.post(`${this.UrlServiceV1}/register`, model)
  }
}


