import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArquivosComponent } from './arquivos.component';

const routes: Routes = [
  { path: 'arquivos', component: ArquivosComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArquivosRoutingModule { }
