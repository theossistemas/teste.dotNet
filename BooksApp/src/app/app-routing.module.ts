import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {HomeComponent} from './home/home.component';
import {ListaLivroComponent} from './livro/lista-livro/lista-livro.component';
import {LoginComponent} from './user/login/login.component';
import {UserComponent} from './user/user.component';
import {RegisterComponent} from './user/register/register.component';
import {ContatoComponent} from './contato/contato.component';
import {AdminComponent} from './administrativo/admin/admin.component';
import {AdmComponent} from './administrativo/adm/adm.component';
import {SobreComponent} from './sobre/sobre.component';

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent}
    ]
  },
  {
    path: 'adm', component: AdmComponent,
    children: [
      {path: 'admin', component: AdminComponent},
      {path: 'cadcurso', component: RegisterComponent}
    ]
  },
  {path: 'livros', component: ListaLivroComponent},
  {path: 'contato', component: ContatoComponent},
  {path: 'sobre', component: SobreComponent},
  {path: 'home', component: HomeComponent},
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: '**', redirectTo: 'home', pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
