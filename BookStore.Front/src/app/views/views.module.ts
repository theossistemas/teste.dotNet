import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { ViewsRoutingModule } from './views-routing.module';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainComponent } from './main/main.component';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { ToolbarModule } from 'primeng/toolbar';
import { DialogModule } from 'primeng/dialog';
import { UsersComponent } from './users/users.component';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [
    LoginComponent,
    HomeComponent,
    MainComponent,
    UsersComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ViewsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    InputNumberModule,
    InputTextModule,
    ButtonModule,
    MenubarModule,
    ToolbarModule,
    DialogModule,
    DropdownModule
  ],
  exports: []
})
export class ViewsModule { }
