import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { LivrariaListComponent } from './components/livraria-list/livraria-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    LivrariaListComponent,
    importProvidersFrom(BrowserAnimationsModule)
  , ConfirmationService
  , MessageService
  ]
};
