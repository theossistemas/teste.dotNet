import { Component, OnInit } from '@angular/core';
import { Livro } from 'src/app/models/livro.model';
import { ActivatedRoute, Router } from '@angular/router';
import { LivrosService } from 'src/app/service/livros.service';

@Component({
  selector: 'app-livro-details',
  templateUrl: './livro-details.component.html',
  styleUrls: ['./livro-details.component.css']
})
export class LivroDetailsComponent implements OnInit {

  currentLivro: Livro = {
    nome: '',
    genero: '',
    editora: '',
    autor: '',
    sinopse: ''
  };
  message = '';

  constructor(
    private livroService: LivrosService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.message = '';
    this.getLivro(this.route.snapshot.params.id);
  }

  getLivro(id: string): void {
    this.livroService.get(id)
      .subscribe(
        data => {
          this.currentLivro = data;
          console.log(data);
          
        },
        error => {
          console.log(error);
        });
  }

  // updatePublished(status: boolean): void {
  //   const data = {
  //     Name: this.currentLivro.Name,
  //     Genero: this.currentLivro.Genero,
  //     Editora: this.currentLivro.Editora,
  //     Autor: this.currentLivro.Autor,
  //     Sinopse: this.currentLivro.Sinopse,
  //     Published: status
  //   };

  //   this.livroService.update(this.currentLivro.id, data)
  //     .subscribe(
  //       response => {
  //         console.log(response);
  //         this.message = response.message;
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // }

  updateLivro(): void {
    this.livroService.update(this.currentLivro.id, this.currentLivro)
      .subscribe(
        response => {
          console.log(response);
          this.message = response.message;
          this.router.navigate(['/livros']);
        },
        error => {
          console.log(error);
        });
  }

  deleteLivro(): void {
    this.livroService.delete(this.currentLivro.id)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/livros']);
        },
        error => {
          console.log(error);
        });
  }

}
