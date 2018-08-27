import { Component, Inject } from '@angular/core';
import { Globals } from '../globals';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-autenticacao-component',
  templateUrl: './autenticacao.component.html'
})
export class AutenticacaoComponent {
  constructor(public globals: Globals, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, public router: Router) {

  }
  model: any = {};
  mensagem: string = '';

  public autenticar() {
    this.mensagem = '';
    this.http.post<Retorno>(this.baseUrl + 'api/Usuario/Autenticar', this.model).subscribe(result => {
      if (result.data) {
        this.globals.SetAuth(result.data.token);
        this.router.navigate(["/"]);
      } else {
        this.mensagem = 'Erro ao realizar a autenticação';
      }
    }, error => console.error(error));
  }
}

interface Retorno {
  data: Usuario;
}

interface Usuario {
  login: string;
  senha: string;
  token: string;
}

