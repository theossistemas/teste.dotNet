import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink } from '@angular/router';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './app.component.html' // estrutura básica da aplicação
})
export class AppComponent {
  constructor(private auth: AuthService) {}

  get isAuth() { return this.auth.isAuthenticated(); }
  get isAdmin() { return this.auth.hasRole('Admin'); }
  get email() { return this.auth.getEmail(); }
  get roles() { return this.auth.getRoles(); }

  sair() {
    this.auth.logout();
    location.href = '/livros';
  }
}

