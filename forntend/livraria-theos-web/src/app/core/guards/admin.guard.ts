import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.isAuthenticated() && auth.hasRole('Admin')) return true;

  // sem permissão → volta pra lista e mostra aviso (o interceptor também cobre 403 vindos do back)
  alert('Apenas usuários com perfil Admin podem acessar esta página.');
  return router.createUrlTree(['/livros']);
};
// guarda de rota p/ proteger rotas que requerem perfil Admin