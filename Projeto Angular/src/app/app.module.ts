import { FinanceiroModule } from './areas/financeiro/financeiro.module';
import { DashboardModule } from './areas/dashboard/dashboard.module';
import { ArquivosModule } from './areas/arquivos/arquivos.module';
import { RelatorioModule } from './areas/relatorio/relatorio.module';
import { LoginModule } from './areas/login/login.module';
import { PessoaModule } from './areas/cadastro/pessoa/pessoa.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID, ErrorHandler } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from 'angular-admin-lte';   //Import the layout module.
import { CoreModule } from 'src/core/core.module';
import { adminLteConf } from 'src/admin-lte.conf';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { BlockUIModule } from 'ng-block-ui';
import { MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { BrDateAdapter } from './helpers/date-picker/br-date-adapter';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { CadastroModule } from './areas/cadastro/cadastro.module';

registerLocaleData(localePt, 'pt');
@NgModule({
  declarations: [
    AppComponent    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    CadastroModule,
    PessoaModule,
    RelatorioModule,
    ArquivosModule,
    FinanceiroModule,
    LoginModule,  
    DashboardModule,
    LayoutModule.forRoot(adminLteConf),   //Provide the configuration to the layout module.    
    ToastrModule.forRoot({
      enableHtml: true,
      closeButton: true,
      positionClass: 'toast-top-center',
      preventDuplicates: true
    }),
    BlockUIModule.forRoot({})
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt' },
    { provide: MAT_DATE_LOCALE, useValue: 'pt' },
    { provide: DateAdapter, useClass: BrDateAdapter }    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
