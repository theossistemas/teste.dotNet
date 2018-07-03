import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { ILivro } from '../Models/livro.interface';

@Injectable()
export class LivroService {

    constructor(private http: Http) { }

    //get
    getLivros() {
        return this.http.get("/api/livros").map(data => <ILivro[]>data.json());
    }

    //post
    addLivro(livro: ILivro) {
        return this.http.post("/api/livros", livro);
    }

    //put
    editLivro(livro: ILivro) {
        return this.http.put(`/api/livros/${livro.livroId}`, livro);
    }

    //delete
    deleteLivro(livroId: number) {
        return this.http.delete(`/api/livros/${livroId}`);
    }
}
