import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AccountService } from 'src/app/shared/services/account.service';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(
    private router: Router,
    private accountService: AccountService,
    private messageService: MessageService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.checkRole(route, state.url);
  }

  checkRole(route: ActivatedRouteSnapshot, url: string): boolean {
    const currentUser = this.accountService.currentUserValue;

    if (!(route.data.role !== undefined && route.data.role !== null && currentUser.role === route.data.role as number)) {
      this.router.navigate(['/main/home']);
      this.messageService.add({ severity: 'error', summary: 'Mensagem de Erro', detail: 'Sem Permissão' });
      return false;
    }
    return true;
  }
}
