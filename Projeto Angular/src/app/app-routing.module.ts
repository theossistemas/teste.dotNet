
import { VisualizarLivroComponent } from './areas/cadastro/livro/visualizar-livro/visualizar-livro.component';
import { RelatorioComponent } from './areas/relatorio/relatorio.component';
import { LoginComponent } from './areas/login/login.component';
import { PessoaComponent } from './areas/cadastro/pessoa/pessoa.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArquivosComponent } from './areas/arquivos/arquivos.component';
import { DashboardComponent } from './areas/dashboard/dashboard.component';
import { FinanceiroComponent } from './areas/financeiro/financeiro.component';


const routes: Routes = [
  //{ path: '', redirectTo: 'login', pathMatch: 'full', },  
  { path: 'login', component: LoginComponent, },  
  { path: 'dashboard', component: DashboardComponent, },
  { path: 'cadastro-pessoa', component: PessoaComponent },
  { path: 'cadastro-livro', component: VisualizarLivroComponent },
  { path: 'relatorio', component: RelatorioComponent },
  { path: 'arquivos', component: ArquivosComponent },
  { path: 'financeiro', component: FinanceiroComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
