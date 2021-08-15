import { PessoaService } from './../services/pessoa.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { Pessoa } from '../models/pessoa';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
  styleUrls: ['./pessoa.component.css']
})
export class PessoaComponent implements OnInit {
  displayedColumns: string[] = ['nome', 'matricula', 'cpf', 'acoes'];
  dataSource = new MatTableDataSource<Pessoa>();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  mostrarMaisFiltros = false;

  constructor(public dialog: MatDialog, private pessoaService: PessoaService, public router: Router) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.obterUsuarios();
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

  obterUsuarios() {    
    this.pessoaService.obterListaFuncionario()
      .subscribe((funcionarios: any) => {
        this.dataSource.data = funcionarios;
      });
  }

  mostrarOcultarMaisFiltros() {
    this.mostrarMaisFiltros = !this.mostrarMaisFiltros;
  }
}