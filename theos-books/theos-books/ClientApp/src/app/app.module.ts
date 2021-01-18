import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { GenreListComponent } from './genre/genre-list/genre-list.component';
import { GenreFormComponent } from './genre/genre-form/genre-form.component';
import { BookListComponent } from './books/book-list/book-list.component';
import { BookFormComponent } from './books/book-form/book-form.component';
import { PublishersListComponent } from './publishers/publishers-list/publishers-list.component';
import { PublishersFormComponent } from './publishers/publishers-form/publishers-form.component';
import { LogListComponent } from './log/log-list/log-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    GenreListComponent,
    GenreFormComponent,
    BookListComponent,
    BookFormComponent,
    PublishersListComponent,
    PublishersFormComponent,
    LogListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: BookListComponent, pathMatch: 'full' },
      { path: 'book/:id', component: BookFormComponent, canActivate: [AuthorizeGuard] },
      { path: 'genre', component: GenreListComponent, canActivate: [AuthorizeGuard] },
      { path: 'genre/:id', component: GenreFormComponent, canActivate: [AuthorizeGuard] },
      { path: 'publishers', component: PublishersListComponent, canActivate: [AuthorizeGuard] },
      { path: 'publishers/:id', component: PublishersFormComponent, canActivate: [AuthorizeGuard] },
      { path: 'log', component: LogListComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
