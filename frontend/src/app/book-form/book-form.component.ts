import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BookService } from '../services/book.service';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent implements OnInit {
  bookForm!: FormGroup;
  public isEditMode = false;
  private currentBookId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.currentBookId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.currentBookId;

    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      publicationYear: ['', [Validators.required, Validators.pattern("^[0-9]*$")]]
    });

    if (this.isEditMode && this.currentBookId) {
      this.bookService.getLivroById(this.currentBookId).subscribe(livro => {
        this.bookForm.patchValue(livro);
      });
    }
  }

  onSubmit(): void {
    if (this.bookForm.invalid) {
      return;
    }

    if (this.isEditMode && this.currentBookId) {
      this.bookService.updateLivro(this.currentBookId, this.bookForm.value).subscribe(
        () => {
          alert('Livro atualizado com sucesso!');
          this.router.navigate(['/livros']);
        },
        (error) => console.error('Erro ao atualizar livro:', error)
      );
    } else {
      this.bookService.addLivro(this.bookForm.value).subscribe(
        () => {
          alert('Livro salvo com sucesso!');
          this.router.navigate(['/livros']);
        },
        (error) => console.error('Erro ao salvar o livro:', error)
      );
    }
  }
}