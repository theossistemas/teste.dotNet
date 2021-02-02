import { Component, OnInit } from '@angular/core'; 
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import {Router} from '@angular/router';  
import * as FileSaver from 'file-saver'; 
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { LivroModel } from 'src/app/shared/models/livro.model';
import { LivroService } from 'src/app/shared/services/livro.service';

import { ViewEncapsulation } from '@angular/core';
import { ResponseLoginModel } from 'src/app/shared/models/responseLogin.model';
@Component({
    // tslint:disable-next-line:component-selector
    selector: 'app-listaLivro',
    templateUrl: './listaLivro.component.html',
    styleUrls: ['./listaLivro.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ListaLivroComponent implements OnInit {
    isLoadPanelVisible = false;
    rows = []; 
      token: ResponseLoginModel;
    constructor(private router: Router, 
        private authenticationService: AuthenticationService, 
        private livroService: LivroService ) { 
    }

    ngOnInit() { 
        this.token = this.authenticationService.currentUserValue as ResponseLoginModel;
         this.carregaLista();
    } 
    carregaLista() {
        this.isLoadPanelVisible = true; 
        this.livroService.getAll().subscribe(res => {
          this.isLoadPanelVisible = false; 
          const data = res.conteudo as LivroModel[];
          this.rows = data.filter(x => x.ativo == true);
      },
      error => {
          this.isLoadPanelVisible = false; 
          this.livroService.handleError(error);
 
      });
    } 

    editar(e, data) {
        const idLivro: number =  data.id;
        this.router.navigate(['livro'], { queryParams: { 'idLivro': data.id } } );
    }

    excluir(e, data) {
        const request = data as LivroModel;
        request.ativo = false;
        request.idUsuario = this.token.idUsuario;
        this.livroService.put(request).subscribe(resp => {
            this.isLoadPanelVisible = false; 
            this.carregaLista();
          },
            error => {
              this.isLoadPanelVisible = false;
              this.livroService.handleError(error); 
          });
    }

  
}
