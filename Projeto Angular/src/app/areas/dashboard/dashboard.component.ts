import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { GlobalService } from 'src/app/shared/services/global.service';
import { Livro } from '../cadastro/models/livro-model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  @BlockUI() blockUi: NgBlockUI;
  livros: Livro[];

  constructor(private toast: ToastrService, private globalService: GlobalService<any>
    ) { }

  ngOnInit() {
   
    this.obterLivros();
  }
  obterLivros() {    
    this.blockUi.start();

    this.globalService.listar("Livro/ObterLivros")
      .pipe(
        finalize(() => this.blockUi.stop())
      ).subscribe((dados: any) => {        
        this.livros = dados as Livro[];
                
      }, (erros: HttpErrorResponse) => {
        console.log(erros);
        this.toast.error('Não foi possívelc conectar-se ao servidor.', 'Atenção', { enableHtml: true });        
      });
  }
 
}