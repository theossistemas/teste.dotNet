import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Livro } from '../models/livro.model';

const baseUrl = 'https://localhost:5001/livraria-api';

@Injectable({
  providedIn: 'root'
})
export class LivrosService {
  teste?:string = '';
  header?: HttpHeaders;

  constructor(private http: HttpClient) { }

  getAll(token:any): Observable<Livro[]> {
    return this.http.get<Livro[]>(`${baseUrl}/Livro`, {headers:this.header});
  }

  get(id: any): Observable<Livro> {
    return this.http.get(`${baseUrl}/Livro/${id}`, {headers:this.header});
    // return this.http.get(`${baseUrl}/${id}`);
  }

  create(data: any): Observable<any> {
    return this.http.post(`${baseUrl}/Livro/create`, data, {headers:this.header});
    return this.http.post(baseUrl, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}/Livro/update`, data, {headers:this.header});
  }

  delete(id: any): Observable<any> {
    return this.http.delete(`${baseUrl}/Livro/delete/${id}`, {headers:this.header});
  }

  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl);
  }

  findByTitle(nome: any): Observable<Livro[]> {
    return this.http.get<Livro[]>(`${baseUrl}/Livro/Name?name=${nome}`,{headers:this.header});
  }

  setHeader(token:any){
    this.header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }
}
