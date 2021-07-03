import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import {  } from '@angular/common/http';
import { environment } from '../../../environments/environment';  
import { ResponseModel } from '../models/response.model';
import { LivroModel } from '../models/livro.model';
import { AuthenticationService } from './authentication.service';
import { ResponseLoginModel } from '../models/responseLogin.model';

@Injectable({
    providedIn: 'root'
})
export class LivroService {
    // Http Headers
    headers = new HttpHeaders().append('Content-Disposition', 'multipart/form-data');
    httpOptions: {};
    token: ResponseLoginModel = new ResponseLoginModel();
  constructor(private http: HttpClient, private authenticationService: AuthenticationService) {
        if (this.authenticationService.currentUserValue != null) {
            this.token = this.authenticationService.currentUserValue as ResponseLoginModel;
            this.httpOptions = { headers: new HttpHeaders(
                        {'Content-Type': 'application/json', 
                        'Authorization': `Bearer ${this.token.token}` })};
        }
   }

    post(input: LivroModel): Observable<ResponseModel> {
        return this.http.post<ResponseModel>(`${environment.baseUrl}/Livro`, input, this.httpOptions);
    }

    put(input: LivroModel): Observable<ResponseModel> {
        return this.http.put<ResponseModel>(`${environment.baseUrl}/Livro`, input, this.httpOptions);
    }
 
    getAll(): Observable<ResponseModel> {
        return this.http.get<ResponseModel>(`${environment.baseUrl}/Livro/ObterLista`);
    }

    get(idLivro: number): Observable<ResponseModel> {
        return this.http.get<ResponseModel>(`${environment.baseUrl}/Livro/ObterPorId/${idLivro}`);
    } 
 
    handleError(error: any) {
        const errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
