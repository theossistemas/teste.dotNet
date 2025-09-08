import { Component } from '@angular/core';
import { ApiService } from '../../api.service';
import { DatePipe, CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { Livro } from '../../interfaces/livraria.interface';
import { TagModule } from 'primeng/tag';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FormsModule } from '@angular/forms';
import { LivrariaCreateComponent } from '../livraria-create/livraria-create.component';
import { SpeedDialModule } from 'primeng/speeddial';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { timeout } from 'rxjs/operators';
import { LocaleDatePipe } from "../../pipes/locale-date.pipe";
import { PaginatorModule } from 'primeng/paginator';


@Component({
  selector: 'app-livraria-list',
  standalone: true,
  imports: [
    TableModule, TagModule, FloatLabelModule,
    FormsModule, LivrariaCreateComponent, CommonModule, SpeedDialModule,
    InputTextModule, ConfirmDialogModule, ToastModule, ProgressSpinnerModule,
    LocaleDatePipe, PaginatorModule
],
  templateUrl: './livraria-list.component.html',
  styleUrl: './livraria-list.component.css'
})
export class LivrariaListComponent {
openViewModal(_t33: any) {
throw new Error('Method not implemented.');
}
  livros: Livro[] = [];
  filtro: string = '';
  pageSize: number = 10;
  pageNumber: number = 1;
  totalRecords: number = 0;
  createModal: boolean = false;
  livroParaExcluir: Livro | null = null;
  livroParaEditar: Livro | null = null;
  loading: boolean = false;

  constructor(private apiService: ApiService, private confirmationService: ConfirmationService, private messageService: MessageService) {}

  getLivrosPaged(event?: any) {
    if (event) {
      this.pageNumber = event.page + 1;
      this.pageSize = event.rows;
    }
    this.loading = true;
    this.apiService.getLivrosPaged(this.pageNumber, this.pageSize).pipe(
      timeout(10000)
    ).subscribe({
      next: (data) => {
        this.livros = data.livros;
        this.totalRecords = data.totalCount;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: 'Falha ao carregar os livros. Tente novamente mais tarde.'
        });
      }
    });
  }

  ngOnInit() {
    this.getLivrosPaged();
  }
  get livrosFiltrados() {
  return this.livros.filter(livro =>
    livro.titulo.toLowerCase().includes(this.filtro.toLowerCase()) ||
    livro.autor.toLowerCase().includes(this.filtro.toLowerCase())
  )
}

getLivros() {
  this.apiService.getDados().subscribe(data => {
    this.livros = data;
  });
}

  onPageChange(event: any) {
    console.log('opa kk')// Handle page change event
  }

  load(){
    console.log('opa')
  }
  cancel(){}
  

  abrirCreate() {
  this.createModal = true;
}

  openEditModal(livro: Livro) {
    this.livroParaEditar = { ...livro };
    this.createModal = true;
  }

  onSalvarLivro(livro: Livro) {
    if (livro.id) {
      // Atualizar livro existente
      this.apiService.updateLivro(livro).subscribe({
        next: () => {
          this.getLivrosPaged();
          this.messageService.add({
            severity: 'success',
            summary: 'Atualizado',
            detail: 'Livro atualizado com sucesso!'
          });
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Erro',
            detail: 'Falha ao atualizar o livro.'
          });
        }
      });
    } else {
      // Criar novo livro
      this.apiService.createLivro(livro).subscribe({
        next: () => {
          this.getLivrosPaged();
          this.messageService.add({
            severity: 'success',
            summary: 'Criado',
            detail: 'Livro criado com sucesso!'
          });
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Erro',
            detail: 'Falha ao criar o livro.'
          });
        }
      });
    }
    this.createModal = false;
    this.livroParaEditar = null;
  }




confirmDelete(livro: Livro) {
    this.livroParaExcluir = livro;
    this.confirmationService.confirm({
      message: `Deseja realmente deletar o livro "${livro.titulo}"?`,
      header: 'Confirmação de exclusão',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Deletar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button',
      rejectButtonStyleClass: 'p-button-danger',
      accept: () => {
        if (this.livroParaExcluir) {
          this.apiService.deleteLivro(this.livroParaExcluir.id).subscribe({
            next: () => {
              this.getLivrosPaged();
              this.messageService.add({
                severity: 'success',
                summary: 'Deletado',
                detail: 'Livro deletado com sucesso!'
              });
            },
            error: (err) => {
              console.error('Erro ao deletar livro:', err);
              this.messageService.add({
              severity: 'error',
              summary: 'Erro',
              detail: 'Falha ao deletar o livro. Tente novamente mais tarde.'
            });
            }
          });
        }
      },
      reject: () => {
        return;
      }
    });
  }

  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}



