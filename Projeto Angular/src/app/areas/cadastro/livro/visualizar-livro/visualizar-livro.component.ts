import { ResultadoConsulta } from './../../../../shared/models/resultado-consulta';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { GlobalService } from 'src/app/shared/services/global.service';
import { Livro } from '../../models/livro-model';
import { Pessoa } from '../../models/pessoa';
import { PessoaService } from '../../services/pessoa.service';

@Component({
  selector: 'app-visualizar-livro',
  templateUrl: './visualizar-livro.component.html',
  styleUrls: ['./visualizar-livro.component.scss']
})
export class VisualizarLivroComponent implements OnInit {
  displayedColumns: string[] = ['nome', 'autor', 'qtdePaginas', 'genero'];
  dataSource = new MatTableDataSource<Livro>();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  mostrarMaisFiltros = false;
  @BlockUI() blockUi: NgBlockUI;

  constructor(public dialog: MatDialog, 
    private toast: ToastrService, private globalService: GlobalService<any>,
    private router: Router) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.obterLivros();
  }

  confirmarExclusao(obj): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: 'auto',
      data: { titulo: 'Confirmação', mensagem: 'Deseja realmente excluir este morador?' }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // excluir
      }
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  obterLivros() {    
    this.blockUi.start();

    this.globalService.listar("Livro/ObterLivros")
      .pipe(
        finalize(() => this.blockUi.stop())
      ).subscribe((dados: any) => {        
        this.dataSource.data = dados as Livro[];
      }, (erros: HttpErrorResponse) => {
        console.log(erros);
        this.toast.error('Não foi possívelc conectar-se ao servidor.', 'Atenção', { enableHtml: true });        
      });
  }

  mostrarOcultarMaisFiltros() {
    this.mostrarMaisFiltros = !this.mostrarMaisFiltros;
  }
}