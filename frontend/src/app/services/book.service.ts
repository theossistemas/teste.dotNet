import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'http://localhost:5239/api/books';

  constructor(private http: HttpClient) { }

  getLivros(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  addLivro(livro: any): Observable<any> {
    return this.http.post(this.apiUrl, livro);
  }

  deleteLivro(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getLivroById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  updateLivro(id: string, livro: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, livro);
  }
}