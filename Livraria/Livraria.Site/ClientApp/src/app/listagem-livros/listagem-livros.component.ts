import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Globals } from '../globals';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listagem-livros',
  templateUrl: './listagem-livros.component.html',
  styleUrls: ['./listagem-livros.component.css']
})
export class ListagemLivrosComponent {

  public livros: Livro[];
  private livroExcluir: Livro;
  private mdlSampleIsOpen: boolean = false;

  constructor(public globals: Globals, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, public router: Router) {
    this.carregarLista();
    this.limparLivroExcluir();
  }

  private carregarLista() {
    this.http.get<RetornoListarOrdenadoPorNome>(this.baseUrl + 'api/Livro/ListarOrdenadoPorNome').subscribe(result => {
      console.log(result);
      this.livros = result.data;
    }, error => console.error(error));
  }

  public editar(livro: Livro) {
    this.router.navigate(['/alterar-livro/' + livro.id]);
  }

  private limparLivroExcluir() {
    this.livroExcluir = {
      id: '',
      nome: '',
      descricao: '',
      autor: {
        id: '',
        nome: ''
      },
      editora: {
        id: '',
        nome: ''
      },
      edicao: 0
    };
  }

  private openModal(livro: Livro): void {
    console.log(livro);
    this.mdlSampleIsOpen = true;
    this.livroExcluir = livro;
  }

  private closeModal() {
    this.mdlSampleIsOpen = false;
  }

  private confirmarExclusao() {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.globals.token
    });
    let options = { headers: headers };
    this.http.delete(this.baseUrl + 'api/Livro/Excluir/' + this.livroExcluir.id, options).subscribe(result => {
      this.closeModal();
      this.carregarLista();
      this.limparLivroExcluir();
    }, result => {
      console.error(result.error);
    });
  }
}
interface RetornoListarOrdenadoPorNome {
  data: Livro[]
}

interface Livro {
  id: string;
  nome: string;
  descricao: string;
  autor: Autor;
  editora: Editora;
  edicao: number;
}

interface Autor {
  id: string;
  nome: string;
}
interface Editora {
  id: string;
  nome: string;
}
