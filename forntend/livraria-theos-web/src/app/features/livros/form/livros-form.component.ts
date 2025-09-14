import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
  FormControl,
  FormGroup
} from '@angular/forms';
import { LivrosService } from '../../../core/services/livros.service';
import { Router } from '@angular/router';

type LivroForm = {
  titulo: FormControl<string>;
  autor: FormControl<string>;
  anoPublicacao: FormControl<number | null>;
  isbn: FormControl<string | null>;
  editora: FormControl<string | null>;
  dataPublicacao: FormControl<string>;   // formato: yyyy-MM-dd
  preco: FormControl<number>;
  estoque: FormControl<number>;
};

@Component({
  standalone: true,
  selector: 'app-livros-form',
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h2>Novo Livro</h2>
    <form [formGroup]="form" (ngSubmit)="salvar()">
      <input formControlName="titulo" placeholder="Título" />
      <input formControlName="autor" placeholder="Autor" />
      <input type="number" formControlName="anoPublicacao" placeholder="Ano" />
      <input formControlName="isbn" placeholder="ISBN" />
      <input formControlName="editora" placeholder="Editora" />
      <input type="date" formControlName="dataPublicacao" />
      <input type="number" formControlName="preco" placeholder="Preço" step="0.01" />
      <input type="number" formControlName="estoque" placeholder="Estoque" />

      <div class="mt-2">
        <button type="submit" [disabled]="form.invalid || salvando">Salvar</button>
        <button type="button" (click)="voltar()" class="ml-2">Voltar</button>
      </div>
    </form>
  `
})
export class LivrosFormComponent {
  salvando = false;

  // apenas declara aqui...
  form!: FormGroup<LivroForm>;

  constructor(
    private fb: FormBuilder,
    private service: LivrosService,
    private router: Router
  ) {
    // ...e inicializa aqui (após o Angular injetar o FormBuilder)
    this.form = this.fb.group({
      titulo: this.fb.nonNullable.control('', { validators: [Validators.required] }),
      autor: this.fb.nonNullable.control('', { validators: [Validators.required] }),
      anoPublicacao: this.fb.control<number | null>(null),
      isbn: this.fb.control<string | null>(''),
      editora: this.fb.control<string | null>(''),
      dataPublicacao: this.fb.nonNullable.control(this.hojeIso(), { validators: [Validators.required] }),
      preco: this.fb.nonNullable.control(0, { validators: [Validators.required, Validators.min(0)] }),
      estoque: this.fb.nonNullable.control(0, { validators: [Validators.required, Validators.min(0)] })
    });
  }

  hojeIso() {
    const d = new Date();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${d.getFullYear()}-${m}-${day}`;
  }

  salvar() {
    if (this.form.invalid) return;

    this.salvando = true;

    const raw = this.form.getRawValue();

    const dto = {
      titulo: raw.titulo.trim(),
      autor: raw.autor.trim(),
      anoPublicacao: raw.anoPublicacao ?? null,
      isbn: raw.isbn ? raw.isbn.trim() : null,
      editora: raw.editora ? raw.editora.trim() : null,
      dataPublicacao: raw.dataPublicacao, // yyyy-MM-dd
      preco: Number(raw.preco),
      estoque: Number(raw.estoque)
    };

    this.service.criar(dto as any).subscribe({
      next: () => this.router.navigateByUrl('/livros'),
      error: () => (this.salvando = false),
      complete: () => (this.salvando = false)
    });
  }

  voltar() {
    this.router.navigateByUrl('/livros');
  }
}
// componente p/ criar novo livro (formulário reativo)