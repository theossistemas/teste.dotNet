import { bootstrapApplication, BootstrapContext } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config.server';

export default function bootstrap(context: BootstrapContext) {
  return bootstrapApplication(AppComponent, appConfig, context);
}
