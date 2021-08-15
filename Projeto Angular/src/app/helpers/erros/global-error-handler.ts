import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/areas/login/services/login-service.service';

const toastOptions = {
    position: 'toast-bottom-center'
};

@Injectable()
export class GlobalErrorHandler extends ErrorHandler {

    constructor(private toastr: ToastrService, private injector: Injector, private zone: NgZone) {
        super();
    }

    handleError(errorResponse: HttpErrorResponse | any) {
        if (errorResponse instanceof HttpErrorResponse) {
            const message = errorResponse.error;

            this.zone.run(() => {
                switch (errorResponse.status) {
                    case 400:
                        errorResponse.error.forEach(e => {
                            this.toastr.error(e, 'Atenção');
                        });
                        break;
                    case 401:
                        this.toastr.warning('Você precisa logar no sistema para acessar este recurso.', null, {
                            positionClass: toastOptions.position
                        });
                        this.injector.get(LoginService).handleLogin();
                        break;
                    case 403:
                        this.toastr.error(message || 'Não autorizado.', null, {
                            positionClass: toastOptions.position
                        });
                        break;
                    case 404:
                        this.toastr.error(message || 'Recurso não encontrado.', null, {
                            positionClass: toastOptions.position
                        });
                        break;
                    default:
                        this.toastr.error('Não foi possível conectar-se ao servidor.', null, {
                            positionClass: toastOptions.position
                        });
                        break;
                }
            });
        }
        super.handleError(errorResponse);
    }
}
