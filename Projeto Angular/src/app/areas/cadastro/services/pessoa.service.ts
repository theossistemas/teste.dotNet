import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import * as faker from 'faker/locale/pt_BR';
import { Helpers } from 'src/app/helpers/helpers';
import { Pessoa } from '../models/pessoa';
import { Endereco } from '../models/endereco';

@Injectable({
  providedIn: 'root'
})
export class PessoaService {

  constructor() { }

  obterListaFuncionario(): Observable<Pessoa[]> {
    return this.mockFuncionarios();
  }

  private mockFuncionarios(): Observable<Pessoa[]> {
    const funcionarios = new Array<Pessoa>();

    for (let i = 0; i < 25; i++) {
      let fakeCpf = '';
      for (let j = 0; j < 3; j++) {
        fakeCpf += faker.random.number(3).toString();
      }

      const pessoa: Pessoa = {
        id: faker.random.number(),
        nome: faker.name.findName(),
        matricula: "1",
        //cpf: Helpers.convertToCpfCnpj(fakeCpf),
        cpf: fakeCpf,
        endereco: new Endereco()
      };

      funcionarios.push(pessoa);
    }

    return of(funcionarios);
  }
}
