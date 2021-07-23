import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Livro} from '../_models/livro';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  // endpoit
  protected  UrlServiceV1: string = "http://localhost:5000/v1/api";

  constructor(private http: HttpClient) { }

  obterLivros(): Observable<Livro[]>{
    return this.http.get<Livro[]>(`${this.UrlServiceV1}/livro`)
  }
  postLivro(livro: Livro){
    return this.http.post(`${this.UrlServiceV1}/livro`,livro);
  }

  removeLivro(livroId: number){
    return this.http.delete(`${this.UrlServiceV1}/livro/${livroId}`)
  }

  putLivro(livro: Livro){
    return this.http.put(`${this.UrlServiceV1}/livro/${livro.id}`, livro);
  }

}
