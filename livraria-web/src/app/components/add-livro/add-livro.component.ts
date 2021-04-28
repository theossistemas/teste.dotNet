import { Component, OnInit } from '@angular/core';
import { Livro } from 'src/app/models/livro.model';
import { LivrosService } from 'src/app/service/livros.service';


@Component({
  selector: 'app-add-livro',
  templateUrl: './add-livro.component.html',
  styleUrls: ['./add-livro.component.css']
})
export class AddLivroComponent implements OnInit {

  livro: Livro = {
    nome: '',
    genero: '',
    editora: '',
    autor: '',
    sinopse: ''
  };
  submitted = false;

  constructor(private livroService: LivrosService) { }

  ngOnInit(): void {
  }

  salvarLivro(): void {
    const data = {
      Nome: this.livro.nome,
      Genero: this.livro.genero,
      Editora: this.livro.editora,
      Autor: this.livro.autor,
      Sinopse: this.livro.sinopse
    };

    this.livroService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
        },
        error => {
          console.log(error);
        });
  }

  novoLivro(): void {
    this.submitted = false;
    this.livro = {
      nome: '',
      genero: '',
      editora: '',
      autor: '',
      sinopse: ''
    };
  }

}
