import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import {registerLocaleData} from '@angular/common';
import localePt from '@angular/common/locales/pt';
//registerLocaleData(localePt);

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LivroComponent } from './livro/livro.component';
import { HomeComponent } from './home/home.component';
import { ListaLivroComponent } from './livro/lista-livro/lista-livro.component';
import { LivroService } from './_services/livro.service';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { TituloComponent } from './_shared/titulo/titulo.component';
import { RegisterComponent } from './user/register/register.component';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {AuthInterceptor} from './auth.intercept';
import { ContatoComponent } from './contato/contato.component';
import {ModalModule} from 'ngx-bootstrap/modal';
import { SepararPipe } from './separar.pipe';
import { CurrencyFormatPipe } from './currency-format.pipe';
import { AdminComponent } from './administrativo/admin/admin.component';
import { AdmComponent } from './administrativo/adm/adm.component';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {NgxCurrencyModule} from 'ngx-currency';
import { SobreComponent } from './sobre/sobre.component';


registerLocaleData(localePt, 'pt');

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LivroComponent,
    HomeComponent,
    ListaLivroComponent,
    UserComponent,
    LoginComponent,
    TituloComponent,
    RegisterComponent,
    ContatoComponent,
    SepararPipe,
    CurrencyFormatPipe,
    AdminComponent,
    AdmComponent,
    SobreComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({progressBar: true, preventDuplicates: false, timeOut: 1000}),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    NgxCurrencyModule,

  ],
  providers: [
    LivroService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }

  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
