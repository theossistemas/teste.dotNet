import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountService } from '../shared/services/account.service';
import { AppAccess } from './constants';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  baseAPI = environment.api;
  constructor(private _accountService: AccountService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = this._accountService.currentUserValue;
    const isApiUrl = request.url.startsWith(this.baseAPI);
    const isLoggedIn = (currentUser && currentUser.jwtToken);

    if (isLoggedIn && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.jwtToken}`
        }
      });
    }

    return next.handle(request);
  }
}
