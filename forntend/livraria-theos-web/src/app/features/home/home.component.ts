import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [CommonModule, RouterLink],
  template: `
    <section style="text-align:center; margin-top: 2rem;">
      <h1>Bem-vindo à Livraria Theós</h1>
      <p style="margin: 1rem 0;">Gerencie seus livros com facilidade.</p>
      <div style="display:flex; gap: 12px; justify-content:center;">
        <a routerLink="/login" style="padding: 8px 12px; border: 1px solid #ccc; border-radius: 4px;">Fazer login</a>
        <a routerLink="/livros" style="padding: 8px 12px; border: 1px solid #ccc; border-radius: 4px;">Listar livros</a>
      </div>
    </section>
  `
})
export class HomeComponent {}

