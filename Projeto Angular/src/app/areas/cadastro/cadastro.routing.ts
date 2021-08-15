import { CriarEditarLivroComponent } from './livro/criar-editar-livro/criar-editar-livro.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [    
    { path: 'cadastro-livro/criar-editar', component: CriarEditarLivroComponent },
    { path: 'cadastro-livro/criar-editar/:id', component: CriarEditarLivroComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CadastroRoutingModule { }