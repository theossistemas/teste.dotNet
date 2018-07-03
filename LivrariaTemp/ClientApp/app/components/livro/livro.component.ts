import { Component, OnInit, OnDestroy } from '@angular/core';
import { LivroService } from '../../Services/livro.service';
import { ILivro } from '../../Models/livro.interface';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'livro',
    templateUrl: './livro.component.html'
})

export class LivroComponent implements OnInit {

    livros: ILivro[] = [];
    livro: ILivro = <ILivro>{};

    //formulário
    formLabel: string;
    isEditMode = false;
    form: FormGroup; 

    constructor(private livroService: LivroService,
        private fb: FormBuilder) {
        this.form = fb.group({
            "titulo": ["", Validators.required],
            "valor": ["", Validators.required]
        });

        this.formLabel = "Adicionar Livro";
    }

    ngOnInit() {
        this.getLivros();
    }

    private getLivros() {
        this.livroService.getLivros().subscribe(
            data => this.livros = data,
            error => alert(error),
            () => console.log(this.livros)
        );
    }

    onSubmit() {
        this.livro.titulo = this.form.controls["titulo"].value;
        this.livro.valor = this.form.controls["valor"].value;

        if (this.isEditMode) {
            this.livroService.editLivro(this.livro)
                .subscribe(response => {
                    this.getLivros();
                    this.form.reset();
                });
        }

            this.livroService.addLivro(this.livro)
                .subscribe(response => {
                    this.getLivros();
                    this.form.reset();
                });
               
    };

    cancel() {
        this.formLabel = "Adicionar Livro";
        this.isEditMode = false;
        this.livro = <ILivro>{};
        this.form.get("titulo")!.setValue("");
        this.form.get("valor")!.setValue("");
    };

    edit(livro: ILivro) {
        this.formLabel = "Editar Livro";
        this.isEditMode = true;
        this.livro = livro;
        this.form.get("titulo")!.setValue(livro.titulo);
        this.form.get("valor")!.setValue(livro.valor);
    }

    delete(livro: ILivro) {
        if (confirm("Deseja realmente excluir esse livro?")) {
            this.livroService.deleteLivro(livro.livroId)
                .subscribe(response => {
                    this.getLivros();
                    this.form.reset();
                });
        }
    };
}
