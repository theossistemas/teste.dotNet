import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

type LoginRequest = { email: string; password: string };
type LoginResponse = { token?: string; accessToken?: string; jwt?: string; [k: string]: any };

@Injectable({ providedIn: 'root' })
export class AuthService {
  private base = `${environment.apiBaseUrl}/Auth`;
  private platformId = inject(PLATFORM_ID);

  constructor(private http: HttpClient) {}

  private isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  login(body: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.base}/login`, body);
  }

  saveTokenFromResponse(res: LoginResponse) {
    if (!this.isBrowser()) return; // nada de localStorage no server
    const candidates = [
      res?.token, res?.accessToken, res?.jwt, ...(Object.values(res || {}))
    ].filter((v): v is string => typeof v === 'string');

    const jwt = candidates.find(v => v.split('.').length === 3);
    if (jwt) localStorage.setItem('token', jwt);
  }

  getToken(): string | null {
    if (!this.isBrowser()) return null;
    return localStorage.getItem('token');
  }

  logout() {
    if (!this.isBrowser()) return;
    localStorage.removeItem('token');
  }

  private decodePayload(): any | null {
    const t = this.getToken();
    if (!t) return null;
    try {
      const json = atob(t.split('.')[1]);
      return JSON.parse(json);
    } catch {
      return null;
    }
  }

  private isExpired(): boolean {
    const p = this.decodePayload();
    if (!p?.exp) return false;
    const nowSec = Math.floor(Date.now() / 1000);
    return p.exp < nowSec;
    }

  isAuthenticated(): boolean {
    const t = this.getToken();
    return !!t && !this.isExpired();
  }

  hasRole(role: string): boolean {
    const p = this.decodePayload();
    if (!p) return false;
    // Common JWT role claim shapes:
    // - ASP.NET Core: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    // - Simpler APIs: "role" (string or array) or "roles" (array)
    const roleUri = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

    const candidateValues: unknown[] = [];
    if (p.role !== undefined) candidateValues.push(p.role);
    if (p.roles !== undefined) candidateValues.push(p.roles);
    if (p[roleUri] !== undefined) candidateValues.push(p[roleUri]);

    for (const v of candidateValues) {
      if (typeof v === 'string' && v === role) return true;
      if (Array.isArray(v) && v.includes(role)) return true;
    }
    return false;
  }

  getEmail(): string | null {
    const p = this.decodePayload();
    if (!p) return null;
    // Backend usa JwtRegisteredClaimNames.Sub com email
    return typeof p.sub === 'string' ? p.sub : null;
  }

  getRoles(): string[] {
    const p = this.decodePayload();
    if (!p) return [];
    const roleUri = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
    const out = new Set<string>();
    const push = (v: unknown) => {
      if (typeof v === 'string') out.add(v);
      if (Array.isArray(v)) v.forEach(x => { if (typeof x === 'string') out.add(x); });
    };
    push(p.role);
    push(p.roles);
    push(p[roleUri]);
    return Array.from(out);
  }
}

// serviço p/ login/logout e gerência de token JWT
