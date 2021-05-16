import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AccountService } from '../shared/services/account.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private accountService: AccountService,
    private messageService: MessageService
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(catchError(err => {
        const error = err.error.message || err.error.title ||err.statusText;

        if (err.status === 401 || err.status === 511) {
          if (err.status === 401) {
            this.messageService.add({ severity: 'error', summary: 'Mensagem de Erro', detail: 'Não autorizado!' });
          } else if (err.status === 511) {
            this.messageService.add({ severity: 'error', summary: 'Mensagem de Erro', detail: 'Login não realizado!' });
          }
          this.accountService.logout();
          this.router.navigate(['/login']);
        } else {
          this.messageService.add({ severity: 'error', summary: 'Mensagem de Erro', detail: error });
        }

        return throwError(error);
      }));
  }
}
