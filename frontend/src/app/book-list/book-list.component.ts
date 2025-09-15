import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookService } from '../services/book.service';
import { RouterLink } from '@angular/router';
import { AuthService } from '../services/auth.service';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {
  livros: any[] = [];

  constructor(
    private bookService: BookService, 
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.bookService.getLivros().subscribe(
      (dadosRecebidos) => {
        this.livros = dadosRecebidos;
      },
      (erro) => {
        console.error('Ocorreu um erro ao buscar os livros:', erro);
      }
    );
  }

  carregarLivros(): void {
    this.bookService.getLivros().subscribe(
      (dadosRecebidos) => {
        this.livros = dadosRecebidos;
      },
      (erro) => {
        console.error('Ocorreu um erro ao buscar os livros:', erro);
      }
    );
  }

  excluirLivro(id: string): void {
    // Pede confirmação ao usuário antes de prosseguir
    if (confirm('Tem certeza que deseja excluir este livro?')) {
      this.bookService.deleteLivro(id).subscribe(
        () => {
          alert('Livro excluído com sucesso!');
          this.carregarLivros();
        },
        (erro) => {
          console.error('Erro ao excluir livro:', erro);
          alert('Ocorreu um erro ao excluir o livro.');
        }
      );
    }
  }
}