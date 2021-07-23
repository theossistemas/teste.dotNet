import {Component, OnInit, TemplateRef} from '@angular/core';
import {Livro} from '../../_models/livro';
import {LivroService} from '../../_services/livro.service';
import {ToastrService} from 'ngx-toastr';
import {AuthService} from '../../_services/auth.service';
import {Router} from '@angular/router';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-lista-curso',
  templateUrl: './lista-livro.component.html',
  styleUrls: ['./lista-curso.component.css']
})
export class ListaLivroComponent implements OnInit {

  public livros: Livro[];
  private userid: number;
  private det:Livro;

  modalRef: BsModalRef;
  config = {
    backdrop: true,
    ignoreBackdropClick: true,
    keyboard:false
  };

  livroDetalhe: any;

  constructor(private modalService: BsModalService,
              public router: Router, private livroService: LivroService,
              private toastr: ToastrService, private authService: AuthService,
              ) {  }


  ngOnInit(): void {
    this.getNome();
    this.carregarLivro();
  }
  openModal(template: TemplateRef<any>, detalhe: any) {
    let temp = Object.assign({},detalhe)


    if(!Array.isArray(temp.resumo)){
      let res = detalhe.resumo.split(";")
      detalhe.resumo = res
    }

    this.livroDetalhe=detalhe;

    this.modalRef = this.modalService.show(template, this.config);
  }

  carregarLivro(){
    this.livroService.obterLivros()
      .subscribe(
        cur =>{
          this.livros = cur;
        },
        error => console.log(error)
      );
  }

  getNome(){
     this.userid = parseInt(sessionStorage.getItem('userid'))
  }

}
