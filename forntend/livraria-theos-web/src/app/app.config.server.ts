import { ApplicationConfig, mergeApplicationConfig } from '@angular/core';
import { provideServerRendering } from '@angular/platform-server';
import { provideHttpClient } from '@angular/common/http';
import { appConfig as browserConfig } from './app.config';

const serverOnly: ApplicationConfig = {
  providers: [
    provideServerRendering(),
    provideHttpClient(),
  ],
};

export const appConfig = mergeApplicationConfig(browserConfig, serverOnly);
