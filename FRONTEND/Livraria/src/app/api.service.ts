import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'https://localhost:44315/Livraria';
  private authUrl = 'https://localhost:44315/Auth';

  constructor(private http: HttpClient) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({ 'Authorization': token ? `Bearer ${token}` : '' });
  }

  getDados(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  deleteLivro(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, { headers: this.getAuthHeaders() });
  }

  createLivro(livro: any): Observable<any> {
    return this.http.post(this.apiUrl, livro, { headers: this.getAuthHeaders() });
  }

  updateLivro(livro: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${livro.id}`, livro, { headers: this.getAuthHeaders() });
  }
  getLivrosPaged(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.authUrl}/login`, { username, password });
  }
}
