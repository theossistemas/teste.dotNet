import { Routes } from '@angular/router';
import { LoginComponent } from './features/livros/auth/login/login.component';
import { LivrosListComponent } from './features/livros/list/livros-list.component';
import { LivrosFormComponent } from './features/livros/form/livros-form.component';
import { LivrosEditComponent } from './features/livros/edit/livros-edit.component';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },

  { path: 'login', component: LoginComponent },

  // pública
  { path: 'livros', component: LivrosListComponent },

  // protegidas (login + Admin)
  { path: 'livros/novo', component: LivrosFormComponent, canActivate: [authGuard, adminGuard] },
  { path: 'livros/:id/editar', component: LivrosEditComponent, canActivate: [authGuard, adminGuard] },

  { path: '**', redirectTo: 'livros' }
];

// define as rotas da aplicação
