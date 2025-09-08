import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ToastModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [MessageService]
})
export class LoginComponent {
  username = '';
  password = '';
  error = '';

  constructor(private api: ApiService, private router: Router, private messageService: MessageService) {}

  login() {
    this.api.login(this.username, this.password).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
        this.messageService.add({severity:'success', summary:'Login realizado', detail:'Bem-vindo!'});
        setTimeout(() => this.router.navigate(['/']), 1200);
      },
      error: () => {
        this.error = 'Usuário ou senha inválidos';
        this.messageService.add({severity:'error', summary:'Login falhou', detail:'Usuário ou senha inválidos'});
      }
    });
  }

  entrarSemLogin() {
    this.router.navigate(['/']);
  }
}
