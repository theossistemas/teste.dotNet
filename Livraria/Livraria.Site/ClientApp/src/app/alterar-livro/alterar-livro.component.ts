import { Component, Inject, OnInit } from '@angular/core';
import { Globals } from '../globals';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-alterar-livro-component',
  templateUrl: './alterar-livro.component.html'
})
export class AlterarLivroComponent implements OnInit {
  constructor(public globals: Globals, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private route: ActivatedRoute, public router: Router) {

  }

  ngOnInit() {
    console.log("ngOnInit");
    this.route.params.subscribe(params => {
      this.http.get<Retorno>(this.baseUrl + 'api/Livro/Consultar/' + params['id']).subscribe(result => {
        console.log(result.data);
        this.model = result.data;
        this.model.autor = this.model.autor.nome;
        this.model.editora = this.model.editora.nome;
      }, result => {
        console.error(result.error);
      });
    });
  }

  model: any = {};
  erros: any = {
    Nome: [],
    Descricao: [],
    Autor: [],
    Editora: [],
    Edicao: []
  };
  mensagem: string = '';

  public salvar() {
    this.mensagem = '';
    this.limparErros();
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.globals.token
    });
    let options = { headers: headers };
    var body = {
      id: this.model.id,
      nome: this.model.nome,
      descricao: this.model.descricao,
      autor: { Nome: this.model.autor },
      editora: { Nome: this.model.editora },
      edicao: this.model.edicao,
    }
    this.http.put(this.baseUrl + 'api/Livro/Alterar', body, options).subscribe(result => {
      this.limpar();
      this.mensagem = 'Livro alterado com sucesso.';
      this.router.navigate(["/listagem-livros"]);
    }, result => {
      var error = result.error;
      console.error(error);
      for (var name in error.errors) {
        var erro = error.errors[name];
        this.erros[erro.key].push(erro.value);
      }
    });
  }

  private limparErros() {
    this.erros = {
      Nome: [],
      Descricao: [],
      Autor: [],
      Editora: [],
      Edicao: []
    };
  }

  private limpar() {
    this.model = {};
  }
}

interface Retorno {
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

