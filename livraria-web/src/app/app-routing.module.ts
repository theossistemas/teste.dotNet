import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AddLivroComponent} from './components/add-livro/add-livro.component';
import {LivroDetailsComponent} from './components/livro-details/livro-details.component';
import {LivroListComponent} from './components/livro-list/livro-list.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { BoardUserComponent } from './board-user/board-user.component';
import { BoardModeratorComponent } from './board-moderator/board-moderator.component';
import { BoardAdminComponent } from './board-admin/board-admin.component';
const routes: Routes = [
 
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }, 
  { path: 'user', component: BoardUserComponent },
  { path: 'mod', component: BoardModeratorComponent },
  { path: 'admin', component: BoardAdminComponent },  

  { path: '', redirectTo: 'livros', pathMatch: 'full' },
  { path: 'livros', component: LivroListComponent },
  { path: 'livros/:id', component: LivroDetailsComponent },
  { path: 'add', component: AddLivroComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
