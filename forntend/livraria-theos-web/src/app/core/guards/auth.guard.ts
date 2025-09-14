import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.isAuthenticated()) return true;

  // sem token → manda pro login
  return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
};
// guarda de rota p/ proteger rotas que requerem autenticação