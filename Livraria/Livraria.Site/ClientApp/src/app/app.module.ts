import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Globals } from './globals';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ListagemLivrosComponent } from './listagem-livros/listagem-livros.component';
import { AutenticacaoComponent } from './autenticacao/autenticacao.component';
import { IncluirLivroComponent } from './incluir-livro/incluir-livro.component';
import { AlterarLivroComponent } from './alterar-livro/alterar-livro.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListagemLivrosComponent,
    AutenticacaoComponent,
    IncluirLivroComponent,
    AlterarLivroComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'listagem-livros', component: ListagemLivrosComponent },
      { path: 'autenticacao', component: AutenticacaoComponent },
      { path: 'incluir-livro', component: IncluirLivroComponent },
      { path: 'alterar-livro/:id', component: AlterarLivroComponent }
    ])
  ],
  providers: [
    Globals
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
