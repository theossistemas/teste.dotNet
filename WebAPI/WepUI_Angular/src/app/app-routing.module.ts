import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; 
import { AuthGuard } from './shared';
import { LoginComponent } from './admin/login/login.component';  
import { DetalheLivroComponent } from './site/detalheLivro/detalheLivro.component';
import { HomeComponent } from './site/home/Home.component';
import { LayoutComponent } from './admin/components/layout/layout.component';
import { ListaLivroComponent } from './admin/livro/list/listaLivro.component';
import { LivroComponent } from './admin/livro/form/livro.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'detalheLivro', component: DetalheLivroComponent },
    { path: '', 
      component: LayoutComponent, 
      canActivate: [AuthGuard],
      children: [
        { path: 'listaLivro', component: ListaLivroComponent, canActivate: [AuthGuard] }, 
        { path: 'livro', component: LivroComponent, canActivate: [AuthGuard] }
        ]
    },
    { path: '**', redirectTo: '' }
    ];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { } 
