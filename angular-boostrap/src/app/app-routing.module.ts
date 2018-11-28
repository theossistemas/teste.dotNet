import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuardService } from './guards/auth-guard.service';
import { LoginComponent } from './login/login.component';
import { PublicComponent } from './public/public.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: '',
    component: PublicComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    //canActivate: [AuthGuardService],
    component: RegisterComponent
  },
  {
    path: 'register/:id',
    //canActivate: [AuthGuardService],
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
