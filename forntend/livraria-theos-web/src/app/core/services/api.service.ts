import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = (environment.apiBaseUrl || '').trim(); // ex.: '/api' ou 'http://localhost:5122/api'

  /** Junta base + caminho de forma segura (funciona com '/api' + '/Livros', ou com URL absoluta) */
  private url(u: string): string {
    // Se já for absoluta, retorna do jeitinho que veio
    if (/^https?:\/\//i.test(u)) return u;

    const base = this.base.replace(/\/+$/g, ''); // remove barras finais
    let path = (u || '').trim();

    // sempre começa com uma barra
    if (!path.startsWith('/')) path = `/${path}`;

    // evita /api/api/... quando base já termina com /api e o path também começa com /api
    if (base.endsWith('/api') && path.startsWith('/api/')) {
      path = path.substring(4); // remove o '/api' do começo do path
    }

    return `${base}${path}`;
  }

  get<T>(u: string, params?: Record<string, any>) {
    return this.http.get<T>(this.url(u), { params: this.toParams(params) });
  }

  post<T>(u: string, body: any, headers?: HttpHeaders | { [header: string]: string | string[] }) {
    return this.http.post<T>(this.url(u), body, { headers });
  }

  put<T>(u: string, body: any) {
    return this.http.put<T>(this.url(u), body);
  }

  delete<T>(u: string) {
    return this.http.delete<T>(this.url(u));
  }

  /** Helper opcional: converte objeto simples em HttpParams */
  private toParams(obj?: Record<string, any>): HttpParams | undefined {
    if (!obj) return undefined;
    let p = new HttpParams();
    for (const [k, v] of Object.entries(obj)) {
      if (v === undefined || v === null) continue;
      p = p.set(k, String(v));
    }
    return p;
  }
}
// serviço genérico p/ chamadas HTTP (GET, POST, PUT, DELETE) com URL base da API