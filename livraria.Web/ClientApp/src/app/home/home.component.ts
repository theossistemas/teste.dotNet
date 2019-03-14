import { CategoriaService } from './../services/categoria.service';
import { Component, OnInit } from '@angular/core';
import { LivroService } from '../services/livro.service';
import { Usuario } from '../models/usuario';
import { SharedService } from '../services/shared.service';
import { AutorService } from '../services/autor.service';
import { FilterPipe } from 'ngx-filter-pipe';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NgxConfirmBoxService } from 'ngx-confirm-box';

declare var $: any;


@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.css'],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  usuario = new Usuario();
  livros: any = [];
  strglivros: any = [];
  filtro: any = '';

  livro: any = {};

  autores: any = [];
  categorias: any = [];

  constructor(private livroService: LivroService,
    sharedService: SharedService,
    private autorService: AutorService,
    private filterPipe: FilterPipe,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private confirmBox: NgxConfirmBoxService,
    private categoriaService: CategoriaService) {

    this.validaUsuario();

    sharedService.callEventLogin.subscribe(a => {
      if (a) {
        this.validaUsuario();
      }
      else {
        this.usuario = new Usuario();
      }

    })

  }

  validaUsuario() {
    const userStrg = localStorage.getItem('usuario');
    if (userStrg)
      this.usuario = JSON.parse(userStrg);
  }

  ngOnInit(): void {
    this.getLivros()
    this.autorService.GetAll().subscribe(data => this.autores = data);
    this.categoriaService.GetAll().subscribe(data => this.categorias = data);
  }

  getLivros() {
    this.spinner.show();
    this.livroService.GetAll().subscribe(data => {
      this.livros = data;
      this.strglivros = data;
      this.spinner.hide();
    }, error => {
      this.spinner.hide();
      this.toastr.error(error.error, 'Erro');
    })
  }

  pressFormConsulta(e) {
    if (e.keyCode == 13)
      this.consultar();
  }

  consultar() {
    debugger
    this.livros = this.filterPipe.transform(this.strglivros, {
      $or: [
        {
          titulo: this.filtro
        },
        {
          categoria: {
            descricao: this.filtro
          }
        }, {

          autor: {
            nome: this.filtro
          }
        }

      ]
    })
  }

  create() {
    $('#modalCadastro').modal('show');
  }

  confirmCreate() {
    this.spinner.show();
    this.livroService.Create(this.livro).subscribe(data => {
      this.livro = {};
      this.getLivros();
      $('#modalCadastro').modal('hide');
      this.spinner.hide();
      this.toastr.success('registro cadastrado com sucesso');
    }, error => {
      this.spinner.hide();
      this.toastr.error(error.error, 'Erro');
    })
  }

  edit(e) {
    this.livro = e;
    this.livro.categoriaId = this.livro.categoria.categoriaId
    this.livro.autorId = this.livro.autor.autorId
    $('#modalAlteracao').modal('show');
    $('#modalAlteracao').on('hidden.bs.modal', () => {
      this.livro = {};
    });
  }

  confirmEdit(e) {

    debugger
    this.spinner.show();
    const livroEdit = {
      Id: this.livro.livroId,
      Livro: this.livro
    }
    this.livroService.Edit(livroEdit).subscribe(data => {
      this.livro = {};
      this.getLivros();
      $('#modalAlteracao').modal('hide');
      this.spinner.hide();
      this.toastr.success('registro alterado com sucesso');
    }, error => {
      this.spinner.hide();
      this.toastr.error(error.error, 'Erro');
    })
  }

  delete(e) {
    this.livro = e;
    this.confirmBox.show();
  }

  confirmDelete(e) {
    if (!e) {
      this.livro = {};
      return;
    }
    this.spinner.show();
    this.livroService.Delete(this.livro).subscribe(data => {
      this.getLivros();
      $('#modalAlteracao').modal('hide');
      this.toastr.success('registro excluido com sucesso');
    }, error => {
      this.spinner.hide();
      this.toastr.error(error.error, 'Erro');
    })
  }

  setAutor(e) {
    this.livro.autorId = parseInt(e);
  }

  setCategoria(e) {
    this.livro.categoriaId = parseInt(e);
  }
}
