import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Livro {
    id: number;
    titulo: string;
    autor: string;
    anoPublicacao: number;
    genero: string;
}

@Injectable({
    providedIn: 'root'
})
export class LivrosService {
    private apiUrl = 'https://api.exemplo.com/livros';

    constructor(private http: HttpClient) {}

    listarLivros(): Observable<Livro[]> {
        return this.http.get<Livro[]>(this.apiUrl);
    }

    obterLivro(id: number): Observable<Livro> {
        return this.http.get<Livro>(`${this.apiUrl}/${id}`);
    }

    adicionarLivro(livro: Livro): Observable<Livro> {
        return this.http.post<Livro>(this.apiUrl, livro);
    }

    atualizarLivro(id: number, livro: Livro): Observable<Livro> {
        return this.http.put<Livro>(`${this.apiUrl}/${id}`, livro);
    }

    removerLivro(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}