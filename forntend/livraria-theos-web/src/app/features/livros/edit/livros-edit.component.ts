import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
  FormControl,
  FormGroup
} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { LivrosService, Livro } from '../../../core/services/livros.service';
import { Subscription, switchMap } from 'rxjs';

type LivroForm = {
  titulo: FormControl<string>;
  autor: FormControl<string>;
  anoPublicacao: FormControl<number | null>;
  isbn: FormControl<string | null>;
  editora: FormControl<string | null>;
  dataPublicacao: FormControl<string>;
  preco: FormControl<number>;
  estoque: FormControl<number>;
};

@Component({
  standalone: true,
  selector: 'app-livros-edit',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  template: `
    <h2>Editar Livro</h2>

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
        <a routerLink="/livros" class="ml-2">Cancelar</a>
      </div>
    </form>
  `
})
export class LivrosEditComponent implements OnDestroy {
  salvando = false;
  id!: string;

  form!: FormGroup<LivroForm>;
  sub!: Subscription;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private service: LivrosService,
    private router: Router
  ) {
    // cria o form
    this.form = this.fb.group({
      titulo: this.fb.nonNullable.control('', [Validators.required]),
      autor: this.fb.nonNullable.control('', [Validators.required]),
      anoPublicacao: this.fb.control<number | null>(null),
      isbn: this.fb.control<string | null>(''),
      editora: this.fb.control<string | null>(''),
      dataPublicacao: this.fb.nonNullable.control(this.hojeIso(), [Validators.required]),
      preco: this.fb.nonNullable.control(0, [Validators.required, Validators.min(0)]),
      estoque: this.fb.nonNullable.control(0, [Validators.required, Validators.min(0)])
    });

    // carrega o livro pelo :id da rota
    this.sub = this.route.paramMap
      .pipe(
        switchMap(pm => {
          const id = pm.get('id')!;
          this.id = id;
          return this.service.obter(id);
        })
      )
      .subscribe({
        next: (livro) => this.preencherForm(livro),
        error: () => {
          // se não achar, volta pra lista
          this.router.navigateByUrl('/livros');
        }
      });
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  hojeIso() {
    const d = new Date();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${d.getFullYear()}-${m}-${day}`;
  }

  private preencherForm(l: Livro) {
    // garante formato yyyy-MM-dd
    const data = (l.dataPublicacao || '').substring(0, 10);
    this.form.setValue({
      titulo: l.titulo,
      autor: l.autor,
      anoPublicacao: l.anoPublicacao ?? null,
      isbn: l.isbn ?? '',
      editora: l.editora ?? '',
      dataPublicacao: data || this.hojeIso(),
      preco: l.preco,
      estoque: l.estoque
    });
  }

  salvar() {
    if (this.form.invalid) return;
    this.salvando = true;

    const raw = this.form.getRawValue();
    const dto: Livro = {
      titulo: raw.titulo.trim(),
      autor: raw.autor.trim(),
      anoPublicacao: raw.anoPublicacao ?? null,
      isbn: raw.isbn ? raw.isbn.trim() : null,
      editora: raw.editora ? raw.editora.trim() : null,
      dataPublicacao: raw.dataPublicacao, // yyyy-MM-dd
      preco: Number(raw.preco),
      estoque: Number(raw.estoque)
    };

    this.service.atualizar(this.id, dto).subscribe({
      next: () => this.router.navigateByUrl('/livros'),
      error: () => (this.salvando = false),
      complete: () => (this.salvando = false)
    });
  }
}
// componente p/ editar livro existente via form reativo