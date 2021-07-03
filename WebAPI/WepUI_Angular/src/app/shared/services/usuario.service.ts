import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import {  } from '@angular/common/http';
import { environment } from '../../../environments/environment';  
import { ResponseModel } from '../models/response.model';
import { UsuarioModel } from '../models/usuario.model';
import { ResponseLoginModel } from '../models/responseLogin.model';
import { AuthenticationService } from './authentication.service';

@Injectable({
    providedIn: 'root'
})
export class UsuarioService {
    // Http Headers
    headers = new HttpHeaders().append('Content-Disposition', 'multipart/form-data');
    httpOptions: {};
    token: ResponseLoginModel;

    constructor(private http: HttpClient, private authenticationService: AuthenticationService) {
            this.token = this.authenticationService.currentUserValue as ResponseLoginModel;
            this.httpOptions = { headers: new HttpHeaders(
                    {'Content-Type': 'application/json', 
                     'Authorization': `Bearer ${this.token.token}` })};
    }

    post(input: UsuarioModel): Observable<ResponseModel> {
        return this.http.post<ResponseModel>(`${environment.baseUrl}/Usuario`, input, this.httpOptions);
    }

    put(input: UsuarioModel): Observable<ResponseModel> {
        return this.http.put<ResponseModel>(`${environment.baseUrl}/Usuario`, input, this.httpOptions);
    } 
    
    getAll(): Observable<ResponseModel> {
        return this.http.get<ResponseModel>(`${environment.baseUrl}/Usuario/ObterLista`);
    }

    get(idUsuario: number): Observable<ResponseModel> {
        return this.http.get<ResponseModel>(`${environment.baseUrl}/Usuario/ObterPorId/${idUsuario}`);
    } 
 
    handleError(error: any) {
        const errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
