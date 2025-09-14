import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // não anexa Authorization no login
  if (req.url.includes('/Auth/login')) return next(req);

  // pular arquivos estaticos (assets)
  const u = req.url || '';
  if (u.startsWith('assets') || u.includes('/assets/')) return next(req);

  // SSR: não há localStorage
  if (typeof localStorage === 'undefined') return next(req);

  const token = localStorage.getItem('token');
  if (!token) return next(req);

  return next(req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }));
};
// adiciona o token JWT no cabeçalho Authorization
// se houver token no localStorage (após login)
