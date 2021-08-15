import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { PessoaService } from '../cadastro/services/pessoa.service';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-financeiro',
  templateUrl: './financeiro.component.html',
  styleUrls: ['./financeiro.component.css']
})
export class FinanceiroComponent implements OnInit {

  displayedColumns: string[] = ['nome', 'formato', 'valor' ,'data', 'acoes'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  mostrarMaisFiltros = false;
  srcResult: any;
  nomeArquivo: string;
  inputNode: any;
  reader = new FileReader();

  arquivos = [
    { id: 1, nome: 'Valores recebidos da mensalidade do condomínio.', formato: 'Creditado', valor: 10000, data: '10/01/2020' },
    { id: 1, nome: 'Prestação de contas.', formato: 'Debitado', valor: 10000, data: '10/01/2020' },
    { id: 1, nome: 'Valores gastos com reforma da piscina.', formato: 'Debitado', valor: 10000, data: '10/01/2020' },
    { id: 1, nome: 'Valores gastos com reforma do salão de festas.', formato: 'Debitado', valor: 10000.50, data: '10/01/2020' },
    { id: 1, nome: 'Valores recebidos de acordos com inadimplentes.', formato: 'Creditado', valor: 10000.50,data: '10/01/2020' },
    { id: 1, nome: 'Valores gastos com funcionários.', formato: 'Debitado', valor: 10000.50, data: '10/01/2020' },

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