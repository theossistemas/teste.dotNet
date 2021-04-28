import { Component, OnInit } from '@angular/core';
import { Livro } from 'src/app/models/livro.model';
import { LivrosService } from 'src/app/service/livros.service';
import { TokenStorageService } from '../../_services/token-storage.service';
import {Router} from '@angular/router'

@Component({
  selector: 'app-livro-list',
  templateUrl: './livro-list.component.html',
  styleUrls: ['./livro-list.component.css']
})
export class LivroListComponent implements OnInit {

  livros?: Livro[];
  currentLivro?: Livro;
  currentIndex = -1;
  nome = '';
  token = '';
  isAdmin =false;

  constructor(private livroService: LivrosService,private tokenStorage: TokenStorageService, private router: Router) { }

  ngOnInit(): void {
    if(this.tokenStorage.getUser()?.token != null){
      this.token = this.tokenStorage.getUser().token;      
      this.livroService.setHeader(this.token);
      this.isAdmin = this.tokenStorage.getUser().role == "Admin" ? true : false;
      this.retrieveLivros();
    }
    else{
      this.router.navigate(['/livros']);
    }
   
  }

  retrieveLivros(): void {
    this.livroService.getAll(this.token)
      .subscribe(
        data => {
          this.livros = data;
          console.log(this.livros);
        },
        error => {
          console.log(error);
        });
  }

  refreshList(): void {
    // this.retrieveLivros();
    this.currentLivro = undefined;
    this.currentIndex = -1;
  }

  setActiveLivro(livro: Livro, index: number): void {
    this.currentLivro = livro;
    this.currentIndex = index;
  }

  removeAllLivros(): void {
    this.livroService.deleteAll()
      .subscribe(
        response => {
          console.log(response);
          this.refreshList();
        },
        error => {
          console.log(error);
        });
  }

  searchNome(): void {
    this.livroService.findByTitle(this.nome)
      .subscribe(
        data => {
          this.livros = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

}
