import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges, OnChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { Livro } from '../../interfaces/livraria.interface';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ProgressSpinnerModule } from "primeng/progressspinner";

@Component({
  selector: 'app-livraria-create',
  standalone: true,
  imports: [ButtonModule, DialogModule, CommonModule, FloatLabelModule, FormsModule, InputTextModule, ToastModule, ProgressSpinnerModule],
  templateUrl: './livraria-create.component.html',
  styleUrl: './livraria-create.component.css'
})
export class LivrariaCreateComponent implements OnChanges {
  bookTitle: string = '';
  bookAuthor: string = '';
  bookIsbn: string = '';
  bookYear: number | null = null;
  bookQuantity: number | null = null;
  bookPrice: number | null = null;

  @Input() livroParaEditar: Livro | null = null;
  @Output() salvarLivro = new EventEmitter<Livro>();
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();

  /**
   *
   */
  constructor(private messageService: MessageService) {
    
  }

  loading = false

  ngOnChanges(changes: SimpleChanges) {
    if (changes['livroParaEditar'] && this.livroParaEditar) {
      this.bookTitle = this.livroParaEditar.titulo;
      this.bookAuthor = this.livroParaEditar.autor;
      this.bookIsbn = this.livroParaEditar.isbn;
      this.bookYear = this.livroParaEditar.anoPublicacao;
      this.bookQuantity = this.livroParaEditar.quantidade;
      this.bookPrice = this.livroParaEditar.preco;
    }
    if (changes['visible'] && !this.visible) {
      // Limpar campos ao fechar modal
      this.bookTitle = '';
      this.bookAuthor = '';
      this.bookIsbn = '';
      this.bookYear = null;
      this.bookQuantity = null;
      this.bookPrice = null;
      this.livroParaEditar = null;
    }
  }

  load(){
    console.log('opa')
  }
  
  closeDialog() {
    this.visible = false;
    this.visibleChange.emit(false);
    this.livroParaEditar = null;
  }

  createBook() {
    if (!this.bookTitle || !this.bookAuthor || !this.bookIsbn || !this.bookYear || !this.bookQuantity || !this.bookPrice) {
      this.messageService.add({
            severity: 'error',
            summary: 'Impossível Criar',
            detail: 'Todos os campos são obrigatórios!'
          });
      return;
    }
    this.loading = true;
    const livro: any = {
      titulo: this.bookTitle,
      autor: this.bookAuthor,
      isbn: this.bookIsbn,
      anoPublicacao: this.bookYear !== null ? this.bookYear : 0,
      quantidade: this.bookQuantity !== null ? this.bookQuantity : 0,
      preco: this.bookPrice !== null ? this.bookPrice : 0
    };
    if (this.livroParaEditar) {
      livro.id = this.livroParaEditar.id;
    }
    this.salvarLivro.emit(livro);
    // Limpar formulário após criar/editar
    this.bookTitle = '';
    this.bookAuthor = '';
    this.bookIsbn = '';
    this.bookYear = null;
    this.bookQuantity = null;
    this.bookPrice = null;
    this.visibleChange.emit(false);
    this.loading = false;
    this.visible = false;
  }

    get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

}
