import { Routes } from '@angular/router';
import { BookListComponent } from './book-list/book-list.component';
import { BookFormComponent } from './book-form/book-form.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { authGuard } from './auth.guard';


export const routes: Routes = [
    { path: '', component: LoginComponent },

    { path: 'livros', component: BookListComponent },

    { path: 'novo-livro', component: BookFormComponent },

    { path: 'login', redirectTo: '', pathMatch: 'full' },

    { path: 'registrar', component: RegisterComponent },

    { path: 'editar-livro/:id', component: BookFormComponent, canActivate: [authGuard] }
];