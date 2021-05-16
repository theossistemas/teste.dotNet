import { AccountService } from 'src/app/shared/services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AppAccess } from 'src/app/helpers/constants';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  isAdmin: boolean = false;
  loggedIn: boolean = false;

  constructor(
    private readonly accountService: AccountService,
    private readonly router: Router,
    private readonly messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.start();
  }

  start(): void {
    this.isAdmin = this.accountService.isADMIN();
    this.loggedIn = this.accountService.currentUserValue != null;

    if (!this.accountService.currentUserValue) {
      this.accountService.login(AppAccess.APP_USER, AppAccess.APP_PASS)
        .subscribe(
          (data) => {
            this.start();
          },
          (err) => {
            console.error(err);
          },
          () => { }
        );
    }
  }

  login(): void {
    this.router.navigate(['/login']);
  }

  logout(): void {
    this.accountService.logout();
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Operação realizada com sucesso!' });
    this.start();
    this.router.navigate(['login']);
  }

}
