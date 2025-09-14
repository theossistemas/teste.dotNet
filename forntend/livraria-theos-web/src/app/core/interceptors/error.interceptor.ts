import { HttpErrorResponse } from '@angular/common/http';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const auth = inject(AuthService);

  return next(req).pipe(
    catchError((err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 401) {
          auth.logout();
          alert('Sua sessão expirou ou você não está autenticado. Faça login.');
          router.navigate(['/login'], { queryParams: { returnUrl: location.pathname } });
        } else if (err.status === 403) {
          alert('Você não tem permissão para essa ação (requer Admin).');
        }
      }
      return throwError(() => err);
    })
  );
};
// intercepta erros HTTP 401 (não autorizado) e 403 (proibido)
// redireciona p/ login se 401, mostra alertas se 403
