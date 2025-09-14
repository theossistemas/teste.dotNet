import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LivrosService, Livro } from '../../../core/services/livros.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  standalone: true,
  selector: 'app-livros-list',
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <h2>Livros</h2>

    <div class="mb-2">
      <input [(ngModel)]="filtro" (keyup.enter)="buscar()" placeholder="Buscar por título/autor" />
      <button (click)="buscar()">Buscar</button>
      <a *ngIf="isAdmin" routerLink="/livros/novo" class="ml-2">Novo</a>
    </div>

    <div *ngIf="carregando()">Carregando...</div>

    <table *ngIf="!carregando() && livros().length">
      <thead>
        <tr>
          <th style="text-align:left;">Título</th>
          <th style="text-align:left;">Autor</th>
          <th>Preço</th>
          <th>Estoque</th>
          <th *ngIf="isAdmin" style="width:160px;">Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let l of livros()">
          <td>{{ l.titulo }}</td>
          <td>{{ l.autor }}</td>
          <td style="text-align:right;">{{ l.preco | currency:'BRL' }}</td>
          <td style="text-align:right;">{{ l.estoque }}</td>
          <td *ngIf="isAdmin">
            <a [routerLink]="['/livros', l.id, 'editar']">Editar</a>
            <button (click)="excluir(l)" style="margin-left:8px;">Excluir</button>
          </td>
        </tr>
      </tbody>
    </table>

    <p *ngIf="!carregando() && !livros().length">Nenhum livro encontrado.</p>
  `
})
export class LivrosListComponent implements OnInit {
  livros = signal<Livro[]>([]);
  carregando = signal<boolean>(false);
  filtro = '';

  constructor(private service: LivrosService, private auth: AuthService) {}

  get isAdmin() {
    return this.auth.hasRole('Admin');
  }

  ngOnInit() { this.buscar(); }

  buscar() {
    this.carregando.set(true);
    this.service.listar(this.filtro).subscribe({
      next: res => this.livros.set(res.itens ?? []),
      error: _ => this.livros.set([]),
      complete: () => this.carregando.set(false)
    });
  }

  excluir(l: Livro) {
    if (!l.id) return;
    const ok = confirm(`Tem certeza que deseja excluir "${l.titulo}"?`);
    if (!ok) return;

    this.service.excluir(l.id).subscribe({
      next: () => this.buscar(),
      error: () => alert('Não foi possível excluir. Verifique permissões e tente novamente.')
    });
  }
}
// componente p/ listar livros com busca e link p/ criar novo