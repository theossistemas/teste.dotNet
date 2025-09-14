import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = (environment.apiBaseUrl ?? '').trim();

  private buildUrl(u: string): string {
    if (/^https?:\/\//i.test(u)) return u;       // já é absoluta
    if (!this.base) return u.startsWith('/') ? u : `/${u}`; // proxy (base vazia)
    const base = this.base.replace(/\/+$/, '');
    const path = u.replace(/^\/+/, '');
    return `${base}/${path}`;
  }

  get<T>(u: string, params?: Record<string, any>) { return this.http.get<T>(this.buildUrl(u), { params }); }
  post<T>(u: string, body: any)                   { return this.http.post<T>(this.buildUrl(u), body); }
  put<T>(u: string, body: any)                    { return this.http.put<T>(this.buildUrl(u), body); }
  delete<T>(u: string)                            { return this.http.delete<T>(this.buildUrl(u)); }
}
