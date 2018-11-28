
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { GlobalService } from './commons/services/global.service';
import { SecureDirective } from './commons/directives/secure.directive';
import { AuthGuardService } from './guards/auth-guard.service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UiModule } from './ui/ui.module';
import { LoginComponent } from './login/login.component';
import { PublicComponent } from './public/public.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PublicComponent,
    RegisterComponent,
    SecureDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    UiModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    AuthGuardService,
    GlobalService
  ],
  bootstrap: [
    AppComponent
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule { }
