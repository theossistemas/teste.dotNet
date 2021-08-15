import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ResultadoConsulta } from '../models/resultado-consulta';
import { environment } from 'src/environments/environment';
import { Helpers } from 'src/app/helpers/helpers';
import { RetornoApiCep } from '../models/retorno-api-cep';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GlobalService<T> {
  private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient, public router: Router) { }

    getItems(caminho) {
        return this.http.get(this.apiUrl + `${caminho}`)
            .pipe(map(res => res));
    }

    listar(caminho): Observable<ResultadoConsulta<T>> {
        return this.http.get<ResultadoConsulta<T>>(this.apiUrl + `${caminho}`, { headers: Helpers.getHttpHeaders() })
            .pipe(map(res => res));
    }

    listarResultados(caminho, filtro: any): Observable<ResultadoConsulta<T>> {
        return this.http.get<ResultadoConsulta<T>>(this.apiUrl + `${caminho}` + this.prepararParametros(filtro),
            { headers: Helpers.getHttpHeaders() });
    }

    obterUnico(caminho, filtro: any): Observable<T> {
        return this.http.get<ResultadoConsulta<T>>(this.apiUrl + `${caminho}` + this.prepararParametros(filtro),
            { headers: Helpers.getHttpHeaders() })
            .pipe(
                map((result: any) => result.lista.shift())
            );
    }

    postItems(caminho, objeto) {
        return this.http.post(this.apiUrl + `${caminho}`, Helpers.toJson(objeto), { headers: Helpers.getHttpHeaders() })
            .pipe(map(res => res));
    }

    postWithFile(caminho, formData) {
        const req = new HttpRequest('POST', this.apiUrl + caminho, formData);
        return this.http.request(req).pipe(map(res => res));
    }

    getItemsObjeto(caminho, objeto) {
        return this.http.get(this.apiUrl + `${caminho}` + objeto)
            .pipe(map(res => res));
    }

    private prepararParametros(filtro: any): string {
        let params = '?';

        if (filtro) {
            const paramsList = Object.keys(filtro)
                .map((param) => this.retornarParametrosComArray(param.toString(), filtro[param]))
                .join('&');

            params = params + paramsList;
        }

        return params;
    }

    private retornarParametrosComArray(nome: string, campo: any): string {
        let textoArray = '';
        let caracterLigacao = '';
        if (Array.isArray(campo)) {
            campo.forEach(valor => {
                textoArray += caracterLigacao + nome + '=' + valor;
                caracterLigacao = '&';
            });
            return textoArray;
        }
        return nome + '=' + encodeURIComponent(campo);
    }
}
