import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { Pessoa } from '../cadastro/models/pessoa';
import { PessoaService } from '../cadastro/services/pessoa.service';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-arquivos',
  templateUrl: './arquivos.component.html',
  styleUrls: ['./arquivos.component.css']
})
export class ArquivosComponent implements OnInit {

  displayedColumns: string[] = ['nome', 'formato', 'data', 'acoes'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  mostrarMaisFiltros = false;
  srcResult: any;
  nomeArquivo: string;
  inputNode: any;
  reader = new FileReader();

  arquivos = [
    { id: 1, nome: 'Informativo sobre a reforma da piscina.', formato: 'PDF', data: '10/01/2020' },
    { id: 1, nome: 'Prestação de contas.', formato: 'EXCEL', data: '10/01/2020' },
    { id: 1, nome: 'Informativo sobre a eleição.', formato: 'DOC', data: '10/01/2020' },
    { id: 1, nome: 'Informativo sobre a pintura dos prédios.', formato: 'PDF', data: '10/01/2020' },
    { id: 1, nome: 'Informativo sobre a reforma no salão de festas.', formato: 'DOC', data: '10/01/2020' },
    { id: 1, nome: 'Informativo sobre a reforma da portaria.', formato: 'PDF', data: '10/01/2020' },

  ];

  constructor(public dialog: MatDialog, private pessoaService: PessoaService, public router: Router) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.obterUsuarios();
  }

  confirmarExclusao(obj): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: 'auto',
      data: { titulo: 'Confirmação', mensagem: 'Deseja realmente excluir este arquivo?' }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // excluir
      }
    });
  }

  onFileSelected() {
    this.inputNode = document.querySelector('#file');

    if (typeof (FileReader) !== 'undefined') {
      this.reader.onload = (e: any) => {
        this.srcResult = e.target.result;
      };

      this.reader.readAsArrayBuffer(this.inputNode.files[0]);
      this.nomeArquivo = this.inputNode.files[0].name;
    }
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  obterUsuarios() {    
    this.dataSource.data = this.arquivos;
    // this.pessoaService.obterListaFuncionario()
    //   .subscribe((funcionarios: any) => {
    //     this.dataSource.data = funcionarios;
    //   });
  }

  mostrarOcultarMaisFiltros() {
    this.mostrarMaisFiltros = !this.mostrarMaisFiltros;
  }
}
