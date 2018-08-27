import { Component, Inject } from '@angular/core';
import { Globals } from '../globals';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-incluir-livro-component',
  templateUrl: './incluir-livro.component.html'
})
export class IncluirLivroComponent {
  constructor(public globals: Globals, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

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
      nome: this.model.nome,
      descricao: this.model.descricao,
      autor: { Nome: this.model.autor },
      editora : { Nome: this.model.editora },
      edicao: this.model.edicao,
    }
    this.http.post(this.baseUrl + 'api/Livro/Incluir', body, options).subscribe(result => {
      this.limpar();
      this.mensagem = 'Livro incluido com sucesso.';
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

